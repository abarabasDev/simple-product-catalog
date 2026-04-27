namespace QualificationTask.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ManufacturerCode { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
