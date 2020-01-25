using AutoMapper;
using CleanArchitecture.Api.Filters;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Api.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IAsyncRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductsController(IAsyncRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repository.ListAsync(r => r.Category).ConfigureAwait(false);
            return Ok(_mapper.Map<IReadOnlyList<Product>, List<ProductDTO>>(products));
        }

        // GET: api/products/5
        [ServiceFilter(typeof(ValidateProductExistsFilter))]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repository.GetByIdAsync(e => e.Id == id, r => r.Category).ConfigureAwait(false);
            return Ok(_mapper.Map<Product, ProductDTO>(product));
        }

        // PUT: api/products/5
        [ServiceFilter(typeof(ValidateProductExistsFilter))]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutProduct([FromRoute] int id, [FromBody] ProductDTO product)
        {     
            await _repository.UpdateAsync(_mapper.Map<ProductDTO, Product>(product)).ConfigureAwait(false);
            return NoContent();
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] ProductDTO product)
        {
            var newProduct = await _repository.AddAsync(_mapper.Map<ProductDTO, Product>(product)).ConfigureAwait(false);
            return Created("api/products", newProduct);
        }

        // DELETE: api/products/5
        [ServiceFilter(typeof(ValidateProductExistsFilter))]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int id)
        {
            await _repository.DeleteAsync(id).ConfigureAwait(false);
            return NoContent();
        }
    }
}
