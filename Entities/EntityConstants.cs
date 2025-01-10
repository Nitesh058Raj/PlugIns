namespace D365PlugIns.Entities
{
    public static class EntityConstants
    {
        // Define the entity logical names
        public const string ContactLogicalName = "contact";
        public const string AccountLogicalName = "account";
        public const string OpportunityLogicalName = "Opportunity";

        // Define the contact entity fields
        public static class ContactFields
        {
            public const string Email = "emailaddress1";
            public const string Phone = "telephone1";
            public const string Account = "parentcustomerid";
        }

        // Define the account entity fields
        public static class AccountFields
        {
            public const string Email = "emailaddress1";
            public const string Phone = "telephone1";
            public const string Revenue = "revenue";
        }

        // Define the account entity fields
        public static class OpportunityFields
        {
            public const string Account = "parentaccountid";
            public const string Revenue = "estimatedvalue";
        }

        // Define the common fields 
        public static class CommonFields
        {
            public const string CreatedOn = "created";
            public const string ModifiedOn = "modified";
            public const string CreatedBy = "createdby";
            public const string ModifiedBy = "modifiedby";
            public const string Email = "emailaddress1";
            public const string StatusCode = "statuscode";
        }

        // Define the required fields for the RequiredFieldsValidationPlugin.cs
        public static class RequiredFields
        {
            public static readonly string[] Fields =
            {
                "firstname",
                "lastname",
                "emailaddress1"
                // Add any other required fields here
            };
        }
    }
}
