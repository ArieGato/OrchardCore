using OrchardCore.Modules.Manifest;

[assembly: Module(
    Id = "OrchardCore.RecipeMigrationIssue",
    Name = "Recipe Migration Issue",
    Author = "Arjan Vermunt",
    Version = "1.0.0",
    Description = "Recipe Migration Issue Repo",
    Category = "Issues",
    Dependencies = new[]
    {
        "OrchardCore.ContentFields",
        "OrchardCore.Contents",
        "OrchardCore.Markdown",
        "OrchardCore.Taxonomies",
        "OrchardCore.Title"
    }
)]
