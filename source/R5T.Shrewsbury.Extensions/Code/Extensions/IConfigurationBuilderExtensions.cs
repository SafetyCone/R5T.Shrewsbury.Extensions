using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Lombardy;


namespace R5T.Shrewsbury.Extensions
{
    public static class IConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddAppSettingsDirectoryJsonFile(this IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider, string fileName, bool optional = false)
        {
            var appSettingsDirectoryPathProvider = configurationServiceProvider.GetRequiredService<IAppSettingsDirectoryPathProvider>();

            var appSettingsDirectoryPath = appSettingsDirectoryPathProvider.GetAppSettingsDirectoryPath();

            var stringlyTypedPathOperator = configurationServiceProvider.GetRequiredService<IStringlyTypedPathOperator>();

            var defaultAppSettingsJsonFilePath = stringlyTypedPathOperator.GetFilePath(appSettingsDirectoryPath, fileName);

            configurationBuilder.AddJsonFile(defaultAppSettingsJsonFilePath, optional);

            return configurationBuilder;
        }

        /// <summary>
        /// Adds the <see cref="FileNames.DefaultAppSettingsJsonFileName"/> appsettings JSON file to the configuration in a service-less way.
        /// Suitable for use in application configuration startup configuration, where no services are available while configuring the configuration.
        /// </summary>
        public static IConfigurationBuilder AddServiceLessDefaultAppSettingsJsonFile(this IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.AddJsonFile(FileNames.DefaultAppSettingsJsonFileName);

            return configurationBuilder;
        }

        /// <summary>
        /// Adds the <see cref="FileNames.DefaultAppSettingsJsonFileName"/> appsettings JSON file to the configuration in a service-less way.
        /// Suitable for use in application configuration startup configuration, where no services are available while configuring the configuration.
        /// Uses <see cref="IConfigurationBuilderExtensions.AddServiceLessDefaultAppSettingsJsonFile(IConfigurationBuilder)"/>.
        /// </summary>
        public static IConfigurationBuilder AddDefaultAppSettingsJsonFile(this IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.AddServiceLessDefaultAppSettingsJsonFile();

            return configurationBuilder;
        }

        /// <summary>
        /// Adds the default appsettings.json file using services from the configuration service provider.
        /// Suitable for use in application startup configuration, where services are available while configuring the configuration.
        /// </summary>
        public static IConfigurationBuilder AddServiceBasedDefaultAppSettingsJsonFile(this IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider)
        {
            var defaultAppSettingsJsonFileNameProvider = configurationServiceProvider.GetRequiredService<IDefaultAppSettingsJsonFileNameProvider>();

            var defaultAppSettingsJsonFileName = defaultAppSettingsJsonFileNameProvider.GetDefaultAppSettingsJsonFileName();

            configurationBuilder.AddAppSettingsDirectoryJsonFile(configurationServiceProvider, defaultAppSettingsJsonFileName);

            return configurationBuilder;
        }

        /// <summary>
        /// Adds the default appsettings.json file using services from the configuration service provider.
        /// Suitable for use in application startup configuration, where services are available while configuring the configuration.
        /// Uses <see cref="IConfigurationBuilderExtensions.AddServiceBasedDefaultAppSettingsJsonFile(IConfigurationBuilder, IServiceProvider)"/>.
        /// </summary>
        public static IConfigurationBuilder AddDefaultAppSettingsJsonFile(this IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider)
        {
            configurationBuilder.AddServiceBasedDefaultAppSettingsJsonFile(configurationServiceProvider);

            return configurationBuilder;
        }
    }
}
