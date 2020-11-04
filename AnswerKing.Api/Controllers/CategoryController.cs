using AnswerKing.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AnswerKing.Api.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : Controller
    {
        public ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository _categoryRepository)
        {
            this.categoryRepository = _categoryRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = this.categoryRepository.GetAll();

            return Ok(categories);
        }

        [HttpPost]
        [Route("{categoryName}")]
        public IActionResult Create(string categoryName)
        {
            var category = this.categoryRepository.Create(categoryName);

            if(category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        [Route("{itemId}/{categoryId}")]
        public IActionResult AddItemToCategory(long itemId, long categoryId)
        {
            var item = this.categoryRepository.AddItemToCategory(itemId, categoryId);

            if(item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpDelete]
        [Route("{itemId}/{categoryId}")]
        public IActionResult RemoveItemFromCategory(long itemId, long categoryId)
        {
            var item = this.categoryRepository.RemoveItemFromCategory(itemId, categoryId);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPut]
        [Route("{categoryId}/{categoryName}")]
        public IActionResult Edit(long categoryId, string categoryName)
        {
            var category = this.categoryRepository.Edit(categoryId, categoryName);

            if(category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpDelete]
        [Route("{categoryId}")]
        public IActionResult Delete(long categoryId)
        {
            var category = this.categoryRepository.Delete(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
    }
}
