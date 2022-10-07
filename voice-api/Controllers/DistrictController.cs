using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using VoiceAPI.Helpers;
using VoiceAPI.IServices;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Data.Enterprises;
using VoiceAPI.Models.Payload.Accounts;
using VoiceAPI.Models.Payload.Districts;
using VoiceAPI.Models.Payload.Enterprises;
using VoiceAPI.Models.Responses.Accounts;
using VoiceAPI.Models.Responses.Categories;
using VoiceAPI.Models.Responses.Districts;
using VoiceAPI.Models.Responses.Enterprises;
using VoiceAPI.Services;

namespace VoiceAPI.Controllers
{
    [Route("api/v{version:apiVersion}/district")]
    [ApiController]
    [ApiVersion("1")]
     public class DistrictController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITools _tools;

        private readonly IDistrictService _districtService;
        public DistrictController(IMapper mapper, ITools tools,
            IDistrictService districtService)
        {
            _mapper = mapper;  
            _tools = tools;
            _districtService = districtService;
        }
        [HttpGet]
        [MapToApiVersion("1")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _districtService.GetAll();

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new MultiObjectResponse<DistrictDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
        [HttpGet("{id}")]
        [MapToApiVersion("1")]
        public async Task<IActionResult> GetById(String id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _districtService.GetById(id);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<DistrictDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
        [HttpGet("{name}")]
        [MapToApiVersion("1")]
        public async Task<IActionResult> GetByName(String name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _districtService.GetByName(name);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<DistrictDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
        [HttpPost]
        [MapToApiVersion("1")]
        public async Task<IActionResult> CreateAllNew(List<DistrictCreatePayload> payloads)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _districtService.CreateAllNew(payloads);

            // Return with statusCode=201 and data if success
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.Created,
                    new MultiObjectResponse<DistrictDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
        //[HttpPut]
        //[MapToApiVersion("1")]
        //[Authorize(Roles = "DISTRICT")]
        //public async Task<IActionResult> Update(DistrictCreatePayload payload)
        //{
        //    var id = _tools.GetUserOfRequest(User.Claims);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var dataModel = _mapper.Map<DistrictUpdateDataModel>(payload);
        //    dataModel.Id = Guid.Parse(id);

        //    var result = await _districtService.UpdateProfile(dataModel);

        //    // Return with statusCode=200 and data if success
        //    if (result.IsSuccess)
        //        return Ok(new SingleObjectResponse<DistrictDTO>(result.Data));

        //    // Add error response data informations
        //    Response.StatusCode = result.StatusCode;

        //    var response = _mapper.Map<ErrorResponse>(result);

        //    return StatusCode(result.StatusCode, response);
        //}
    }
}
