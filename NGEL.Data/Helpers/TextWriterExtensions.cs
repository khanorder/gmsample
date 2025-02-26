using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGEL.Data.Helpers
{
    public static class TextWriterExtensions
    {
        const string DefaultForegroundColor = "\x1B[39m\x1B[22m";
        const string DefaultBackgroundColor = "\x1B[49m";

        public static void LogDebug<T>(this ILogger<T> logger, string message, ConsoleColor foreground)
        {
            logger.LogDebug($"{GetForegroundColorEscapeCode(foreground)}{message}{DefaultForegroundColor}");
        }

        public static void LogDebug<T>(this ILogger<T> logger, string message, ConsoleColor foreground, ConsoleColor background)
        {
            logger.LogDebug($"{GetForegroundColorEscapeCode(foreground)}{GetBackgroundColorEscapeCode(background)}{message}{DefaultForegroundColor}{DefaultBackgroundColor}");
        }

        public static void LogInformation<T>(this ILogger<T> logger, string message, ConsoleColor foreground)
        {
            logger.LogInformation($"{GetForegroundColorEscapeCode(foreground)}{message}{DefaultForegroundColor}");
        }

        public static void LogInformation<T>(this ILogger<T> logger, string message, ConsoleColor foreground, ConsoleColor background)
        {
            logger.LogInformation($"{GetForegroundColorEscapeCode(foreground)}{GetBackgroundColorEscapeCode(background)}{message}{DefaultForegroundColor}{DefaultBackgroundColor}");
        }

        public static void LogWarning<T>(this ILogger<T> logger, string message, ConsoleColor foreground)
        {
            logger.LogWarning($"{GetForegroundColorEscapeCode(foreground)}{message}{DefaultForegroundColor}");
        }

        public static void LogWarning<T>(this ILogger<T> logger, string message, ConsoleColor foreground, ConsoleColor background)
        {
            logger.LogWarning($"{GetForegroundColorEscapeCode(foreground)}{GetBackgroundColorEscapeCode(background)}{message}{DefaultForegroundColor}{DefaultBackgroundColor}");
        }

        public static void LogError<T>(this ILogger<T> logger, string message, ConsoleColor foreground)
        {
            logger.LogError($"{GetForegroundColorEscapeCode(foreground)}{message}{DefaultForegroundColor}");
        }

        public static void LogError<T>(this ILogger<T> logger, string message, ConsoleColor foreground, ConsoleColor background)
        {
            logger.LogError($"{GetForegroundColorEscapeCode(foreground)}{GetBackgroundColorEscapeCode(background)}{message}{DefaultForegroundColor}{DefaultBackgroundColor}");
        }

        public static void WriteWithColor(this TextWriter textWriter, string message, ConsoleColor? background, ConsoleColor? foreground)
        {
            // Order:
            //   1. background color
            //   2. foreground color
            //   3. message
            //   4. reset foreground color
            //   5. reset background color

            var backgroundColor = background.HasValue ? GetBackgroundColorEscapeCode(background.Value) : null;
            var foregroundColor = foreground.HasValue ? GetForegroundColorEscapeCode(foreground.Value) : null;

            if (backgroundColor != null)
            {
                textWriter.Write(backgroundColor);
            }
            if (foregroundColor != null)
            {
                textWriter.Write(foregroundColor);
            }

            textWriter.WriteLine(message);

            if (foregroundColor != null)
            {
                textWriter.Write(DefaultForegroundColor);
            }
            if (backgroundColor != null)
            {
                textWriter.Write(DefaultBackgroundColor);
            }
        }

        static string GetForegroundColorEscapeCode(ConsoleColor color) => color switch
        {
            ConsoleColor.Black => "\x1B[30m",
            ConsoleColor.DarkRed => "\x1B[31m",
            ConsoleColor.DarkGreen => "\x1B[32m",
            ConsoleColor.DarkYellow => "\x1B[33m",
            ConsoleColor.DarkBlue => "\x1B[34m",
            ConsoleColor.DarkMagenta => "\x1B[35m",
            ConsoleColor.DarkCyan => "\x1B[36m",
            ConsoleColor.Gray => "\x1B[37m",
            ConsoleColor.Red => "\x1B[1m\x1B[31m",
            ConsoleColor.Green => "\x1B[1m\x1B[32m",
            ConsoleColor.Yellow => "\x1B[1m\x1B[33m",
            ConsoleColor.Blue => "\x1B[1m\x1B[34m",
            ConsoleColor.Magenta => "\x1B[1m\x1B[35m",
            ConsoleColor.Cyan => "\x1B[1m\x1B[36m",
            ConsoleColor.White => "\x1B[1m\x1B[37m",

            _ => DefaultForegroundColor
        };

        static string GetBackgroundColorEscapeCode(ConsoleColor color) => color switch
        {
            ConsoleColor.Black => "\x1B[40m",
            ConsoleColor.DarkRed => "\x1B[41m",
            ConsoleColor.DarkGreen => "\x1B[42m",
            ConsoleColor.DarkYellow => "\x1B[43m",
            ConsoleColor.DarkBlue => "\x1B[44m",
            ConsoleColor.DarkMagenta => "\x1B[45m",
            ConsoleColor.DarkCyan => "\x1B[46m",
            ConsoleColor.Gray => "\x1B[47m",
            ConsoleColor.Red => "\x1B[1m\x1B[41m",
            ConsoleColor.Green => "\x1B[1m\x1B[42m",
            ConsoleColor.Yellow => "\x1B[1m\x1B[43m",
            ConsoleColor.Blue => "\x1B[1m\x1B[44m",
            ConsoleColor.Magenta => "\x1B[1m\x1B[45m",
            ConsoleColor.Cyan => "\x1B[1m\x1B[46m",
            ConsoleColor.White => "\x1B[1m\x1B[47m",

            _ => DefaultBackgroundColor
        };
    }
}