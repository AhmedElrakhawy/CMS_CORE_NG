using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog.Sinks.SystemConsole.Themes;

namespace LoggingService
{
    public class CustomConsoleTheme
    {
        private readonly IReadOnlyDictionary<ConsoleThemeStyle, string> _styles;

        public CustomConsoleTheme(IReadOnlyDictionary<ConsoleThemeStyle, string> styles)
        {
            if (styles == null)
            {
                throw new ArgumentNullException(nameof(styles));
            }

            this._styles = styles.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static CustomConsoleTheme VisualStudioLight { get; } = CustomConsoleThemes.VisualStudioLight;

    }
}
