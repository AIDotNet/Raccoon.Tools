namespace Raccoon.Tools.Dto;

public class ModelsDto
{
    public ChatModelsDto[] ChatModels { get; set; }
    public string CheckModel { get; set; }
    public bool Enabled { get; set; }
    public string Id { get; set; }
    public ModelList ModelList { get; set; }
    public string Name { get; set; }
}

public class ModelList
{
    public bool showModelFetcher { get; set; }
}