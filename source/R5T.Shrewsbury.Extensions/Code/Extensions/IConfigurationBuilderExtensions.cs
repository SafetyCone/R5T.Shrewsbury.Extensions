using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace R5T.Shrewsbury.Extensions
{
    public static class IConfigurationBuilderExtensions
    {
        /// <summary>
        /// Adds the <see cref="FileNames.DefaultAppSettingsJsonFileName"/> appsettings JSON file to the configuration in a service-less way by assuming the appsettings.json file is in the current directory.
        /// Suitable for use in application configuration startup configuration, where no services are available while configuring the configuration.
        /// </summary>
        public static IConfigurationBuilder AddServiceLessDefaultAppSettingsJsonFile(this IConfigurationBuilder configurationBuilder, bool optional = true)
        {
            configurationBuilder.AddJsonFile(FileNames.DefaultAppSettingsJsonFileName, optional); // Add the filename, assuming the appsettings.json file is in the current directory.

            return configurationBuilder;
        }

        /// <summary>
        /// Adds the <see cref="FileNames.DefaultAppSettingsJsonFileName"/> appsettings JSON file to the configuration in a service-less way.
        /// Suitable for use when no services are available, for example while configuring the an initial configuration.
        /// Uses <see cref="IConfigurationBuilderExtensions.AddServiceLessDefaultAppSettingsJsonFile(IConfigurationBuilder, bool)"/>.
        /// </summary>
        public static IConfigurationBuilder AddDefaultAppSettingsJsonFile(this IConfigurationBuilder configurationBuilder, bool optional = true)
        {
            configurationBuilder.AddServiceLessDefaultAppSettingsJsonFile(optional);

            return configurationBuilder;
        }

        /// <summary>
        /// Adds the default appsettings.json file using services from the configuration service provider.
        /// Suitable for use in application startup configuration, where services are available while configuring the configuration.
        /// Uses the <see cref="IDefaultAppSettingsJsonFilePathProvider"/> service.
        /// </summary>
        public static IConfigurationBuilder AddServiceBasedDefaultAppSettingsJsonFile(this IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider, bool optional = true)
        {
            var defaultAppSettingsJsonFilePathProvider = configurationServiceProvider.GetRequiredService<IDefaultAppSettingsJsonFilePathProvider>();

            var defaultAppSettingsJsonFilePath = defaultAppSettingsJsonFilePathProvider.GetDefaultAppSettingsJsonFilePath();

            configurationBuilder.AddJsonFile(defaultAppSettingsJsonFilePath, optional);

            return configurationBuilder;
        }

        /// <summary>
        /// Adds the default appsettings.json file using services from the configuration service provider.
        /// Suitable for use in application startup configuration, where services are available while configuring the configuration.
        /// Uses <see cref="IConfigurationBuilderExtensions.AddServiceBasedDefaultAppSettingsJsonFile(IConfigurationBuilder, IServiceProvider, bool)"/>.
        /// </summary>
        public static IConfigurationBuilder AddDefaultAppSettingsJsonFile(this IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider, bool optional = true)
        {
            configurationBuilder.AddServiceBasedDefaultAppSettingsJsonFile(configurationServiceProvider);

            return configurationBuilder;
        }

        /// <summary>
        /// Adds the configuration name-specific appsettings.{Configuration Name}.json file using the <see cref="IConfigurationNameSpecificAppSettingsJsonFilePathProvider"/> service.
        /// </summary>
        public static IConfigurationBuilder AddConfigurationSpecificAppSettingsJsonFile(this IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider, bool optional = false)
        {
            var configurationNameSpecificAppSettingsJsonFilePathProvider = configurationServiceProvider.GetRequiredService<IConfigurationNameSpecificAppSettingsJsonFilePathProvider>();

            var configurationNameSpecificAppSettingsJsonFilePath = configurationNameSpecificAppSettingsJsonFilePathProvider.GetConfigurationNameSpecificAppSettingsJsonFilePath();

            configurationBuilder.AddJsonFile(configurationNameSpecificAppSettingsJsonFilePath, optional);

            return configurationBuilder;
        }

        /// <summary>
        /// Adds the default and configuration name-specific appsettings.json files to the configuration.
        /// Defaults make the default appsettings.json file non-optional, and the configuration name-specific appsettings file optional since all configuration might just be in the default appsettings file.
        /// </summary>
        public static IConfigurationBuilder AddDefaultAndConfigurationSpecificAppSettingsJsonFiles(this IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider, bool defaultIsOptional = false, bool configurationSpecificIsOptional = true)
        {
            configurationBuilder
                .AddDefaultAppSettingsJsonFile(configurationServiceProvider, defaultIsOptional)
                .AddConfigurationSpecificAppSettingsJsonFile(configurationServiceProvider, configurationSpecificIsOptional)
                ;

            return configurationBuilder;
        }
    }
}
