using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QualificationTask.Application.Features.Products.Commands.AddProduct;
using QualificationTask.Application.Features.Products.Queries.GetProducts;
using QualificationTask.Domain.Entities;
using QualificationTask.Infrastructure.Persistance;
using System.Net.Http.Json;
using Tests.Integration.Core;

namespace Features.Products.Tests
{
    public class FeatureProductsIntegrationTests : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly QualificationTaskDbContext dbContext;

        public FeatureProductsIntegrationTests(IntegrationTestWebAppFactory factory)
        {
            _httpClient = factory.CreateClient();

            var scope = factory.Services.CreateScope();
            dbContext = scope.ServiceProvider.GetRequiredService<QualificationTaskDbContext>();
        }

        [Fact]
        public async Task GetProducts_WhenCalled_ShouldReturnOkResult()
        {
            //act
            var response = await _httpClient.GetAsync("/api/Products");

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetProducts_WhenProductsExistInDatabase_ShouldReturnProductList()
        {
            //arrange
            var newProducts = new List<Product>
            {
                new Product
                {
                    ManufacturerCode = "EAN0000001",
                    Name = "Testowy produkt",
                    Price = 10,
                    Quantity = 1,
                },
                new Product
                {
                    ManufacturerCode = "EAN0000002",
                    Name = "Testowy produkt2",
                    Price = 20,
                    Quantity = 2,
                },
            };

            await dbContext.Products.AddRangeAsync(newProducts);
            await dbContext.SaveChangesAsync();

            //act
            var response = await _httpClient.GetAsync("/api/products");

            //assert
            var content = await response.Content.ReadFromJsonAsync<List<GetProductsResponse>>();
            content.Should().HaveCountGreaterThan(1);
        }

        [Fact]
        public async Task AddProduct_WhenSuccessful_ShouldReturnCreatedResult()
        {
            //arrange
            var request = new AddProductRequest
            {
                ManufacturerCode = "EAN0000001",
                Name = "Testowy produkt",
                Price = 10,
                Quantity = 1,
            };

            //act
            var response = await _httpClient.PostAsJsonAsync("/api/products", request);

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task AddProduct_WhenSuccessful_ShouldCreateProductInDatabase()
        {
            //arrange
            var request = new AddProductRequest
            {
                ManufacturerCode = "EAN0000001",
                Name = "Testowy produkt",
                Price = 10,
                Quantity = 1,
            };

            //act
            var response = await _httpClient.PostAsJsonAsync("/api/products", request);

            //assert
            var newProductId = await response.Content.ReadFromJsonAsync<int>();

            var newProduct = await dbContext.Products.FirstAsync(x => x.Id == newProductId);
            newProduct.Should().NotBeNull();
        }
    }
}
