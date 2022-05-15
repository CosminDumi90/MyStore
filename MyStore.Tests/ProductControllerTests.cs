using Microsoft.AspNetCore.Mvc;
using Moq;
using MyStore.Controllers;
using MyStore.Models;
using MyStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyStore.Tests
{
    public class ProductControllerTests
    {
        private Mock<IProductService> mockProductService;
        public ProductControllerTests()
        {
            mockProductService = new Mock<IProductService>();

        }

        [Fact]
        public void Should_Return_Ok_OnGetAll()
        {
            //arange
            mockProductService.Setup(x => x.GetAllProducts())
                .Returns(MultipleProducts());

            var controller = new ProductsController(mockProductService.Object);


            //act
            var response = controller.Get();

            var result = response.Result as OkObjectResult;
            var actualData = result.Value as IEnumerable<ProductModel>;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<ProductModel>>(actualData);
        }

        [Fact]
        public void Should_Return_OK_OnGetByID()
        {
            //arrange
            int testSessionId = 1;
            mockProductService.Setup(x => x.GetById(testSessionId))
                .Returns(MultipleProducts()[testSessionId]);

            var controller = new ProductsController(mockProductService.Object);

            //act
            var response = controller.GetById(testSessionId);

            var result = response.Result as OkObjectResult;
            var actualData = result.Value as ProductModel;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<ProductModel>(actualData);
        }


        [Fact]
        public void ShouldReturn_AllProducts()
        {
            //arrange
            mockProductService.Setup(x => x.GetAllProducts()).
                Returns(MultipleProducts());
            var controller = new ProductsController(mockProductService.Object);
            //act
            var response = controller.Get();
            var result = response.Result as OkObjectResult;

            var actualData = result.Value as IEnumerable<ProductModel>;
            //assert
            Assert.Equal(MultipleProducts().Count(), actualData.Count());

        }
        [Fact]
        public void ShouldReturn_NoContent_On_Post()
        {
            ////arrange       
            mockProductService.Setup(x => x.AddProduct(It.IsAny<ProductModel>())).Returns(MultipleProducts()[1]);
            // Arrange
            var controller = new ProductsController(mockProductService.Object);
            // Act
            var response = controller.Post(MultipleProducts()[1]);
            // Assert
            Assert.IsType<CreatedAtActionResult>(response);
        }
        [Fact]
        public void ShouldReturn_NoContent_On_Delete()
        {
            ////arrange                     
            mockProductService.Setup(x => x.Delete(MultipleProducts()[1].Productid)).Returns(true);
            mockProductService.Setup(x => x.Exists(MultipleProducts()[1].Productid)).Returns(true);
            // Arrange
            var controller = new ProductsController(mockProductService.Object);
            // Act
            var response = controller.Delete(MultipleProducts()[1].Productid);
            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        private List<ProductModel> MultipleProducts()
        {
            return new List<ProductModel>()
            {
                new ProductModel
                {
                    Categoryid = 2,
                    Productid = 3,
                    Productname = "Test Product 1",
                    Discontinued = false,
                    Supplierid = 4,
                    Unitprice = 100
                },
                     new ProductModel
                {
                    Categoryid = 2,
                    Productid = 3,
                    Productname = "Test Product 2",
                    Discontinued = false,
                    Supplierid = 4,
                    Unitprice = 100
                },
                        new ProductModel
                {
                    Categoryid = 2,
                    Productid = 3,
                    Productname = "Test Product 3",
                    Discontinued = false,
                    Supplierid = 4,
                    Unitprice = 100
                },
            };
        }
    }
}
