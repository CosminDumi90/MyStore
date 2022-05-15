using AutoMapper;
using MyStore.Data;
using MyStore.Domain.Entities;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services
{
   public interface IProductService
    {
        ProductModel AddProduct(ProductModel newProduct);//Post
        bool Delete(int id);
        bool Exists(int id);
        IEnumerable<ProductModel> GetAllProducts();
       ProductModel GetById(int id);
        ProductModel UpdateProduct(ProductModel model);
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper )
        {
           this.productRepository = productRepository;
           this.mapper = mapper;
        }
        public IEnumerable<ProductModel> GetAllProducts()
        {
            var allProducts = productRepository.GetAll().ToList();
            
           var productModels= mapper.Map<IEnumerable<ProductModel>>(allProducts);
            return productModels;
        }
        public ProductModel GetById(int id)
        {
          var allProductsById= productRepository.GetById(id);
            var allProductModelsById = mapper.Map<ProductModel>(allProductsById);
            return allProductModelsById;
        }
        public ProductModel AddProduct(ProductModel newProduct)
        {
            //-.Product model - > Product

            Product productToAdd = mapper.Map<Product>(newProduct);
            var addedProduct = productRepository.Add(productToAdd);
            newProduct = mapper.Map<ProductModel>(addedProduct);
            return newProduct;
        }
        public ProductModel UpdateProduct(ProductModel model)
        {
            Product productToUpdate = mapper.Map<Product>(model);
            var updatedProduct = productRepository.Update(productToUpdate);
            return mapper.Map<ProductModel>(updatedProduct);
        }
        public bool Exists(int id)
        {
            return productRepository.Exists(id);
        }

        public bool Delete(int id) 
        {
            var itemToDelete = productRepository.GetById(id);
            return productRepository.Delete(itemToDelete);
        }
    }
}
