using Netina.Stomp.Client.Messages;
using Netina.Stomp.Client.Utils;
using System.Text;

namespace GMServer.Stomp.Messages
{
    public class StompMessageSerializer
    {
        public string Serialize(StompMessage message)
        {
            var buffer = new StringBuilder();

            buffer.Append(message.Command + "\n");

            if (message.Headers != null)
            {
                foreach (var header in message.Headers)
                {
                    buffer.Append(header.Key + ":" + header.Value + "\n");
                }
            }

            buffer.Append('\n');
            buffer.Append(message.Body);
            buffer.Append('\0');

            return buffer.ToString();
        }

        public StompMessage Deserialize(string message)
        {
            var headers = new Dictionary<string, string>();
            var command = "";
            var body = "";
            if (false == string.IsNullOrWhiteSpace(message))
            {
                using (var reader = new StringReader(message))
                {
                    command = reader.ReadLine();

                    var header = reader.ReadLine();
                    while (!string.IsNullOrEmpty(header))
                    {
                        var split = header.Split(':');
                        if (split.Length == 2) headers[split[0].Trim()] = split[1].Trim();
                        header = reader.ReadLine() ?? string.Empty;
                    }

                    body = reader.ReadToEnd().TrimEnd('\r', '\n', '\0');
                }
            }
            return new StompMessage(command ?? StompCommand.Connected, body, headers);
        }
    }

    public class StompMessage
    {
        public IDictionary<string, string> Headers { get; }
        public string Body { get; }
        public string Command { get; }

        public StompMessage(string command)
            : this(command, string.Empty)
        {
        }

        public StompMessage(string command, string body)
            : this(command, body, new Dictionary<string, string>())
        {
        }

        public StompMessage(string command, IDictionary<string, string> headers)
            : this(command, string.Empty, headers)
        {
        }

        public StompMessage(string command, string body, IDictionary<string, string> headers)
        {
            Command = command;
            Body = body;
            Headers = headers;
        }
    }
}
