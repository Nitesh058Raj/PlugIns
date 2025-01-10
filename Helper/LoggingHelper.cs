using Microsoft.Xrm.Sdk;

namespace D365PlugIns.Helper
{
    public static class LoggingHelper
    {
        public static void Log(ITracingService tracingService, string message)
        {
            tracingService.Trace(message);
        }
    }
}

