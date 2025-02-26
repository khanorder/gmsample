using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Netina.Stomp.Client.Utils;
using Newtonsoft.Json;
using Websocket.Client;
using Websocket.Client.Models;
using GMServer.Stomp.Client.Interfaces;
using GMServer.Stomp.Messages;
using System.Net.WebSockets;
using System.Net.Sockets;
using NGEL.Data.Models;

namespace GMServer.Stomp
{
    public class ActiveMQStompClient : IActiveMQStompClient, IDisposable
    {
        private readonly WebsocketClient _socket;

        private readonly StompMessageSerializer _stompSerializer = new StompMessageSerializer();

        private readonly IDictionary<string, EventHandler<StompMessage>> _subscribers = new Dictionary<string, EventHandler<StompMessage>>();

        private readonly IDictionary<string, string> _connectingHeaders = new Dictionary<string, string>();

        public StompConnectionState StompState { get; private set; } = StompConnectionState.Closed;

        public string Version { get; private set; }

        public event EventHandler<StompMessage> OnConnect;

        public event EventHandler<DisconnectionInfo> OnClose;

        public event EventHandler<StompMessage> OnMessage;

        public event EventHandler<ReconnectionInfo> OnReconnect;

        public event EventHandler<StompMessage> OnError;

        public ActiveMQStompClient(string url, bool reconnectEnable = true, string? stompVersion = null, TimeSpan? reconnectTimeOut = null, string? heartBeat = null)
        {
            Version = "";

            _socket = new WebsocketClient(new Uri(url), delegate
            {
                ClientWebSocket clientWebSocket = new ClientWebSocket();
                clientWebSocket.Options.AddSubProtocol("stomp");
                return clientWebSocket;
            })
            {
                ReconnectTimeout = reconnectTimeOut,
                IsReconnectionEnabled = reconnectEnable,
                ErrorReconnectTimeout = TimeSpan.FromSeconds(2.0)
            };

            _socket.MessageReceived.Subscribe(HandleMessage);
            _socket.DisconnectionHappened.Subscribe(delegate (DisconnectionInfo info)
            {
                StompState = StompConnectionState.Closed;
                this.OnClose?.Invoke(this, info);
                _subscribers.Clear();
            });

            _socket.ReconnectionHappened.Subscribe(async delegate (ReconnectionInfo info)
            {
                if (info.Type != 0)
                {
                    this.OnReconnect?.Invoke(this, info);
                    StompState = StompConnectionState.Reconnecting;
                    await Reconnect();
                }
            });

            _connectingHeaders.Add("accept-version", string.IsNullOrEmpty(stompVersion) ? "1.0,1.1,1.2" : stompVersion);
            _connectingHeaders.Add("heart-beat", string.IsNullOrEmpty(heartBeat) ? "0,1000" : heartBeat);

            OnConnect += delegate (object? sender, StompMessage message)
            {
                Version = message.Headers["version"];
            };

            OnClose += delegate (object? sender, DisconnectionInfo disconnectionInfo)
            {
            };

            OnMessage += delegate (object? sender, StompMessage message)
            {
            };

            OnReconnect += delegate (object? sender, ReconnectionInfo reconnectionInfo)
            {
            };

            OnError += delegate (object? sender, StompMessage message)
            {
            };
        }

        public async Task ConnectAsync(IDictionary<string, string> headers)
        {
            try
            {
                if (!_socket.IsRunning)
                {
                    await _socket.Start();
                }

                if (!_socket.IsRunning)
                {
                    throw new Exception("Connection is not open");
                }

                if (StompState != StompConnectionState.Closed)
                {
                    return;
                }

                foreach (KeyValuePair<string, string> header in headers)
                {
                    _connectingHeaders.Add(header);
                }

                StompMessage message = new StompMessage("CONNECT", _connectingHeaders);
                await _socket.SendInstant(_stompSerializer.Serialize(message));
                StompState = StompConnectionState.Open;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Reconnect()
        {
            try
            {
                if (!_socket.IsRunning)
                {
                    await _socket.Start();
                }

                if (StompState != 0)
                {
                    StompMessage message = new StompMessage("CONNECT", _connectingHeaders);
                    await _socket.SendInstant(_stompSerializer.Serialize(message));
                    StompState = StompConnectionState.Open;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SendAsync(object body, string destination, IDictionary<string, string> headers)
        {
            try
            {
                string text = JsonConvert.SerializeObject(body);
                headers.Add("content-type", "application/json;charset=UTF-8");
                headers.Add("content-length", Encoding.UTF8.GetByteCount(text).ToString());
                await SendAsync(text, destination, headers);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SendAsync(string body, string destination, IDictionary<string, string> headers)
        {
            try
            {
                if (StompState != 0)
                {
                    await Reconnect();
                }

                headers.Add("destination", destination);
                StompMessage message = new StompMessage("SEND", body, headers);
                await _socket.SendInstant(_stompSerializer.Serialize(message));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SubscribeAsync<T>(string topic, IDictionary<string, string> headers, EventHandler<T> handler)
        {
            try
            {
                await SubscribeAsync(topic, headers, delegate (object? sender, StompMessage message)
                {
                    handler(this, JsonConvert.DeserializeObject<T>(message.Body));
                });

            }
            catch (Exception ex)
            {
                throw;
            }   
        }

        public async Task SubscribeAsync(string topic, IDictionary<string, string> headers, EventHandler<StompMessage> handler)
        {
            try
            {
                if (StompState != 0)
                {
                    await Reconnect();
                }

                headers.Add("destination", topic);
                headers.Add("id", $"sub-{_subscribers.Count}");
                StompMessage message = new StompMessage("SUBSCRIBE", headers);
                await _socket.SendInstant(_stompSerializer.Serialize(message));
                _subscribers.Add(topic, handler);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AckAsync(string id, string? transaction = null)
        {
            try
            {
                await Acknowledge(isPositive: true, id, transaction);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task NackAsync(string id, string? transaction = null)
        {
            try
            {
                await Acknowledge(isPositive: false, id, transaction);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DisconnectAsync()
        {
            try
            {
                StompMessage message = new StompMessage("DISCONNECT");
                await _socket.SendInstant(_stompSerializer.Serialize(message));
                StompState = StompConnectionState.Closed;
                _socket.Dispose();
                _subscribers.Clear();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                StompState = StompConnectionState.Closed;
                ((IDisposable)_socket).Dispose();
                _subscribers.Clear();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task Acknowledge(bool isPositive, string id, string? transaction = null)
        {
            try
            {
                if (StompState != 0)
                {
                    await Reconnect();
                }

                Dictionary<string, string> dictionary = new Dictionary<string, string> { { "id", id } };
                if (!string.IsNullOrEmpty(transaction))
                {
                    dictionary.Add("transaction", transaction);
                }

                StompMessage message = new StompMessage(isPositive ? "ACK" : "NACK", dictionary);
                await _socket.SendInstant(_stompSerializer.Serialize(message));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void HandleMessage(ResponseMessage messageEventArgs)
        {
            try
            {
                StompMessage stompMessage = _stompSerializer.Deserialize(messageEventArgs.Text);
                this.OnMessage?.Invoke(this, stompMessage);
                if (stompMessage.Command == "CONNECTED")
                {
                    this.OnConnect?.Invoke(this, stompMessage);
                }

                if (stompMessage.Command == "ERROR")
                {
                    this.OnError?.Invoke(this, stompMessage);
                }

                if (stompMessage.Headers.ContainsKey("destination"))
                {
                    _subscribers[stompMessage.Headers["destination"]](this, stompMessage);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}