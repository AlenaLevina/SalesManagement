using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            return GetRepository<ICharacteristicRepository>().GetAll();
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

        public void EditCategory(int id, IEnumerable<Characteristic> chosenCharacteristics)
        {
            if (chosenCharacteristics == null) throw new ArgumentNullException("chosenCharacteristics");

            var categoryRepo = GetRepository<ICategoryRepository>();
            var category = categoryRepo.Get(id);
            category.Characteristics.Clear();
            categoryRepo.Update(category);
            foreach (var characteristic in chosenCharacteristics)
            {
                AddCharacteristicToCategory(id, characteristic.Id);
            }
            //foreach (var categoryCharacteristic in category.Characteristics)
            //{
            //    if (!chosenCharacteristics.Any(c => c.Id.Equals(categoryCharacteristic.Id)))
            //    {
            //        RemoveCharacteristicFromCategory(id,categoryCharacteristic.Id);
            //    }
            //}
            //foreach (var chosenCharacteristic in chosenCharacteristics)
            //{
            //    if (!category.Characteristics.Any(c => c.Id.Equals(chosenCharacteristic.Id)))
            //    {
            //        AddCharacteristicToCategory(id, chosenCharacteristic.Id);
            //    }
            //}
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return GetRepository<ICategoryRepository>().GetAll();
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
