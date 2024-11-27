namespace NKnife.TDMSDataViewer
{
    public static class Constants
    {
        public static readonly TimeSpan Heartbeat = TimeSpan.FromSeconds(5);
        public static readonly TimeSpan UiFreeze = TimeSpan.FromMilliseconds(500);
        public static readonly TimeSpan UiFreezeTimer = TimeSpan.FromMilliseconds(333);

        public static readonly TimeSpan DiagnosticsLogInterval = TimeSpan.FromSeconds(1);
        public static readonly TimeSpan DiagnosticsIdleBuffer = TimeSpan.FromMilliseconds(666);
        public static readonly TimeSpan DiagnosticsCpuBuffer = TimeSpan.FromMilliseconds(666);
        public static readonly TimeSpan DiagnosticsSubscriptionDelay = TimeSpan.FromMilliseconds(1000);

        public const string DEFAULT_RPS_STRING = "Render: 00 RPS";
        public const string DEFAULT_CPU_STRING = "CPU: 00 %";
        public const string DEFAULT_MANAGED_MEMORY_STRING = "Managed Memory: 00 Mb";
        public const string DEFAULT_TOTAL_MEMORY_STRING = "Total Memory: 00 Mb";
    }
}
