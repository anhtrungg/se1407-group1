using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Helpers;
using VoiceAPI.IServices;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Data.Candidates;
using VoiceAPI.Models.Data.Enterprises;
using VoiceAPI.Models.Payload.Accounts;
using VoiceAPI.Models.Payload.Candidates;
using VoiceAPI.Models.Payload.Enterprises;
using VoiceAPI.Models.Payload.Provinces;
using VoiceAPI.Models.Responses.Accounts;
using VoiceAPI.Models.Responses.Candidates;
using VoiceAPI.Models.Responses.Enterprises;
using VoiceAPI.Models.Responses.Provinces;
using VoiceAPI.Services;

namespace VoiceAPI.Controllers
{
    [Route("api/v{version:apiVersion}/province")]
    [ApiController]
    [ApiVersion("1")]
    public class ProvinceController : ControllerBase
    {
        private readonly IMapper _mapper;
        //private readonly ITools _tools;

        private readonly IProvinceService _provinceService;
        public ProvinceController(IMapper mapper, ITools tools,
            IProvinceService provinceService)
        {
            _mapper = mapper;
            //_tools = tools;
            _provinceService = provinceService;
        }
        [HttpGet]
        [MapToApiVersion("1")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _provinceService.GetAll();

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new MultiObjectResponse<ProvinceDTO>(result.Data));

            // Add error response data information
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
        [HttpGet("{code}")]
        [MapToApiVersion("1")]
        public async Task<IActionResult> GetById(string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _provinceService.GetById(code);
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<ProvinceDTO>(result.Data));
            Response.StatusCode = result.StatusCode;
            var response = _mapper.Map<ErrorResponse>(result);
            return StatusCode(result.StatusCode, response);
        }
        [HttpPost]
        [MapToApiVersion("1")]
        public async Task<IActionResult> CreateNew(ProvinceCreatePayload payloads)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _provinceService.CreateProvince(payloads);

            // Return with statusCode=201 and data if success
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.Created,
                    new SingleObjectResponse<ProvinceDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
        [HttpPut]
        [MapToApiVersion("1")]
        public async Task<IActionResult> Update(ProvinceUpdatePayload payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _provinceService.UpdateProvince(payload);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<AccountDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
    }
}
