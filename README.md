# Dynamics365 Plugins

## Data Validation Plugins
- EmailValidationPlugIn
    > Simple email validation using regex.
- RequiredFieldsValidationPlugin
    > Checks for reqired fields and show custom error popup notification

## Automation PlugIns
- SysncStatus:
  > When Account record gets dective (status to deactive) then, related opportunities and contacts records's status will be deactive too.
- UpdateTotalRevenue
  > Calculate and set's the new total revenue for an account when opportunity is considered as won. (status changes to won)

# Contributing

I welcome contributions from the community. If you'd like to contribute to the project, please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Make your changes and commit them with below formate.
4. Push your changes to your fork.
5. Submit a pull request to the main repository.

### Git Commit Format:

<i>commit_msg</i> + ":" + `$(date +%a), $(date +%b) $(date +%d), $(date +%Y)` + ";"

<b>Example:</b> Made Some changes:  Fri, May 31, 2024;
