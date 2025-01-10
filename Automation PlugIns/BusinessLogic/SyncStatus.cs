using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D365PlugIns.PluginBase;
using Microsoft.Xrm.Sdk;
using D365PlugIns.Entities;
using Microsoft.Xrm.Sdk.Query;

namespace Dynamics_365_PlugIns.Automation_PlugIns.BusinessLogic
{
    public class SyncStatus : BasePlugin
    {
        public override void ExecutePluginLogic(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var entity = (Entity)context.InputParameters["Target"];     // Account in this case

            var preImage = (Entity)context.PreEntityImages["PreImage"];
            var oldStatus = preImage?.GetAttributeValue<OptionSetValue>(EntityConstants.CommonFields.StatusCode)?.Value;
            var newStatus = entity?.GetAttributeValue<OptionSetValue>(EntityConstants.CommonFields.StatusCode)?.Value;
            

            if (oldStatus == newStatus)
            {
                return;
            }

            var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = serviceFactory.CreateOrganizationService(context.UserId);

            const int StatusInactive = 2; // 2 == "deactive"

            if (entity != null  && entity.Contains(EntityConstants.CommonFields.StatusCode) && entity.GetAttributeValue<OptionSetValue>(EntityConstants.CommonFields.StatusCode).Value == StatusInactive) 
            {
                Guid accountId = entity.Id;

                var contacts = RetrieveRelatedRecords(EntityConstants.ContactLogicalName, EntityConstants.ContactFields.Account, accountId, service);

                foreach(var contact in contacts.Entities)
                {
                    contact[EntityConstants.CommonFields.StatusCode] = new OptionSetValue(StatusInactive); 
                    service.Update(contact);
                }

                var opportunities = RetrieveRelatedRecords(EntityConstants.OpportunityLogicalName, EntityConstants.OpportunityFields.Account, accountId, service);

                foreach(var opportunity in opportunities.Entities)
                {
                    opportunity[EntityConstants.CommonFields.StatusCode] = new OptionSetValue(StatusInactive); 
                    service.Update(opportunity);
                }            
            }
        }

        private EntityCollection RetrieveRelatedRecords(string entityName, string relationshipField, Guid accountId, IOrganizationService service)
        {
            var query = new QueryExpression(entityName)
            {
                ColumnSet = new ColumnSet("statecode"), // Retrieve only necessary fields
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression(relationshipField, ConditionOperator.Equal, accountId)
                    }
                }
            };

            return service.RetrieveMultiple(query);
        }
    }
}
