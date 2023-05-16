using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using CleanArchitecture.Application.Dtos;

namespace CleanArchitecture.IntegrationTest.Api.Controllers;

public sealed class ProductControllerTests
{
    [Fact]
    public async Task Create_WithValidProperties_ShouldCreateProduct()
    {
        // Arrange
        var apiFactory = new DefaultWebApplicationFactory();
        HttpClient client = apiFactory.CreateClient();
        var addProductDto = new AddProductRequestDto(
                Ean: "978�014102662",
                Sku: "TSH-FF0000-L",
                Name: "Test Product",
                Description: "Test Product Description"
                );

        // Act

        HttpResponseMessage response = await client.PostAsJsonAsync(ApiAddress.Product, addProductDto);
        var product = await response.Content.ReadFromJsonAsync<ProductDto>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        product!.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Get_WithValidId_ShouldReturnProduct()
    {
        // Arrange
        var apiFactory = new DefaultWebApplicationFactory();
        HttpClient client = apiFactory.CreateClient();
        var addProductDto = new AddProductRequestDto(
            Ean: "978�014102662",
            Sku: "TSH-FF0000-L",
            Name: "Test Product",
            Description: "Test Product Description"
        );
        HttpResponseMessage response = await client.PostAsJsonAsync(ApiAddress.Product, addProductDto);
        var addedProduct = await response.Content.ReadFromJsonAsync<ProductDto>();

        // Act

        var product = await client.GetFromJsonAsync<ProductDto>($"{ApiAddress.Product}/{addedProduct!.Id}");

        // Assert
        product!.Id.Should().Be(addedProduct.Id);
    }

    [Fact]
    public async Task Get_WithInValidId_ShouldReturnNotFound()
    {
        // Arrange
        var apiFactory = new DefaultWebApplicationFactory();
        HttpClient client = apiFactory.CreateClient();

        // Act
        var response = await client.GetAsync($"{ApiAddress.Product}/1022222");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }


    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public async Task Create_WithInvalidName_ReturnsBadRequest(string productName)
    {
        // Arrange
        var apiFactory = new DefaultWebApplicationFactory();
        HttpClient client = apiFactory.CreateClient();
        var addProductDto = new AddProductRequestDto(
            Ean: "978�014102662",
            Sku: "TSH-FF0000-L",
            Name: productName,
            Description: "Test Product Description"
        );

        // Act
        HttpResponseMessage createProductResponse =
            await client.PostAsJsonAsync(ApiAddress.Product, addProductDto);

        // Assert
        createProductResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Update_WithValidProperties_ShouldUpdateProduct()
    {
        // Arrange
        var apiFactory = new DefaultWebApplicationFactory();
        HttpClient client = apiFactory.CreateClient();

        var addProductDto = new AddProductRequestDto(
            Ean: "978�014102662",
            Sku: "TSH-FF0000-L",
            Name: "Test Product",
            Description: "Test Product Description"
        );
        HttpResponseMessage createProductResponse = await client.PostAsJsonAsync(ApiAddress.Product, addProductDto);
        var createdProduct = await createProductResponse.Content.ReadFromJsonAsync<ProductDto>();

        var updateProductDto = new UpdateProductRequestDto(
            Ean: "978�014102662",
            Sku: "TSH-FF0000-M",
            Name: "Updated Product",
            Description: "Test Product Description"
        );
        // Act

        HttpResponseMessage response = await client.PutAsJsonAsync($"{ApiAddress.Product}/{createdProduct!.Id}", updateProductDto);
        var updatedProduct = await client.GetFromJsonAsync<ProductDto>($"{ApiAddress.Product}/{createdProduct!.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        updatedProduct!.Id.Should().Be(createdProduct!.Id);
        updatedProduct!.Name.Should().Be(updateProductDto.Name);
        updatedProduct!.Sku.Should().Be(updateProductDto.Sku);
    }
}