using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.API.Interfaces.Repository;
using Product.API.Models;
using Product.API.Models.Dtos;
using System.Collections.Generic;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("ClientIdPolicy")]
    public class PiesController : ControllerBase
    {

        private readonly IPieRepository _pieRepository;
        private readonly IMapper _mapper;

        public PiesController(IPieRepository pieRepository, IMapper mapper)
        {
            _pieRepository = pieRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult GetPies()
        {

            var pieList = _pieRepository.GetPies();
            var pieDto = new List<PieDto>();
            foreach (var pie in pieList)
            {
                pieDto.Add(_mapper.Map<PieDto>(pie));
            }
            return Ok(pieDto);

        }


        [HttpGet("{id}")]
        public ActionResult GetPie(int id)
        {
            var pie = _pieRepository.GetPie(id);
            if (pie == null)
            {
                return NotFound();
            }
            var pieDto = _mapper.Map<PieDto>(pie);

            return Ok(pieDto);
        }



        [HttpPut("{id}")]
        public
            IActionResult UpdatePie(int id, [FromBody] PieDto pieDto)
        {
            if (pieDto == null || id != pieDto.Id)
            {
                return BadRequest(ModelState);
            }

            var pie = _mapper.Map<Pie>(pieDto);
            if (!_pieRepository.UpdatePie(pie))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {pie.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


        [HttpPost]
        public ActionResult AddPie([FromBody] PieDto pieDto)
        {
            if (pieDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_pieRepository.PieExists(pieDto.Id))
            {
                ModelState.AddModelError("", "Pie Exists!");
                return StatusCode(404, ModelState);
            }
            var pie = _mapper.Map<Pie>(pieDto);
            if (!_pieRepository.AddPie(pie))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {pie.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("AddPie", new { pieId = pie.Id }, pie);

        }


        [HttpDelete("{id}")]
        public IActionResult DeletePie(int id)
        {
            if (!_pieRepository.PieExists(id))
            {
                return NotFound();
            }

            var pie = _pieRepository.GetPie(id);
            if (!_pieRepository.DeletePie(pie))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {pie.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


    }

}

