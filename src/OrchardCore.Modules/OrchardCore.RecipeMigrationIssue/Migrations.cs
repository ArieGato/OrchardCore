using System.Threading.Tasks;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using OrchardCore.Title.Models;

namespace OrchardCore.RecipeMigrationIssue
{
    public class Migrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IRecipeMigrator _recipeMigrator;

        public Migrations(
            IRecipeMigrator recipeMigrator,
            IContentDefinitionManager contentDefinitionManager)
        {
            _recipeMigrator = recipeMigrator;
            _contentDefinitionManager = contentDefinitionManager;
        }

        public async Task<int> CreateAsync()
        {
            // create the article2 content item
            await _recipeMigrator.ExecuteAsync("migration.recipe.json", this);

            // Create the Custom ContentType
            _contentDefinitionManager.AlterTypeDefinition("Custom", builder => builder
                .Creatable()
                .Listable()
                .WithPart(nameof(TitlePart))
            );

            return 1;
        }
    }
}
