using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Repository.Contracts;
using Repository.Data.Entities;


namespace Core.Implementation
{
    public class ProductCoreImpl : IProductCore
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _maper;

        public ProductCoreImpl(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _maper = mapper;
        }

        public async Task<ProductCore> Add(ProductCore product)
        {
            try
            {
                var newProduct = _maper.Map<Product>(product);
                await _productRepository.AddAsync(newProduct);

                // Aquí va la lógica de negocio

                var productCore = _maper.Map<ProductCore>(newProduct);
                return productCore;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            
        }

        public async Task<ProductCore> DeleteById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new KeyNotFoundException($"The product with ID = {id} not exists.");
            }
            else
            {
                var productCore = _maper.Map<ProductCore>(product);
                await _productRepository.DeleteAsync(id);
                return productCore;
            }
        }

        public async Task<IEnumerable<ProductCore>> GetAll()
        {
            IEnumerable<Product> products = await _productRepository.GetAllAsync();
            return products.Select(p => _maper.Map<ProductCore>(p)).ToList();
        }

        public async Task<ProductCore> GetById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _maper.Map<ProductCore>(product);
        }

        public async Task<ProductCore> Update(ProductCore product)
        {
            var existingProduct = await _productRepository.GetByIdAsync(product.Id);

            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"The product with ID = {product.Id} not exists.");
            }
            else
            {
                var updatedProduct = _maper.Map<Product>(product);
                await _productRepository.UpdateAsync(updatedProduct);

                return _maper.Map<ProductCore>(updatedProduct);
            }

        }
    }
}
