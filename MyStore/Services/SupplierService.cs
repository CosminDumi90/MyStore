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
    public interface ISupplierService
    {
        SupplierModel AddSupplier(SupplierModel newSupplier);
        bool Delete(int id);
        bool Exists(int id);
        IEnumerable<SupplierModel> GetAllSuppliers();
        SupplierModel GetSupplierById(int id);
        void UpdateSupplier(SupplierModel model);
    }
    public class SupplierService:ISupplierService
    {
        private readonly ISupplierRepository supplierRepository;
        private readonly IMapper mapper;
        public SupplierService(ISupplierRepository supplierRepository, IMapper mapper)
        {
            this.supplierRepository = supplierRepository;
            this.mapper = mapper;
        }

        public SupplierModel AddSupplier(SupplierModel newSupplier)
        {
            Supplier supplierToAdd = mapper.Map<Supplier>(newSupplier);
            var adddSupplier = supplierRepository.Add(supplierToAdd);
            newSupplier = mapper.Map<SupplierModel>(adddSupplier);
            return newSupplier;
        }

        public IEnumerable<SupplierModel> GetAllSuppliers()
        {
            var allSuppliers = supplierRepository.GetAll().ToList();
            var allsupplierModels = mapper.Map<IEnumerable<SupplierModel>>(allSuppliers);
            return allsupplierModels;
        }

        public SupplierModel GetSupplierById(int id)
        {
            var allSuppliersById= supplierRepository.GetSupplierById(id);
            var allSupplierModelsById = mapper.Map<SupplierModel>(allSuppliersById);
            return allSupplierModelsById;
        }
        public void UpdateSupplier(SupplierModel model)
        {
            Supplier supplierToUpdate = mapper.Map<Supplier>(model);
            supplierRepository.Update(supplierToUpdate);
        }
        public bool Exists(int id)
        {
            return supplierRepository.Exists(id);
        }
        public bool Delete(int id)
        {
            var itemToDelete = supplierRepository.GetSupplierById(id);
            return supplierRepository.Delete(itemToDelete);
        }
    }
}
