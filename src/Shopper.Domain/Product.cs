namespace Shopper.Domain;

public abstract class Base
{
}

public abstract  class BaseEntity : Base
{
    public int Id { get; set; }
}

public class Product : BaseEntity
{  
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; } = "https://via.placeholder.com/300x300";
    public decimal Price { get; set; }
    public decimal PriceDiscounted { get; set; }
    public Size? Size { get; set; }
}

public enum Size
{
    Small,
    Medium,
    Large,
}