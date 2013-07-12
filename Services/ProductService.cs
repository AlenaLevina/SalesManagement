using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Common.Helpers;
using Contracts;
using Data.Exceptions;
using Data.Repositories;
using Model;

namespace Services
{
    public class ProductService:BaseService,IProductService
    {
        public IEnumerable<Characteristic> GetAllCharacteristics()
        {
            return GetRepository<ICharacteristicRepository>().GetAll().OrderBy(e=>e.Name);
        }

        public Category GetCategoryById(int id)
        {
            return GetRepository<ICategoryRepository>().Get(id);
        }

        public void CreateCategory(string name, IEnumerable<Characteristic> characteristics)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (characteristics == null) throw new ArgumentNullException("characteristics");

            var categoryRepo = GetRepository<ICategoryRepository>();
            categoryRepo.Create(new Category { Name = name });
            foreach (var characteristic in characteristics)
            {
                AddCharacteristicToCategory(name, characteristic.Id);
            }
        }

        public void EditCategory(Category newCategory)
        {
            if (newCategory == null) throw new ArgumentNullException("newCategory");

            var categoryRepo = GetRepository<ICategoryRepository>();
            var oldCategory = categoryRepo.Get(newCategory.Id);
            oldCategory.CopyFrom(newCategory);
            oldCategory.Characteristics.Clear();
            categoryRepo.Update(oldCategory);
            foreach (var characteristic in newCategory.Characteristics)
            {
                AddCharacteristicToCategory(oldCategory.Id, characteristic.Id);
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return GetRepository<ICategoryRepository>().GetAll().OrderBy(e=>e.Name);
        }

        public void CreateCharacteristic(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            GetRepository<ICharacteristicRepository>().Create(new Characteristic {Name = name});
        }

        public void DeleteCategory(int id)
        {
            GetRepository<ICategoryRepository>().Delete(id);
        }

        public int GetNewSku(int size)
        {
            int newSku;
            while (true)
            {
                newSku = RandomHelper.GenerateNumber(size);
                if (!GetRepository<IProductRepository>().SkuExists(newSku)) break;
            }
            return newSku;
        }

        

        public void AddCharacteristicToCategory(string categoryName, int characteristicId)
        {
            var categoryRepo = GetRepository<ICategoryRepository>();
            var characteristicRepo = GetRepository<ICharacteristicRepository>();
            var category = categoryRepo.GetByName(categoryName);
            var characteristic = characteristicRepo.Get(characteristicId);
            if (category != null && characteristic!=null) {
                if (category.Characteristics != null) category.Characteristics.Add(characteristic);
                else category.Characteristics=new Collection<Characteristic>{characteristic};
                categoryRepo.Update(category);
            }
            else throw new DataException("Can't perform operation due to invalid data");
        }

        public void AddCharacteristicToCategory(int categoryId, int characteristicId)
        {
            var categoryRepo = GetRepository<ICategoryRepository>();
            var characteristicRepo = GetRepository<ICharacteristicRepository>();
            var category = categoryRepo.Get(categoryId);
            var characteristic = characteristicRepo.Get(characteristicId);
            if (category != null && characteristic != null)
            {
                if (category.Characteristics != null) category.Characteristics.Add(characteristic);
                else category.Characteristics = new Collection<Characteristic> { characteristic };
                categoryRepo.Update(category);
            }
            else throw new DataException("Can't perform operation due to invalid data");
        }

        public void RemoveCharacteristicFromCategory(int categoryId, int characteristicId)
        {
            var categoryRepo = GetRepository<ICategoryRepository>();
            var characteristicRepo = GetRepository<ICharacteristicRepository>();
            var category = categoryRepo.Get(categoryId);
            var characteristic = characteristicRepo.Get(characteristicId);
            if (category != null && characteristic != null)
            {
                if (category.Characteristics != null) category.Characteristics.Remove(characteristic);
                categoryRepo.Update(category);
            }
            else throw new DataException("Can't perform operation due to invalid data");
        }
    }
}
