using AutoMapper;
using ECommerce.API.Entities;
using ECommerce.Data.Abstracts;
using ECommerce.Data.Concretes;
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
    public class ProductsController : BuggyController
    {
        private readonly IGenericRepository<Product> _GproductRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGenericRepository<ProductBrand> _GproductBrandRepository;
        private readonly IGenericRepository<ProductType> _GproductTypeRepository;
        private readonly IMapper _mapper;
        public ProductsController(
            IProductRepository productRepository,
            IGenericRepository<Product> GproductRepository,
            IGenericRepository<ProductBrand> GproductBrandRepository,
            IGenericRepository<ProductType> GproductTypeRepository
,           IMapper mapper)
        {
            _productRepository = productRepository;
            _GproductRepository = GproductRepository;
            _GproductBrandRepository = GproductBrandRepository;
            _GproductTypeRepository = GproductTypeRepository;
            _mapper = mapper;
        }
        [HttpGet]
        //[HttpGet("getProductsBySpecification")]
        //public async Task<ActionResult<IReadOnlyList<Product>>> GetProductsBySpecification()
        //{
        //    IReadOnlyCollection<Product> spec = await _productRepository.ListAsync(new ProductsWithTypesAndBrandsSpecification());
        //    return Ok(_mapper.Map<IReadOnlyList<Product>,
        //                          IReadOnlyList<ProductToReturnDto
        //                          >>(spec.ToList()));

        //}
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams productParams)
       {

            ProductsWithTypesAndBrandsSpecification spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            ProductWithFiltersForCountSpecification countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await _productRepository.CountAsync(countSpec);

            IReadOnlyCollection<Product> products = await _productRepository.ListAsync(spec);


            IReadOnlyList<ProductToReturnDto> mappedProducts =_mapper.Map<IReadOnlyList<Product>,
                                  IReadOnlyList<ProductToReturnDto
                                  >>(products.ToList());

            Pagination<List<ProductToReturnDto>> data = new Pagination<List<ProductToReturnDto>>()
            {
                Count = totalItems,
                Data = mappedProducts.ToList(),
                PageIndex = productParams.PageIndex,
                PageSize = productParams.PageSize,  
            };

            return Ok(data);

        }
        //[HttpGet("getProductsByPaging")]
        //public async Task<ActionResult<IReadOnlyList<Product>>> GetProductsByPaging(ProductSpecParams productParams)
        //{
        //    IReadOnlyCollection<Product> spec = await _productRepository.ListAsync(new ProductsWithTypesAndBrandsSpecification(skip,take));
        //    return Ok(_mapper.Map<IReadOnlyList<Product>,
        //                          IReadOnlyList<ProductToReturnDto
        //                          >>(spec.ToList()));

        //}
        [HttpGet("activeProducts")]
        public async Task<ActionResult<List<Product>>> GetActiveProducts()
        {
            return Ok(await _productRepository.ListAsync(new ProductsWithTypesAndBrandsSpecification(p=> p.ActiveFlag == 1)));

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id) 
        {
            Product product = await _productRepository.GetProductById(id);
            if(product == null)
                return GetNotFoundRequest();
            ProductToReturnDto productToReturnDto = _mapper.Map<Product, ProductToReturnDto>(product);
            return Ok(productToReturnDto);
        }
        [HttpGet("specification/{id}")]
        public async Task<IActionResult> GetProductBySpec(int id)
        {
            ProductsWithTypesAndBrandsSpecification spec = new ProductsWithTypesAndBrandsSpecification(p => p.Id == id);
            Product product = await _productRepository.GetEntityWithSpec(spec);

            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _GproductBrandRepository.GetAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            return Ok(await _GproductTypeRepository.GetAllAsync());
        }
    }
}
