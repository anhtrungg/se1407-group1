using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using VoiceAPI.Helpers;
using VoiceAPI.IServices;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Data.Reviews;
using VoiceAPI.Models.Payload.Categories;
using VoiceAPI.Models.Payload.Reviews;
using VoiceAPI.Models.Responses.Categories;
using VoiceAPI.Models.Responses.Reviews;
using VoiceAPI.Services;

namespace VoiceAPI.Controllers
{
    [Route("api/v{version:apiVersion}/review")]
    [ApiController]
    [ApiVersion("1")]
    public class ReviewController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITools _tools;

        private readonly IReviewService _reviewService;
        public ReviewController(IMapper mapper, ITools tools, IReviewService reviewService)
        {
            _mapper = mapper;
            _tools = tools;
            _reviewService = reviewService;
        }

        [HttpPost]
        [MapToApiVersion("1")]
        [Authorize(Roles = "ENTERPRISE")]
        public async Task<IActionResult> EnterpriseCreateReview(ReviewEnterpriseCreateDataModel dataModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _reviewService.EnterpriseCreateReview(dataModel);
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.Created,
                    new SingleObjectResponse<ReviewDTO>(result.Data));
            Response.StatusCode = result.StatusCode;
            var response = _mapper.Map<ErrorResponse>(result);
            return StatusCode(result.StatusCode, response);
        }
    }
}
