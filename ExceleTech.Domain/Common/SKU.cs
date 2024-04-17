using System.Text.Json.Serialization;

namespace ExceleTech.Domain.Common
{
    public record Sku
    {
        private const int DefaultLenght = 15;

        [JsonConstructor]
        private Sku() { }
        internal Sku(string value) => Value = value;
        public string Value { get; private set; }
        public static Sku? Create(string value)
        {
            if (value == null || value.Length != DefaultLenght)
            {
                return null;
            }
            return new Sku(value);
        }
    }
}
