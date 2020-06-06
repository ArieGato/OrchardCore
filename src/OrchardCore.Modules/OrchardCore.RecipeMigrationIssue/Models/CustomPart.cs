using OrchardCore.ContentManagement;
using OrchardCore.Taxonomies.Fields;

namespace OrchardCore.RecipeMigrationIssue.Models
{
    public class CustomPart : ContentPart
    {
        public TaxonomyField Category { get; set; }
    }
}
