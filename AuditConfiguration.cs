using Audit.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Audit.WebApi;
using Projects.Providers.Database;
using Audit.SqlServer.Providers;
using Audit.SqlServer;
using System.Collections.Generic;
using System;
using Projects.Interfaces;

namespace Projects
{
    public static class AuditConfiguration
    {
        private const string CorrelationIdField = "CorrelationId";

        /// <summary>
        /// Add the global audit filter to the MVC pipeline
        /// </summary>
        public static MvcOptions AddAudit(this MvcOptions mvcOptions)
        {
            // Configure the global Action Filter
            mvcOptions.AddAuditFilter(a => a
                    .LogAllActions()
                    .WithEventType("MVC:{verb}:{controller}:{action}")
                    .IncludeModelState()
                    .IncludeRequestBody()
                    .IncludeResponseBody());
            return mvcOptions;
        }

        /// <summary>
        /// Global Audit configuration
        /// </summary>
        public static IServiceCollection ConfigureAudit(this IServiceCollection serviceCollection, string connString)
        {
            #region Use this when you have another table for audit
            Audit.Core.Configuration.Setup()
                .UseEntityFramework(ef => ef.AuditTypeExplicitMapper(m => m
                    .Map<ValueEntity, Audit_ValueEntity>()
                    // add more .Map<TableEntity, Audit_TableEntity>()
                    .AuditEntityAction<IAudit>((evt, entry, auditEntity) =>
                    {
                        auditEntity.AuditDate = DateTime.UtcNow;
                        auditEntity.UserName = evt.Environment.UserName;
                        auditEntity.AuditAction = entry.Action; // Insert, Update, Delete
                    })));
            #endregion
            #region Add an Event table in the database
            //Audit.Core.Configuration.Setup()
            //    .UseSqlServer(config => config
            //        .ConnectionString(connString)
            //        .Schema("forumpur_tests")
            //        .TableName("Event")
            //        .IdColumnName("EventId")
            //        .JsonColumnName("JsonData")
            //        .LastUpdatedColumnName("LastUpdatedDate")
            //        .CustomColumn("EventType", ev => ev.EventType)
            //        .CustomColumn("User", ev => ev.Environment.UserName));

            // Entity framework audit output configuration
            //Audit.EntityFramework.Configuration.Setup()
            //    .ForContext<MyContext>(_ => _
            //        .AuditEventType("EF:{context}"))
            //    .UseOptOut();
            #endregion
            #region  Save changes on files
            //Audit.Core.Configuration.Setup()
            //    .UseFileLogProvider(_ => _
            //        .Directory(@"C:\Temp")
            //        .FilenameBuilder(ev => $"{ev.StartDate:yyyyMMddHHmmssffff}_{ev.CustomFields[CorrelationIdField]?.ToString().Replace(':', '_')}.json"))
            //    .WithCreationPolicy(EventCreationPolicy.InsertOnEnd);
            #endregion

            return serviceCollection;
        }

        public static void UseAuditMiddleware(this IApplicationBuilder app)
        {
            // Configure the Middleware
            app.UseAuditMiddleware(_ => _
                .FilterByRequest(r => !r.Path.Value.EndsWith("favicon.ico"))
                .IncludeHeaders()
                .IncludeRequestBody()
                .IncludeResponseBody()
                .WithEventType("HTTP:{verb}:{url}"));
        }

        /// <summary>
        /// Add a RequestId so the audit events can be grouped per request
        /// </summary>
        public static void UseAuditCorrelationId(this IApplicationBuilder app, IHttpContextAccessor ctxAccesor)
        {
            Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
            {
                var httpContext = ctxAccesor.HttpContext;
                scope.Event.CustomFields[CorrelationIdField] = httpContext.TraceIdentifier;
            });
        }
    }
}
