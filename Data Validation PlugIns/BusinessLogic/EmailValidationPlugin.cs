using D365PlugIns.Entities;
using D365PlugIns.PluginBase;
using Microsoft.Xrm.Sdk;
using System;


namespace Dynamics_365_PlugIns.Data_Validation_Plugins.BusinessLogic
{ 
    public class EmailValidationPlugin : BasePlugin
    {
        public override void ExecutePluginLogic(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var entity = (Entity)context.InputParameters["Target"];

            if (entity.Contains(EntityConstants.CommonFields.Email) && !IsValidEmail(entity[EntityConstants.CommonFields.Email].ToString()))
            {
                throw new InvalidPluginExecutionException("Invalid Email Address.");
            }
        }

        private bool IsValidEmail(string email)
        {
            // Simple regex for email validation
            return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$");
        }
    }
}