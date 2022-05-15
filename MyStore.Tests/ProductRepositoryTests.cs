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
        [Fact]
        public void Should_GetOneProduct()
        {
            //arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetById(ReturnMultiple()[0].Productid))
                .Returns(ReturnOneProduct(Consts.TestProduct));

            //act
            var result = mockRepo.Object.GetById(Consts.TestProduct);

            //asert
            Assert.Equal(Consts.TestProduct, result.Productid);
            Assert.IsType<Product>(result);
        }


  
        [Fact]
        public void Shoul_Return_Product_On_Post()
        {
            //arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.Add(It.IsAny<Product>())).Returns(ReturnOneProduct(Consts.TestProduct));

            //act
            var result = mockRepo.Object.Add(ReturnOneProduct(Consts.TestProduct));

            //asert
            Assert.Equal(Consts.ProductName, result.Productname);
            Assert.IsType<Product>(result);
        }
        [Fact]
        public void ShouldReturn_Product_On_Put()
        {
            //arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.Update(It.IsAny<Product>())).Returns(ReturnOneProduct(Consts.TestProduct));

            //act
            var result = mockRepo.Object.Update(ReturnOneProduct(Consts.TestProduct));

            //asert
            Assert.Equal(Consts.ProductName, result.Productname);
            Assert.IsType<Product>(result);
        }


        [Fact]
        public void Shoul_Return_True_On_Delete()
        {
            //arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Product>())).Returns(true);

            //act
            var result = mockRepo.Object.Delete(ReturnOneProduct(Consts.TestProduct));

            //asert
            Assert.True(result);
        }
        private Product ReturnOneProduct(int i)
        {
            IEnumerable<Product> products = ReturnMultiple();
            return products.Where(x => x.Productid == i).FirstOrDefault();
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
