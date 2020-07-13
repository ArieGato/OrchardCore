using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Taxonomies.Fields;

namespace OrchardCore.RecipeMigrationIssue.Models
{
    public class CustomPart : ContentPart
    {
        public TextField Header { get; set; }
        public TaxonomyField Category { get; set; }
    }
}
