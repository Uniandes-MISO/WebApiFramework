using System;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Redbridge.Diagnostics;
using Redbridge.WebApi.Filters;
using Redbridge.WebApi.Handlers;

namespace Redbridge.WebApi.Configuration
{
    public static class HttpConfigurationFilterExtensions
    {
        public static void InstallExceptionFilters(this HttpConfiguration configuration, ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            logger.WriteDebug("Installing exception filters...");
            configuration.Filters.Add(new ValidationExceptionFilter(logger));
            configuration.Filters.Add(new UnknownEntityExceptionFilter(logger));
            configuration.Filters.Add(new UserNotAuthenticatedExceptionFilter(logger));
            configuration.Filters.Add(new UserNotAuthorizedExceptionFilter(logger));
            configuration.Filters.Add(new LoggingExceptionFilter(logger));
            configuration.MessageHandlers.Add(new NotFoundCustomMessageHandler());

            logger.WriteDebug("Installing exception logger...");
            configuration.Services.Replace(typeof(IExceptionLogger), new UnhandledExceptionLogger(logger));
        }
    }
}
