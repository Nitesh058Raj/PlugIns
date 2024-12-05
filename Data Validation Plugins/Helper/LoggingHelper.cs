using Microsoft.Xrm.Sdk;

namespace Data_Validation_Plugins.Helper
{
    public static class LoggingHelper
    {
        public static void Log(ITracingService tracingService, string message)
        {
            tracingService.Trace(message);
        }
    }
}

