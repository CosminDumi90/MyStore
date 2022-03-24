using MyStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyStore.Tests
{
   
    public class ProductModelTests
    {
        public const string ProductNameReqMessage = "The Productname field is required.";
        public const int ValidCategoryId = 2;
        
        
        [Fact]
        public void FailingTest()
        {
            Assert.Equal(2, 2);
            
        }

        [Fact]
        public void Should_Pass()
        {
            //arrange
            var sut = new ProductModel()
            {
             Categoryid = ValidCategoryId,
             Productid = 2,
             Supplierid = 2,
             Unitprice = 10,
             Discontinued = true,
             Productname = "Test productName"
            };
            //sut=subject under test

            //act
             var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResults, true);

            //assert
            Assert.True(actual, "Expected to succeed");
        }
        [Fact]
        public void Should_Fail_When_ProductName_IsEmpty()
        {
            //arrange
            var sut = new ProductModel()
            {
                Categoryid = ValidCategoryId,
                Productid = 2,
                Supplierid = 2,
                //Unitprice = 10,
                Discontinued = true,
                Productname = ""
            };
            //sut=subject under test
            //act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResults, true);
            //assert
            var message = validationResults[0];
            //var message1 = validationResults[1];

            Assert.Equal(ProductNameReqMessage, message.ErrorMessage);
            //Assert.Equal("The UnitPrice field is required.", message1.ErrorMessage);
        }
    }
}



