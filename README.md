# Audit.NET WebApi

How to implement an efficient _audit_ system with [C#](https://www.puresourcecode.com/category/dotnet/csharp/) and [Entity Framework Core](https://www.puresourcecode.com/tag/entity-framework-core/)? I have talked about Entity Framework in my previous posts:

*   [Creating A Model For An Existing Database](https://www.puresourcecode.com/dotnet/net-core/creating-a-model-for-an-existing-database-in-entity-framework-core/)
*   [Entity Framework Core And Calling A Stored Procedure](https://www.puresourcecode.com/dotnet/net-core/entity-framework-core-and-calling-a-stored-procedure/)
*   [Transactions With Entity Framework Core](https://www.puresourcecode.com/dotnet/csharp/transactions-with-entity-framework-core/)
*   [Database Connection Resiliency In Entity Framework Core](https://www.puresourcecode.com/dotnet/net-core/database-connection-resiliency-in-entity-framework-core/)
*   [Database Connection Resiliency In Entity Framework Core: Update](https://www.puresourcecode.com/dotnet/net-core/database-connection-resiliency-in-entity-framework-core-update/)

Often, during my implementation, there is a common request: track all changes in the database and where and who is changing a record.

A way to implement an audit is to override the `SubmitChanges` function in your data context. I created a post about it a long time ago; it was 2014\. The post has title [Log record changes in SQL server in an audit table](https://www.puresourcecode.com/news/apps/log-record-changes-in-sql-server-in-an-audit-table/) and I was using Entity Framework.

So, I googled a bit and I found a very nice NuGet package that can help us to implement what we need. [Audit.NET](https://github.com/thepirat000/Audit.NET) is the package and it is pretty complete. Although the documentation is good, I was struggle to understand how to use it in my project. For this reason, I want to give you all my thoughts and the final solution. So, you can use it.

For the full explanation of this code, read [Audit with Entity Framework Core](https://www.puresourcecode.com/dotnet/net-core/audit-with-entity-framework-core/)

## API samples

Description | Command
------------ | -------------- 
**Get all records** | ```curl -X GET http://localhost:50732/api/values -H "content-type: application/json"```
**Get record** | ```curl -X GET http://localhost:50732/api/values/1 -H "content-type: application/json"```
**Insert record** | ```curl -X POST http://localhost:50732/api/values -H "content-type: application/json" -d '"Some description"'```
**Update record** | ```curl -X PUT http://localhost:50732/api/values/1 -H "content-type: application/json" -d '"New description"'```
**Delete record** | ```curl -X DELETE http://localhost:50732/api/values/1 -H "content-type: application/json"```
**Delete multiple records** | ```curl -X DELETE http://localhost:50732/api/values/delete -H "content-type: application/json" -d '"2,3,4"'```
**Get all records** | ```curl -X GET http://localhost:50732/api/contacts -H "content-type: application/json"```
**Get record** | ```curl -X GET http://localhost:50732/api/contacts/1 -H "content-type: application/json"```
**Insert record** | ```curl -X POST http://localhost:50732/api/contacts -H "content-type: application/json" -d '"Some description"'```
**Update record** | ```curl -X PUT http://localhost:50732/api/contacts/1 -H "content-type: application/json" -d '"New description"'```
**Delete record** | ```curl -X DELETE http://localhost:50732/api/contacts/1 -H "content-type: application/json"```
**Delete multiple records** | ```curl -X DELETE http://localhost:50732/api/contacts/delete -H "content-type: application/json" -d '"2,3,4"'```

If you have any question, please use my [forum](https://www.puresourcecode.com/forum/) and visit my [blog](https://www.puresourcecode.com).
