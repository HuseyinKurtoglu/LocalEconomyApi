using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.DataAcces.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = _dbSet.AsQueryable();

            if (typeof(T) == typeof(BusinessEntity))
            {
                query = query.Include("Campaigns").Include("Category");
            }
            else if (typeof(T) == typeof(Campaign))
            {
                query = query.Include("Business").Include("Category");
            }
            else if (typeof(T) == typeof(Category))
            {
                query = query.Include("Businesses").Include("Campaigns");
            }

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                throw new InvalidOperationException($"ID {id} olan {typeof(T).Name} bulunamadı.");

            // Soft delete kontrolü
            var isDeletedProperty = entity.GetType().GetProperty("IsDeleted");
            if (isDeletedProperty != null)
            {
                bool? isDeleted = (bool?)isDeletedProperty.GetValue(entity);
                if (isDeleted == true)
                    throw new InvalidOperationException($"ID {id} olan {typeof(T).Name} silinmiş.");
            }

            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            // Eğer eklenen nesne bir BusinessEntity ise isim benzersiz mi kontrol et
            if (typeof(T) == typeof(BusinessEntity))
            {
                var business = entity as BusinessEntity;

                // Aynı isimde aktif işletme var mı?
                var existingBusiness = await _dbSet
                    .Where(e => EF.Property<string>(e, "Name") == business.Name)
                    .FirstOrDefaultAsync();

                if (existingBusiness != null)
                {
                    var isDeletedProperty = existingBusiness.GetType().GetProperty("IsDeleted");
                    if (isDeletedProperty != null)
                    {
                        bool? isDeleted = (bool?)isDeletedProperty.GetValue(existingBusiness);
                        if (isDeleted == true)
                        {
                            // Eğer işletme soft delete olarak işaretlenmişse, tekrar aktif hale getir
                            isDeletedProperty.SetValue(existingBusiness, false);
                            _dbSet.Update(existingBusiness);
                            await _context.SaveChangesAsync();
                            return existingBusiness as T;
                        }
                    }

                    throw new InvalidOperationException($"'{business.Name}' adında aktif bir işletme zaten mevcut.");
                }
            }

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            var isDeletedProperty = entity.GetType().GetProperty("IsDeleted");

            if (isDeletedProperty != null)
            {
                isDeletedProperty.SetValue(entity, true);
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }

            await _context.SaveChangesAsync();
        }
    }
}
