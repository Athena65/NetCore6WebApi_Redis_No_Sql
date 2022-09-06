using Microsoft.AspNetCore.Mvc;
using Reddis_NetCore6.Models;
using Reddis_NetCore6.Services;

namespace Reddis_NetCore6.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            try
            {
                return Ok(await _cacheService.GetValue(key));
            }
            catch (Exception ex)
            {
                var response = new ServiceResponse();
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CacheRequestModel model)
        {
            try
            {
                await _cacheService.SetValue(model.Key, model.Value);
                return Ok(model.Key + " Created!");

            }
            catch (Exception ex)
            {
                var response = new ServiceResponse();
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string key)
        {
            try
            {
                await _cacheService.Clear(key);
                return Ok(key + " Deleted!");

            }
            catch (Exception ex)
            {
                var response = new ServiceResponse();
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }


    }
}
