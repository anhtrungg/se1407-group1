using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Helpers;
using VoiceAPI.IServices;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Payload.Categories;
using VoiceAPI.Models.Responses.Categories;

namespace VoiceAPI.Controllers
{
    [Route("api/v{version:apiVersion}/category")]
    [ApiController]
    [ApiVersion("1")]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITools _tools;

        private readonly ICategoryService _categoryService;
        public CategoryController(IMapper mapper, ITools tools, ICategoryService categoryService)
        {
            _mapper = mapper;
            _tools = tools;
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.GetById(id);
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<CategoryDetailDTO>(result.Data));
            Response.StatusCode = result.StatusCode;
            var response = _mapper.Map<ErrorResponse>(result);
            return StatusCode(result.StatusCode, response);
        }

        [HttpPost]
        [MapToApiVersion("1")]
        public async Task<IActionResult> CreateNew(CategoryCreatePayload payloads)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryService.CreateNew(payloads);
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.Created,
                    new SingleObjectResponse<CategoryDTO>(result.Data));
            Response.StatusCode = result.StatusCode;
            var response = _mapper.Map<ErrorResponse>(result);
            return StatusCode(result.StatusCode, response);
        }

        [HttpGet]
        [MapToApiVersion("1")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllWithSubCategories()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.GetAllWithSubCategories();
            if (result.IsSuccess)
                return Ok(new MultiObjectResponse<CategoryDetailDTO>(result.Data));
            Response.StatusCode = result.StatusCode;
            var response = _mapper.Map<ErrorResponse>(result);
            return StatusCode(result.StatusCode, response);
        }

        //[HttpPut]
        //[MapToApiVersion("1")]
        //public async Task<IActionResult> UpdateCategory(CategoryCreatePayload payloads)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var result = await _categoryService.UpdateCategory(payloads);
        //    if (result.IsSuccess)
        //        return StatusCode((int)HttpStatusCode.Created,
        //            new SingleObjectResponse<CategoryDetailDTO>(result.Data));
        //    Response.StatusCode = result.StatusCode;
        //    var response = _mapper.Map<ErrorResponse>(result);
        //    return StatusCode(result.StatusCode, response);
        //}
    }
}
