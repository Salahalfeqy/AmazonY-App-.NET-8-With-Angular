using AmazonY.API.Helper;
using AmazonY.Core.DTO;
using AmazonY.Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonY.API.Controllers
{

    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {

        }
        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var Product = await work.ProductRepository
                    .GetAllAsync(x => x.Category, x => x.Photos);
                var result = mapper.Map<List<ProductDTO>>(Product);
                if (Product is null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await work.ProductRepository.GetByIdAsync(id, x => x.Category, x => x.Photos);
                var result = mapper.Map<ProductDTO>(product);
                if (product is null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not find {ex.Message}");
            }
        }

        [HttpPost("Add-Product")]
        public async Task<IActionResult> Add(AddProductDTO productDTO)
        {
            try
            {
                await work.ProductRepository.AddAsync(productDTO);
                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));

            }
        }

        //[HttpPut("Update-Product/{id}")]
        //public async Task<IActionResult> Update(int id, UpdateProductDTO productDTO)
        //{
        //    try
        //    {
        //        var product = await work.ProductRepository.GetByIdAsync(id);
        //        if (product is null)
        //        {
        //            return BadRequest(new ResponseAPI(400));
        //        }
        //        mapper.Map(productDTO, product);
        //        await work.ProductRepository.UpdateAsync(product);
        //        return Ok(new ResponseAPI(200));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ResponseAPI(400, ex.Message));
        //    }
        //}
        [HttpPut("Update-Product/{id}")]
        public async Task<IActionResult> Update(UpdateProductDTO updateproductDTO)
        {
            try
            {
                await work.ProductRepository.UpdateAsync(updateproductDTO);
                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        [HttpDelete("Delete-Product/{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            try
            {
                var product = await work.ProductRepository.GetByIdAsync(id, x => x.Photos, x => x.Category);
                await work.ProductRepository.DeleteAsync(product);
                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

    }
}
