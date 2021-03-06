using AutoMapper;
using MyStore.Domain.Entities;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Infrastructure
{
    public class ProductProfile:Profile//from Automapper to map models with database entities
    {
        public ProductProfile()
        {
            CreateMap<Product,ProductModel>();
            CreateMap<ProductModel, Product>();
        }
    }
}
