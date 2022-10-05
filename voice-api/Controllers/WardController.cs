using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using Twilio.Http;
using VoiceAPI.Helpers;
using VoiceAPI.IServices;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Payload.Accounts;
using VoiceAPI.Models.Payload.Provinces;
using VoiceAPI.Models.Payload.Wards;
using VoiceAPI.Models.Responses.Accounts;
using VoiceAPI.Models.Responses.Provinces;
using VoiceAPI.Models.Responses.Wards;
using VoiceAPI.Services;

namespace VoiceAPI.Controllers
{
    [Route("api/v{version:apiVersion}/Wards")]
    [ApiController]
    [ApiVersion("1")]
    public class WardController : ControllerBase
    {
        private readonly IMapper _mapper;
        

        private readonly IWardService _wardService;
        public WardController(IMapper mapper,
            IWardService wardService)
        {
            _mapper = mapper;
            _wardService = wardService;
        }
        [HttpGet]
        [MapToApiVersion("1")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _wardService.GetAll();

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new MultiObjectResponse<WardDTO>(result.Data));

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
            var result = await _wardService.GetById(code);
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<WardDTO>(result.Data));
            Response.StatusCode = result.StatusCode;
            var response = _mapper.Map<ErrorResponse>(result);
            return StatusCode(result.StatusCode, response);
        }
        [HttpGet("{name}")]
        [MapToApiVersion("1")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _wardService.GetById(name);
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<WardDTO>(result.Data));
            Response.StatusCode = result.StatusCode;
            var response = _mapper.Map<ErrorResponse>(result);
            return StatusCode(result.StatusCode, response);
        }
        [HttpPost]
        [MapToApiVersion("1")]
        public async Task<IActionResult> CreateNew(List<WardCreatePayload> payloads)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _wardService.CreateAllNew(payloads);

            // Return with statusCode=201 and data if success
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.Created,
                    new SingleObjectResponse<WardDTO>(result));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
    }
}

