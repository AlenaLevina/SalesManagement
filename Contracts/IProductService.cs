using System.Collections.Generic;
using Model;

namespace Contracts
{
    public interface IProductService
    {
        void CreateCategory(string name, IEnumerable<Characteristic> characteristics);
        IEnumerable<Characteristic> GetAllCharacteristics();
        Category GetCategoryById(int id);
        void EditCategory(int id, IEnumerable<Characteristic> characteristics);
        IEnumerable<Category> GetAllCategories();
    }
}
