using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Structure.Core.Entities
{
    public enum Condition
    {
        Used,
        New
    }

    public class Car  : BaseEntity
    {
        public string Make { get; init; } = String.Empty;
        public string Color { get; init; } = String.Empty;
        public uint Mileage { get; init; }
        public Condition Condition { get; init; }
    }
}
