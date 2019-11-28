using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Framework.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Sitecore.Support.Pipelines.Blocks
{
    public class ImportRelationshipDefinitionsBlock : Sitecore.Commerce.Plugin.Catalog.ImportRelationshipDefinitionsBlock
    {
        public ImportRelationshipDefinitionsBlock(ICreateRelationshipDefinitionPipeline createRelationshipDefinitionPipeline, IEditRelationshiDefinitionPipeline editRelationshiDefinitionPipeline) : base(createRelationshipDefinitionPipeline, editRelationshiDefinitionPipeline)
        {
        }

        protected override async Task ImportSingleEntity(ImportCatalogsArgument arg, CommercePipelineExecutionContext context, RelationshipDefinition entity)
        {
            Condition.Requires(arg, "arg").IsNotNull();
            Condition.Requires(context, "context").IsNotNull();
            Condition.Requires(entity, "entity").IsNotNull();
            CreateRelationshipDefinitionArgument arg2 = new CreateRelationshipDefinitionArgument(entity.Name, string.IsNullOrWhiteSpace(entity.DisplayName) ? entity.Name : entity.DisplayName, string.IsNullOrWhiteSpace(entity.RelationshipDescription) ? entity.Name : entity.RelationshipDescription, entity.SourceType, entity.TargetType, entity.RenderList);

            // Pass new instance of pipeline context to CreateRelationshipDefinitionPipeline so if it aborts the import continues
            await CreateRelationshipDefinitionPipeline.Run(arg2, context.CommerceContext.GetPipelineContext()).ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}