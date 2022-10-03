using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using VoiceAPI.Provinces;


namespace VoiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        public static List<ProvinceVM> provinces = new List<ProvinceVM>();
        private object pv;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(provinces);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string ID)
        {
            try
            {
                var ProVince = provinces.SingleOrDefault(pv => pv.id == Guid.Parse(ID));
                if (ProVince == null)
                {
                    return NotFound();
                }
                return Ok(ProVince);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult Create(ProvinceVM provinceVM)
        {
            var province = new ProvinceVM
            {
                name = provinceVM.name,
                code = provinceVM.code
            };
            provinces.Add(province);
            return Ok(new
            {
                success = true,
                Data = province
            });
        }
        [HttpPut("{id}")]
        public IActionResult Edit(string Id, ProvinceVM provinceEdit )
        {
            try
            {
                var ProVince = provinces.SingleOrDefault(pv => pv.id == Guid.Parse(Id));
                if (ProVince == null)
                {
                    return NotFound();
                }
                if(Id != ProVince.name.ToString())
                ProVince.name = provinceEdit.name;
                ProVince.code = provinceEdit.code;
                return Ok();


            }
            catch
            {
                return BadRequest();
            }
        }
    }
}