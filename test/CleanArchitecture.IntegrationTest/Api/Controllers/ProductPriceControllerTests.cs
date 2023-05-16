using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using CleanArchitecture.Application.Dtos;

namespace CleanArchitecture.IntegrationTest.Api.Controllers;

public sealed class ProductPriceControllerTests
{
    [Fact]
    public async Task Create_WithValidProperties_ShouldCreatePriceForAProduct()
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
        var product = await response.Content.ReadFromJsonAsync<ProductDto>();

        var seller = (await client.GetFromJsonAsync<List<SellerDto>>(ApiAddress.Seller))!.First();
        var productPriceRequest = new AddProductPriceRequestDto(product!.Id, seller.Id, 12, DateOnly.FromDateTime(DateTime.Now));

        // Act
        HttpResponseMessage productPriceResponse =
            await client.PostAsJsonAsync(ApiAddress.ProductPrice, productPriceRequest);
        var productPrice = await productPriceResponse.Content.ReadFromJsonAsync<ProductPriceDto>();

        // Assert
        productPriceResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        productPrice!.Id.Should().BeGreaterThan(0);
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
        var product = await response.Content.ReadFromJsonAsync<ProductDto>();

        var seller = (await client.GetFromJsonAsync<List<SellerDto>>(ApiAddress.Seller))!.First();
        var productPriceRequest = new AddProductPriceRequestDto(product!.Id, seller.Id, 12, DateOnly.FromDateTime(DateTime.Now));

        // Act
        HttpResponseMessage productPriceResponse =
            await client.PostAsJsonAsync(ApiAddress.ProductPrice, productPriceRequest);
        var createdProductPrice = await productPriceResponse.Content.ReadFromJsonAsync<ProductPriceDto>();

        // Act
        var productPrice =
            await client.GetFromJsonAsync<ProductPriceDto>($"{ApiAddress.ProductPrice}/{createdProductPrice!.Id}");

        // Assert
        productPrice!.Id.Should().Be(createdProductPrice!.Id);
    }

    [Fact]
    public async Task Get_WithInValidId_ShouldReturnNotFound()
    {
        // Arrange
        var apiFactory = new DefaultWebApplicationFactory();
        HttpClient client = apiFactory.CreateClient();

        // Act

        var response = await client.GetAsync($"{ApiAddress.ProductPrice}/1022222");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }


    [Theory]
    [InlineData(-2)]
    [InlineData(0)]
    public async Task Create_WithInvalidPrice_ReturnsBadRequest(decimal price)
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
        var product = await response.Content.ReadFromJsonAsync<ProductDto>();

        var seller = (await client.GetFromJsonAsync<List<SellerDto>>(ApiAddress.Seller))!.First();
        var productPriceRequest = new AddProductPriceRequestDto(product!.Id, seller.Id, price, DateOnly.FromDateTime(DateTime.Now));

        // Act
        HttpResponseMessage productPriceResponse =
            await client.PostAsJsonAsync(ApiAddress.ProductPrice, productPriceRequest);

        // Assert
        productPriceResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
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

        HttpResponseMessage response =
            await client.PutAsJsonAsync($"{ApiAddress.Product}/{createdProduct!.Id}", updateProductDto);

        var updatedProduct = await client.GetFromJsonAsync<ProductDto>($"{ApiAddress.Product}/{createdProduct!.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        updatedProduct!.Id.Should().Be(createdProduct!.Id);
        updatedProduct!.Name.Should().Be(updateProductDto.Name);
        updatedProduct!.Sku.Should().Be(updateProductDto.Sku);
    }

    [Fact]
    public async Task
        GetAllByProductId_WithTwoProductPriceForValidProductId_ShouldReturnBothProductPricesForSpecificProduct()
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
        var product = await response.Content.ReadFromJsonAsync<ProductDto>();

        var seller = (await client.GetFromJsonAsync<List<SellerDto>>(ApiAddress.Seller))!.First();

        var productPriceRequest = new AddProductPriceRequestDto(product!.Id, seller.Id, 12, DateOnly.FromDateTime(DateTime.Now));
        HttpResponseMessage productPriceResponse =
            await client.PostAsJsonAsync(ApiAddress.ProductPrice, productPriceRequest);
        var createdProductPrice = await productPriceResponse.Content.ReadFromJsonAsync<ProductPriceDto>();

        var secondProductPriceRequest =
            new AddProductPriceRequestDto(product!.Id, seller.Id, 10, DateOnly.FromDateTime(DateTime.Now.AddDays(1)));
        HttpResponseMessage secondProductPriceResponse =
            await client.PostAsJsonAsync(ApiAddress.ProductPrice, secondProductPriceRequest);
        var secondCreatedProductPrice = await secondProductPriceResponse.Content.ReadFromJsonAsync<ProductPriceDto>();

        // Act
        var productPrices =
            await client.GetFromJsonAsync<List<ProductPriceDto>>(
                $"{ApiAddress.ProductPrice}/Product/{createdProductPrice!.Id}");

        // Assert
        productPrices!.Count.Should().Be(2);
    }

    [Fact]
    public async Task UpdatePrice_WithValidProperties_ShouldProductPrice()
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
        var product = await response.Content.ReadFromJsonAsync<ProductDto>();

        var seller = (await client.GetFromJsonAsync<List<SellerDto>>(ApiAddress.Seller))!.First();
        var productPriceRequest = new AddProductPriceRequestDto(product!.Id, seller.Id, 12, DateOnly.FromDateTime(DateTime.Now));
        HttpResponseMessage productPriceResponse =
            await client.PostAsJsonAsync(ApiAddress.ProductPrice, productPriceRequest);
        var productPrice = await productPriceResponse.Content.ReadFromJsonAsync<ProductPriceDto>();

        var updatePriceRequest = new UpdateProductPriceRequestDto(productPrice!.Id, 100);
       
        // Act
        HttpResponseMessage updatedProductPriceResponse =
            await client.PutAsJsonAsync(ApiAddress.ProductPrice, updatePriceRequest);
        
        var updatedProductPrice = await client.GetFromJsonAsync<ProductPriceDto>($"{ApiAddress.ProductPrice}/{productPrice.Id}");
        
        // Assert
        updatedProductPriceResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        updatedProductPrice!.Price.Should().Be(updatePriceRequest.NewPrice);
    }
    
    [Fact]
    public async Task
        Create_TwoProductPriceForSameTimeAndProcuctAndSeller_ShouldReturnError()
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
        var product = await response.Content.ReadFromJsonAsync<ProductDto>();

        var seller = (await client.GetFromJsonAsync<List<SellerDto>>(ApiAddress.Seller))!.First();

        var productPriceRequest = new AddProductPriceRequestDto(product!.Id, seller.Id, 12, DateOnly.FromDateTime(DateTime.Now));
        HttpResponseMessage productPriceResponse =
            await client.PostAsJsonAsync(ApiAddress.ProductPrice, productPriceRequest);
        var createdProductPrice = await productPriceResponse.Content.ReadFromJsonAsync<ProductPriceDto>();
        
        // Act
        var secondProductPriceRequest =
            new AddProductPriceRequestDto(product!.Id, seller.Id, 10, DateOnly.FromDateTime(DateTime.Now));
        HttpResponseMessage secondProductPriceResponse =
            await client.PostAsJsonAsync(ApiAddress.ProductPrice, secondProductPriceRequest);

        // Assert
        secondProductPriceResponse!.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

}