using System.Text.Json.Serialization;

namespace Structure.Core.Entities
{
    public interface IEntity
    {
        public Guid Id { get; init; }
        public string Timestamp { get; init; }
        public DateTime CreatedUtc { get; init; }
        public DateTime? UpdatedUtc { get; set; }
        public string PartitionKey { get; init; }
    }

    public interface ISoftDeleteEntity : IEntity
    {
        public bool Deleted { get; set; }
        public DateTime? DeletedUtc { get; set; }
    }

    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        [JsonPropertyName("_ts")]
        public string Timestamp { get; init; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

        public DateTime CreatedUtc { get; init; } = DateTime.UtcNow;
        public DateTime? UpdatedUtc { get; set; }
        public string PartitionKey { get; init; } = default!;
    }
}