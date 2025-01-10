using Microsoft.Xrm.Sdk;
using System;

namespace D365PlugIns.PluginBase
{
    public abstract class BasePlugin : IPlugin
    {
        public virtual void Execute(IServiceProvider serviceProvider)
        {
            var tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var pluginExecutionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            try
            {
                ExecutePluginLogic(serviceProvider);
            }
            catch (Exception ex)
            {
                tracingService.Trace("Error: {0}", ex.ToString());
                throw new InvalidPluginExecutionException("An error occurred in the plugin.", ex);
            }
        }

        public abstract void ExecutePluginLogic(IServiceProvider serviceProvider);
    }
}
