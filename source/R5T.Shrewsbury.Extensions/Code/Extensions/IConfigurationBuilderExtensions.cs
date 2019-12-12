using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Lombardy;


namespace R5T.Shrewsbury
{
    public static class IConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddAppSettingsDirectoryJsonFile(this IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider, string fileName)
        {
            var appSettingsDirectoryPathProvider = configurationServiceProvider.GetRequiredService<IAppSettingsDirectoryPathProvider>();

            var appSettingsDirectoryPath = appSettingsDirectoryPathProvider.GetAppSettingsDirectoryPath();

            var stringlyTypedPathOperator = configurationServiceProvider.GetRequiredService<IStringlyTypedPathOperator>();

            var defaultAppSettingsJsonFilePath = stringlyTypedPathOperator.GetFilePath(appSettingsDirectoryPath, fileName);

            configurationBuilder.AddJsonFile(defaultAppSettingsJsonFilePath);

            return configurationBuilder;
        }

        public static IConfigurationBuilder AddDefautltAppSettingsJsonFile(this IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider)
        {
            var defaultAppSettingsJsonFileNameProvider = configurationServiceProvider.GetRequiredService<IDefaultAppSettingsJsonFileNameProvider>();

            var defaultAppSettingsJsonFileName = defaultAppSettingsJsonFileNameProvider.GetDefaultAppSettingsJsonFileName();

            configurationBuilder.AddAppSettingsDirectoryJsonFile(configurationServiceProvider, defaultAppSettingsJsonFileName);

            return configurationBuilder;
        }
    }
}
