using System.Diagnostics;
using System.Globalization;
using NLog;

namespace NKnife.TDMSDataViewer
{
    public sealed class Duration : IDisposable
    {
        private readonly string _context;
        private readonly Stopwatch _stopWatch;
        private readonly Logger _logger;

        private Duration(Logger logger, string context)
        {
            _context = context;
            _stopWatch = new Stopwatch();
            _logger = logger;

            _stopWatch.Start();
        }

        public static IDisposable Measure(Logger logger, string context, params object[] args)
        {
            if (args is { Length: > 0 })
            {
                context = string.Format(CultureInfo.InvariantCulture, context, args);
            }

            return new Duration(logger, context);
        }

        public void Dispose()
        {
            _stopWatch.Stop();
            _logger.Debug(CultureInfo.InvariantCulture, "{0}, duration = {1} ms", _context, _stopWatch.ElapsedMilliseconds);
        }
    }
}
