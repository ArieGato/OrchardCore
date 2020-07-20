using System.Threading.Tasks;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Markdown.Models;
using OrchardCore.RecipeMigrationIssue.Models;
using OrchardCore.Recipes.Services;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.Taxonomies.Settings;
using OrchardCore.Title.Models;

namespace OrchardCore.RecipeMigrationIssue
{
    public class Migrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IContentAliasManager _contentAliasManager;
        private readonly IRecipeMigrator _recipeMigrator;

        public Migrations(
            IRecipeMigrator recipeMigrator,
            IContentDefinitionManager contentDefinitionManager,
            IContentAliasManager contentAliasManager)
        {
            _recipeMigrator = recipeMigrator;
            _contentDefinitionManager = contentDefinitionManager;
            _contentAliasManager = contentAliasManager;
        }

        public async Task<int> CreateAsync()
        {
            await _recipeMigrator.ExecuteAsync("addmedia1.recipe.json", this);

            // Create the Custom ContentType
            _contentDefinitionManager.AlterTypeDefinition("Custom", builder => builder
                .Creatable()
                .Listable()
                .WithPart(nameof(TitlePart))
                .WithPart(nameof(MarkdownBodyPart))
                .WithPart(nameof(CustomPart)));

            await _recipeMigrator.ExecuteAsync("addarticle2.recipe.json", this);

            // ensure that the category taxonomy content item is present
            var categoriesContentItemId = await _contentAliasManager.GetContentItemIdAsync("alias:categories");
            if (string.IsNullOrEmpty(categoriesContentItemId))
            {
                // create the categories content item
                await _recipeMigrator.ExecuteAsync("addcategory.recipe.json", this);
                categoriesContentItemId = await _contentAliasManager.GetContentItemIdAsync("alias:categories");
            }

            // Add Category field to the Custom Part
            var taxonomyFieldSettings = new TaxonomyFieldSettings
            {
                TaxonomyContentItemId = categoriesContentItemId,
                LeavesOnly = true,
                Unique = true
            };

            _contentDefinitionManager.AlterPartDefinition(nameof(CustomPart), part =>
            {
                part
                    .WithField(nameof(CustomPart.Category), field => field
                        .OfType(nameof(TaxonomyField))
                        .WithDisplayName("Category")
                        .WithSettings(taxonomyFieldSettings))
                    .WithField(nameof(CustomPart.Header), field => field
                        .OfType(nameof(TextField))
                        .WithDisplayName(nameof(CustomPart.Header)));
            });

            await _recipeMigrator.ExecuteAsync("addmedia2.recipe.json", this);

            return 1;
        }
    }
}
