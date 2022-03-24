using Moq;
using MyStore.Domain.Entities;
using MyStore.Models;
using MyStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Tests.Mocks.Services
{
   public class MockProductService : Mock<IProductService>
    {
        public MockProductService MockGetAllProducts(List<ProductModel> results)
        {
            Setup(x => x.GetAllProducts())
                .Returns(results);
            return this;//fluent validations
        }

        public MockProductService MockGetById(Product product)
        {
            Setup(x => x.GetById(It.IsAny<int>()))
                .Throws(new Exception("Product with ID not found"));
            return this;
        }

        public MockProductService MockGetByInvalidId()
        {
            Setup(x => x.GetById(It.IsAny<int>()))
                .Throws(new Exception("Product with that ID was not found!"));
            return this;
        }
    }
}
