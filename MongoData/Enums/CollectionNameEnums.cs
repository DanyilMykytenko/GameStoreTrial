using Ardalis.SmartEnum;

namespace MongoData.Enums
{
    public class CollectionNameEnums : SmartEnum<CollectionNameEnums>
    {
        public static readonly CollectionNameEnums Categories = new("categories", 0);
        public static readonly CollectionNameEnums Products = new("products", 1);
        public static readonly CollectionNameEnums Shippers = new("shippers", 2);
        public static readonly CollectionNameEnums Suppliers = new("suppliers", 3);
        public CollectionNameEnums(string name, int value) : base(name, value)
        {
        }
    }
}
