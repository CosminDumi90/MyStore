using Moq;
using MyStore.Data;
using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyStore.Tests
{
    public class ProductRepositoryTests
    {
        public ProductRepositoryTests()
        {

        }

        [Fact]
        public void Should_GetAllProducts()
        {
            //arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetAll())
                .Returns(ReturnMultiple()); 
           

            //act
            
            var result = mockRepo.Object.GetAll();
           
            //assert
            Assert.Equal(2, result.Count());
            Assert.IsType<List<Product>>(result);
        }

        private List<Product> ReturnMultiple()
        {
            return new List<Product>() {
            new Product
            {
                Categoryid=1,
                Productname="test",
                Supplierid=2,
                Unitprice = 10,
                Discontinued = true

            },
                new Product
            {
                Categoryid=2,
                Productname="test2",
                Supplierid=3,
                Unitprice = 100,
                Discontinued = true

            }


            };
        }
    }
}
