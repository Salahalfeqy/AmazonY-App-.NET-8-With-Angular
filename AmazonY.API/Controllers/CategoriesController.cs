using AmazonY.API.Helper;
using AmazonY.Core.DTO;
using AmazonY.Core.Entities.Product;
using AmazonY.Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonY.API.Controllers
{
    
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var category = await work.CategoryRepository.GetAllAsync();
                if (category == null) 
                    return BadRequest(new ResponseAPI(400)); 
                return Ok(category);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id) {
            try
            {
                var category = await work.CategoryRepository.GetByIdAsync(id);
                if(category is null)
                    return BadRequest(new ResponseAPI(400, $"NOT FOUND CATEGORY ID ={id}"));
                return Ok(category);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-category")]
        public async Task<IActionResult> add(CategoryDTO categoryDTO)
        {
            try
            {
                var category = mapper.Map<Category>(categoryDTO);
                 await work.CategoryRepository.AddAsync(category);
                //return Ok(new {message =" Item has been added"});
                return Ok(new ResponseAPI(200, "Item has been added"));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-category")]
        public async Task<IActionResult> Update(UpdateCategoryDTO categoryDTO) 
        {
            try
            {
                var category = mapper.Map<Category>(categoryDTO);
                await work.CategoryRepository.UpdateAsync(category);
                //return Ok(new { message = "Item has been Updated" });
                return Ok(new ResponseAPI(200, "Item has been Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpDelete("delete-category")]
        public async Task<IActionResult> dalete(int id)
        {
            try
            {
                await work.CategoryRepository.DeleteAsync(id);
                //return Ok(new { message = " Item has been deleted" });
                return Ok(new ResponseAPI(200, "Item has been deleted"));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
       

        

    }
}
