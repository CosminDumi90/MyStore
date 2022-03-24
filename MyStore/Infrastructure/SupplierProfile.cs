using AutoMapper;
using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class SupplierProfile:Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier,SupplierModel>();
            CreateMap<SupplierModel, Supplier>();
        }
    }
}
