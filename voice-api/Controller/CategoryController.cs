using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using VoiceAPI.Categories;
using VoiceAPI.DbContextVoiceAPI;
using VoiceAPI.Entities;

namespace VoiceAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly VoiceAPIDbContext _context;

        public CategoryController(VoiceAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var listCategories = _context.Categories.ToList();
            return Ok(listCategories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var category = _context.Categories.SingleOrDefault(ct => ct.Id == Guid.Parse(id));
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
        public IActionResult Create(CategoryVM model)
        {
            try
            {
                var category = new Category
                {
                    Name = model.Name
                };
                _context.Add(category);
                _context.SaveChanges();
                return Ok(category);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UdateById(string id, CategoryVM model)
        {
            try
            {
                var category = _context.Categories.SingleOrDefault(ct => ct.Id == Guid.Parse(id));
                if (category != null)
                {
                    category.Name = model.Name;
                    _context.SaveChanges();
                    return Ok();
                }
                if (id != category.Id.ToString())
                {
                    return BadRequest();
                }
                return NotFound();
            }
            catch 
            { 
                return BadRequest(); 
            }
        }
    }
}
