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

        public IEnumerable<Characteristic> GetCharacteristics(int categoryId)
        {
            return GetRepository<ICharacteristicRepository>().GetByCategoryId(categoryId);
        }

        public IEnumerable<CharacteristicValue> GetCharacteristicValuesByProductSku(int productSku)
        {
            var id = GetRepository<IProductRepository>().GetIdBySku(productSku);
            return GetRepository<ICharacteristicValueRepository>().GetByProductId(id);
        }

        public bool SkuExists(int sku)
        {
            return GetRepository<IProductRepository>().SkuExists(sku);
        }

        public void CreateProduct(Product product, IEnumerable<CharacteristicValue> characteristicValues)
        {
            if (product == null) throw new ArgumentNullException("product");
            if (characteristicValues == null) throw new ArgumentNullException("characteristicValues");

            var productRepo = GetRepository<IProductRepository>();
            productRepo.Create(product);
            var productId = GetRepository<IProductRepository>().GetIdBySku(product.Sku);
            foreach (var characteristicValue in characteristicValues)
            {
                characteristicValue.ProductId = productId;
                GetRepository<ICharacteristicValueRepository>().Create(characteristicValue);
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return GetRepository<IProductRepository>().GetAll();
        }

        public Product GetProductById(int id)
        {
            return GetRepository<IProductRepository>().Get(id);
        }

        public Product GetProductBySku(int sku)
        {
            return GetRepository<IProductRepository>().GetBySku(sku);
        }

        public void EditProduct(Product product,IEnumerable<CharacteristicValue> characteristicValues)
        {
            if (product == null) throw new ArgumentNullException("product");

            var productRepo = GetRepository<IProductRepository>();
            var characteristicValueRepo = GetRepository<ICharacteristicValueRepository>();
            product.Category = GetRepository<ICategoryRepository>().Get(product.CategoryId);
            var oldProduct = productRepo.GetBySku(product.Sku);

            bool categoryChanged = !oldProduct.CategoryId.Equals(product.CategoryId);
            var productId = GetRepository<IProductRepository>().GetIdBySku(product.Sku);
            oldProduct.CopyFrom(product);
            productRepo.Update(oldProduct);
            if (!categoryChanged)
            {
                foreach (var characteristicValue in characteristicValues)
                {
                    characteristicValue.ProductId = productId;
                    characteristicValue.Product = productRepo.Get(productId);
                    var oldCharacteristicValue = characteristicValueRepo.Get(characteristicValue.Id);
                    oldCharacteristicValue.CopyFrom(characteristicValue);
                    characteristicValueRepo.Update(oldCharacteristicValue);
                }
            }
            else
            {
                var oldCaharcteristicValues = characteristicValueRepo.GetByProductId(productId).ToList();
                foreach (var characteristicValue in oldCaharcteristicValues)
                {
                    characteristicValueRepo.Delete(characteristicValue.Id);
                }
                foreach (var characteristicValue in characteristicValues)
                {
                    characteristicValue.ProductId = productId;
                    GetRepository<ICharacteristicValueRepository>().Create(characteristicValue);
                }
            }
        }

        public void DeleteProduct(int id)
        {
            GetRepository<IProductRepository>().Delete(id);
        }


        public void AddCharacteristicToCategory(string categoryName, int characteristicId)
        {
            if (categoryName == null) throw new ArgumentNullException("categoryName");
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
