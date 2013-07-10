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
    }
}
