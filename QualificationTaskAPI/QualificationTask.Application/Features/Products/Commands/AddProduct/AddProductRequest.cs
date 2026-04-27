namespace QualificationTask.Application.Features.Products.Commands.AddProduct
{
    public class AddProductRequest
    {
        public string ManufacturerCode { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
