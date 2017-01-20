# Novi.ElmahToSlack

A simple extension to elmah to send error notifications to Slack after writing to SQL Server.

## What's this about?

We decided we wanted to send ourselves Slack messages whenever our friend Elmah wrote to the SQL database. This little derivative will do just that.

## Configuration

Typically, your elmah configuration looks something like this:

    <elmah>
        <errorLog type="Elmah.SqlErrorLog, Elmah" connectionStringName="DefaultConnection" />		
    </elmah>

To use this version, you'll need a few more attributes on the errorLog element, and specify this assembly instead:

    <elmah>
        <errorLog type="Novi.ElmahToSlack.SqlPlusSlackErrorLog, Novi.ElmahToSlack" 
        connectionStringName="YourConnectionName" 
        webHookUri="https://hooks.slack.com/services/YourWebHookEndpoint" 
        userName="The user or bot you want to post under" 
        channel="the #channel to post to" />
    </elmah>

Check the [Slack documentation](https://api.slack.com/incoming-webhooks) for setting up a web hook.
