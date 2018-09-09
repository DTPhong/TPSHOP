using System.Collections.Generic;
using TPshop.Data.Infrastructure;
using TPshop.Data.Respositories;
using TPshop.Model.Models;

namespace TPshop.Service
{
    public interface ISupplierService
    {
        Supplier Add(Supplier Supplier);

        Supplier Delete(int id);

        void Update(Supplier Supplier);

        IEnumerable<Supplier> GetAll();

        IEnumerable<Supplier> GetAll(string keyword);

        Supplier GetById(int id);

        void Save();
    }

    public class SupplierService : ISupplierService
    {
        private ISupplierRepository _supplierRepository;
        private IUnitOfWork _unitOfWork;

        public SupplierService(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork)
        {
            this._supplierRepository = supplierRepository;
            this._unitOfWork = unitOfWork;
        }

        public Supplier Add(Supplier Supplier)
        {
            return _supplierRepository.Add(Supplier);
        }

        public Supplier Delete(int id)
        {
            return _supplierRepository.Delete(id);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _supplierRepository.GetAll();
        }

        public IEnumerable<Supplier> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _supplierRepository.GetMulti(x => x.Name.Contains(keyword));
            }
            else
            {
                return _supplierRepository.GetAll();
            }
        }

        public Supplier GetById(int id)
        {
            return _supplierRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Supplier Supplier)
        {
            _supplierRepository.Update(Supplier);
        }
    }
}