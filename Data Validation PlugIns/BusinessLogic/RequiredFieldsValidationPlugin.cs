using D365PlugIns.Entities;
using D365PlugIns.PluginBase;
using Microsoft.Xrm.Sdk;
using System;

namespace Dynamics_365_PlugIns.Data_Validation_Plugins.BusinessLogic
{
    public class RequiredFieldsValidationPlugin : BasePlugin
    {
        public override void ExecutePluginLogic(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var entity = (Entity)context.InputParameters["Target"];

            foreach (var field in EntityConstants.RequiredFields.Fields)
            {
                ValidateRequiredField(entity, field);
            }
        }

        private void ValidateRequiredField(Entity entity, string fieldName)
        {
            // Check if field exists and if it's not empty or null
            if (!entity.Contains(fieldName) || string.IsNullOrWhiteSpace(entity[fieldName]?.ToString()))
            {
                throw new InvalidPluginExecutionException($"The field '{fieldName}' is required and cannot be empty.");
            }
        }
    }
}
