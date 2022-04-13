using System.Text.Json.Serialization;

namespace Structure.Core.Entities
{
    public interface IEntity
    {
        public Guid Id { get; init; }
        public string Timestamp { get; init; }
        public DateTime CreatedUtc { get; init; }
        public DateTime UpdatedUtc { get; init; }
        public string PartitionKey { get; init; }
    }

    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        [JsonPropertyName("_ts")]
        public string Timestamp { get; init; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        public DateTime CreatedUtc { get; init; }
        public DateTime UpdatedUtc { get; init; }
        public string PartitionKey { get; init; }
    }
}
