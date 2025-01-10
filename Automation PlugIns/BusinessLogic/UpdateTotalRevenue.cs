using D365PlugIns.PluginBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using D365PlugIns.Entities;

namespace Dynamics_365_PlugIns.Automation_PlugIns.BusinessLogic
{
    class UpdateTotalRevenue: BasePlugin
    {
        public override void ExecutePluginLogic(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var entity = (Entity)context.InputParameters["Target"];     // Opportunity in this case

            var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = serviceFactory.CreateOrganizationService(context.UserId);

            // Ensure Target Entity Exists
            if ( entity != null && entity.LogicalName == EntityConstants.OpportunityLogicalName)
            {
                // Ensure Target Entity has statuscode
                if (entity.Contains(EntityConstants.CommonFields.StatusCode) && entity.GetAttributeValue<OptionSetValue>(EntityConstants.CommonFields.StatusCode)?.Value == 3)  // 3 == 'Won'
                {
                    Guid opportunityId = entity.Id;

                    var opportunity = service.Retrieve(EntityConstants.OpportunityLogicalName, opportunityId, new ColumnSet(EntityConstants.OpportunityFields.Account, EntityConstants.OpportunityFields.Revenue));

                    if (opportunity.Contains(EntityConstants.OpportunityFields.Account))
                    {
                        EntityReference accountRef = opportunity.GetAttributeValue<EntityReference>(EntityConstants.OpportunityFields.Account);

                        var account = service.Retrieve(EntityConstants.AccountLogicalName, accountRef.Id, new ColumnSet(EntityConstants.AccountFields.Revenue));

                        var totalRevenue = account.GetAttributeValue<Money>(EntityConstants.AccountFields.Revenue).Value + opportunity.GetAttributeValue<Money>(EntityConstants.OpportunityFields.Revenue).Value;

                        var accountUpdate = new Entity(EntityConstants.AccountLogicalName, accountRef.Id)
                        {
                            [EntityConstants.AccountFields.Revenue] = new Money(totalRevenue)
                        };

                        service.Update(accountUpdate);

                    }

                }

            }
        }
    }
}
