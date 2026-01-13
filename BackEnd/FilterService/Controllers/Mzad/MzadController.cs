using FilterService.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilterService.Controllers.Mzad
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

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult> Search([FromQuery] string searchTerm, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            var result = await _mzadService.SearchMzad(searchTerm, pageSize, pageNumber);
            if(result is null) return NotFound();
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAllAsync()
        {
            try
            {
                await _mzadService.DeleteAllAssync();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
            
        }
    }
}
