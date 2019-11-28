using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;

namespace Sitecore.Support
{
    public class ConfigureSitecore : IConfigureSitecore
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);

            services.Sitecore().Pipelines(config => config

             .ConfigurePipeline<IImportCatalogsPipeline>(configure =>
                configure.Replace<ImportRelationshipDefinitionsBlock, Sitecore.Support.Pipelines.Blocks.ImportRelationshipDefinitionsBlock>()));


            services.RegisterAllCommands(assembly);
        }
    }
}