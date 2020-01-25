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
    public class CategoriesController : BaseController
    {
        private readonly IAsyncRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoriesController(IAsyncRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            var categories = await _repository.ListAsync(r => r.Product).ConfigureAwait(false);
            return Ok(_mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryProductDTO>>(categories));
        }

        // GET: api/categories/5
        [ServiceFilter(typeof(ValidateCategoryExistsFilter))]
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _repository.GetByIdAsync(e => e.Id == id, r => r.Product).ConfigureAwait(false);
            return Ok(_mapper.Map<Category, CategoryProductDTO>(category));
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory([FromBody] CategoryDTO category)
        {
            var newCategory = await _repository.AddAsync(_mapper.Map<CategoryDTO, Category>(category)).ConfigureAwait(false);
            return Created("api/categories", newCategory);
        }

        // PUT: api/categories/5
        [ServiceFilter(typeof(ValidateCategoryExistsFilter))]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutCategory([FromRoute] int id, [FromBody] CategoryDTO category)
        {
            await _repository.UpdateAsync(_mapper.Map<CategoryDTO, Category>(category)).ConfigureAwait(false);
            return NoContent();
        }

        // DELETE: api/categories/5
        [ServiceFilter(typeof(ValidateCategoryExistsFilter))]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory([FromRoute] int id)
        {
            await _repository.DeleteAsync(id).ConfigureAwait(false);
            return NoContent();
        }
    }
}
