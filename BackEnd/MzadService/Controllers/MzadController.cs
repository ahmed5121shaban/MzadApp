using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MzadService.Application.Contracts.Mzad;
using MzadService.Application.DTOs.Mzad;

namespace MzadService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MzadController : ControllerBase
    {
        private readonly IMzadService _mzadService;
        public MzadController(IMzadService mzadService)
        {
            _mzadService = mzadService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var mzad = await _mzadService.GetById(id);
            return Ok(mzad);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mzads = await _mzadService.GetAll();
            return Ok(mzads);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MzadDto mzadDto)
        {
            var createdMzad = await _mzadService.Create(mzadDto);
            return CreatedAtAction(nameof(GetById), new { id = createdMzad.Id }, createdMzad);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMzadDto mzadDto)
        {
            if (id != mzadDto.Id)
            {
                return BadRequest("ID mismatch");
            }
            var updatedMzad = await _mzadService.Update(mzadDto);
            return Ok(updatedMzad);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mzadService.Delete(id);
            return NoContent();
        }
    }
}
