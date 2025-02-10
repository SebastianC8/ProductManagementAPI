
using Core.Entities;

namespace Core.Contracts
{
    public interface IProductCore
    {
        Task<IEnumerable<ProductCore>> GetAll();

        Task<ProductCore> GetById(int id);

        Task<ProductCore> Add(ProductCore product);

        Task<ProductCore> Update(ProductCore product);

        Task<ProductCore> DeleteById(int id);
    }
}
