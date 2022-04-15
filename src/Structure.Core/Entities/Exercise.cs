namespace Structure.Core.Entities
{
    public class Exercise : BaseEntity
    {
        public string Name { get; init; } = String.Empty;
        public HashSet<string> Aliases { get; init; } = new();
        public Uri? Image { get; set; }
        public Uri? Demo { get; set; }
    }
}