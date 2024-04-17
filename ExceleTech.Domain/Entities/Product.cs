using ExceleTech.Domain.Common;
using ExceleTech.Domain.Enums;
using ExceleTech.Domain.Primitives;
using System.Text.Json.Serialization;

namespace ExceleTech.Domain.Entities
{
    public class Product : Entity<Guid>
    {

        public string Name { get; private set; }
        public ProductCategory Category { get; private set; }

        public Sku Sku { get; private set; }
        public int PercentDiscount { get; private set; }
        public Money Price { get; private set; }
        public int TimesOrdered { get; private set; }
        public void SetOrderedTimes(int times)
        {
            if (times <= 0 || TimesOrdered > times) return;
            TimesOrdered += times - TimesOrdered;
        }
        private Product(string name, ProductCategory category, Money price, Sku sku) 
        {
            Name = name;
            Category = category;
            Price = price;
            Sku = sku;
        }
        public static Product Create(string name, ProductCategory category, Sku sku, Money price)
        {
            if (name == null || sku == null || price == null)
            {
                return null;
            }
            return new Product(name, category, price, sku);
        }
        public Product SetDiscount(int Percent)
        {
            if (Percent > 100)
            {
                PercentDiscount = 100;
            }
            else if (Percent < 0)
            {
                PercentDiscount = 0;
            }
            else
            {
                PercentDiscount = Percent;
            }
            return this;
        }


        [JsonConstructor]
        private Product() { }
    }
}
