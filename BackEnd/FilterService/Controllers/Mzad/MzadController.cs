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
        public async Task<ActionResult> Search([FromQuery] string search)
        {
            var result = await _mzadService.SearchMzad(search);
            if(result is null) return NotFound();
            return Ok(result);
        }
    }
}
