namespace QualificationTask.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsResponse
    {
        public int Id { get; set; }
        public string ManufacturerCode { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
