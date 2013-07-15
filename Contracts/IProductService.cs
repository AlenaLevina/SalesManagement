using System.Collections.Generic;
using Model;

namespace Contracts
{
    public interface IProductService
    {
        void CreateCategory(string name, IEnumerable<Characteristic> characteristics);
        IEnumerable<Characteristic> GetAllCharacteristics();
        Category GetCategoryById(int id);
        void EditCategory(Category newCategory);
        IEnumerable<Category> GetAllCategories();
        void CreateCharacteristic(string name);
        void DeleteCategory(int id);
        int GetNewSku(int size);
        IEnumerable<Characteristic> GetCharacteristics(int categoryId);
        IEnumerable<CharacteristicValue> GetCharacteristicValuesByProductSku(int productSku);
        bool SkuExists(int sku);
        void CreateProduct(Product product, IEnumerable<CharacteristicValue> characteristicValues);
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        Product GetProductBySku(int sku);
        void EditProduct(Product product,IEnumerable<CharacteristicValue> characteristicValues);
        void DeleteProduct(int id);
    }
}
