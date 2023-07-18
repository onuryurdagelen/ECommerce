using AutoMapper;
using ECommerce.API.Entities;
using ECommerce.Data.Abstracts;
using ECommerce.Data.Data;
using ECommerce.Data.Dtos;
using ECommerce.Data.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        //private readonly IProductRepository _productRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;
        public ProductsController(
            IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> productBrandRepository,
            IGenericRepository<ProductType> productTypeRepository
,           IMapper mapper)
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return Ok(await _productRepository.ListAsync(new ProductsWithTypesAndBrandsSpecification()));

        }
        //[HttpGet("getProductsBySpecification")]
        //public async Task<ActionResult<IReadOnlyList<Product>>> GetProductsBySpecification()
        //{
        //    IReadOnlyCollection<Product> spec = await _productRepository.ListAsync(new ProductsWithTypesAndBrandsSpecification());
        //    return Ok(_mapper.Map<IReadOnlyList<Product>,
        //                          IReadOnlyList<ProductToReturnDto
        //                          >>(spec.ToList()));

        //}
        [HttpGet("getProductsBySpecification")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductsByOrderSpecification(string order,int? brandId,int? typeId)
        {
            IReadOnlyCollection<Product> spec = await _productRepository.ListAsync(new ProductsWithTypesAndBrandsSpecification(order,brandId,typeId));
            return Ok(_mapper.Map<IReadOnlyList<Product>,
                                  IReadOnlyList<ProductToReturnDto
                                  >>(spec.ToList()));

        }
        [HttpGet("getProductsByPaging")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductsByOrderSpecification(int skip = 0, int take = 5)
        {
            IReadOnlyCollection<Product> spec = await _productRepository.ListAsync(new ProductsWithTypesAndBrandsSpecification(skip,take));
            return Ok(_mapper.Map<IReadOnlyList<Product>,
                                  IReadOnlyList<ProductToReturnDto
                                  >>(spec.ToList()));

        }
        [HttpGet("activeProducts")]
        public async Task<ActionResult<List<Product>>> GetActiveProducts()
        {
            return Ok(await _productRepository.ListAsync(new ProductsWithTypesAndBrandsSpecification(p=> p.ActiveFlag == 1)));

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) 
        {
            return Ok(await _productRepository.GetByIdAsync(id));
        }
        [HttpGet("specification/{id}")]
        public async Task<ActionResult<Product>> GetProductBySpec(int id)
        {
            ProductsWithTypesAndBrandsSpecification spec = new ProductsWithTypesAndBrandsSpecification(p => p.Id == id);
            Product product = await _productRepository.GetEntityWithSpec(spec);

            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepository.GetAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepository.GetAllAsync());
        }
    }
}
