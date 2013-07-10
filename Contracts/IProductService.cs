using System.Collections.Generic;
using Model;

namespace Contracts
{
    public interface IProductService
    {
        void CreateCategory(string name, IEnumerable<Characteristic> characteristics);
        IEnumerable<Characteristic> GetAllCharacteristics();
    }
}
