using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using VoiceAPI.Categories;

namespace VoiceAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public static List<CategoryVM> categories = new List<CategoryVM>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(categories);
        }

        [HttpGet ("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var category = categories.SingleOrDefault(ct => ct.Id == Guid.Parse(id));
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(CategoryVM categoryVM)
        {
            var category = new CategoryVM
            {
                Id = Guid.NewGuid(),
                Name = categoryVM.Name
            };
            categories.Add(category);
            return Ok(new
            {
                Success = true,
                Data = category
            });
        }

        [HttpPut ("{id}")]
        public IActionResult Edit(string id, CategoryVM categoryEdit)
        {
            try
            {
                var category = categories.SingleOrDefault(ct => ct.Id == Guid.Parse(id));
                if (category == null)
                {
                    return NotFound();
                }
                
                if (id != category.Id.ToString())
                {
                    return BadRequest();
                }
                category.Name = categoryEdit.Name;
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
