using LocalEconomyApi.Abstract.business;
using LocalEconomyApi.Abstract;
using LocalEconomyApi.DataAccess.Abstract;
using LocalEconomyApi.Models;
using LocalEconomyApi.Models.Concrete;
using System;
using System.Collections.Generic;

namespace LocalEconomyApi.Concrete.business
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;

        public BusinessService(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }

        public IEnumerable<Business> GetAllBusinesses()
        {
            try
            {
                return _businessRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("İşletmeler alınırken bir hata oluştu.", ex);
            }
        }

        public Business GetBusinessById(int id)
        {
            try
            {
                var business = _businessRepository.Get(b => b.BusinessId == id);
                if (business == null)
                    throw new KeyNotFoundException($"ID: {id} olan işletme bulunamadı.");

                return business;
            }
            catch (Exception ex)
            {
                throw new Exception($"ID: {id} için işletme alınırken bir hata oluştu.", ex);
            }
        }

        public void AddBusiness(Business business)
        {
            try
            {
                _businessRepository.Add(business);
            }
            catch (Exception ex)
            {
                throw new Exception("İşletme eklenirken bir hata oluştu.", ex);
            }
        }

        public void UpdateBusiness(Business business)
        {
            try
            {
                var existingBusiness = _businessRepository.Get(b => b.BusinessId == business.BusinessId);
                if (existingBusiness == null)
                    throw new KeyNotFoundException($"ID: {business.BusinessId} olan işletme bulunamadı.");

                _businessRepository.Update(business);
            }
            catch (Exception ex)
            {
                throw new Exception($"ID: {business.BusinessId} olan işletme güncellenirken bir hata oluştu.", ex);
            }
        }

        public void DeleteBusiness(int id)
        {
            try
            {
                var business = _businessRepository.Get(b => b.BusinessId == id);
                if (business == null)
                    throw new KeyNotFoundException($"ID: {id} olan işletme bulunamadı.");

                _businessRepository.Delete(business);
            }
            catch (Exception ex)
            {
                throw new Exception($"ID: {id} olan işletme silinirken bir hata oluştu.", ex);
            }
        }

        public void SoftDeleteBusiness(int id)
        {
            try
            {
                var business = _businessRepository.Get(b => b.BusinessId == id);
                if (business == null)
                    throw new KeyNotFoundException($"ID: {id} olan işletme bulunamadı.");

                // İşletmeyi soft delete yapmak için IsDeleted alanını true olarak ayarlayın.
                business.IsDeleted = true;
                business.UpdatedDate = DateTime.Now;
                business.UpdatedBy = "CurrentUser"; // Giriş yapan kullanıcı bilgisi buraya alınabilir.

                _businessRepository.Update(business);
            }
            catch (Exception ex)
            {
                throw new Exception($"ID: {id} olan işletme soft delete yapılırken bir hata oluştu.", ex);
            }
        }

        public IEnumerable<Business> GetBusinessesByCity(string city)
        {
            try
            {
                return _businessRepository.GetBusinessesByCity(city);
            }
            catch (Exception ex)
            {
                throw new Exception($"Şehir: {city} için işletmeler alınırken bir hata oluştu.", ex);
            }
        }
    }
}
