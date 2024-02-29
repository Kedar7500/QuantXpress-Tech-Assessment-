using CachingWebAPI.Data;
using CachingWebAPI.Models;
using CachingWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CachingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly ILogger<DriverController> _logger;

        private readonly ICacheService _cacheService;

        private readonly AppDbContext _appDbContext;

        public DriverController(AppDbContext appDbContext, ICacheService cacheService,ILogger<DriverController> logger)
        {
            _appDbContext = appDbContext;
            _cacheService = cacheService;
            _logger = logger;

            bool isCached = _cacheService.IsKeyCached("drivers");
            Console.WriteLine($"Is data cached: {isCached}");
        }

        [HttpGet("drivers")]
        public async Task<IActionResult> Get()
        {
            // check cache data
            var cacheData = _cacheService.GetData<IEnumerable<Driver>>("drivers");

            if(cacheData != null && cacheData.Count()>0) 
            {
                return Ok(cacheData);
            }

            cacheData=await _appDbContext.Drivers.ToListAsync();

            // set expiry time

            var expiryTime= DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<IEnumerable<Driver>>("drivers",cacheData,expiryTime);

            return Ok(cacheData);

        }

        [HttpPost("AddDriver")]
        public async Task<IActionResult> Post(Driver value)
        {
            value.Id = 0;
            var addedObject= await _appDbContext.Drivers.AddAsync(value);

            var expiryTime = DateTimeOffset.Now.AddSeconds(30);

            _cacheService.SetData<Driver>($"driver{addedObject.Entity.Id}", addedObject.Entity, expiryTime);

            await _appDbContext.SaveChangesAsync();

            return Ok(addedObject.Entity);
        }

        [HttpDelete("DeleteDriver")]
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await _appDbContext.Drivers.FirstOrDefaultAsync(x => x.Id == id);

            if(exist != null)
            {
                _appDbContext.Drivers.Remove(exist);
                await _appDbContext.SaveChangesAsync();
                _cacheService.RemoveData($"driver{id}");

               // await _appDbContext.SaveChangesAsync();

                return NoContent();
            }

            return NotFound();
        }

        [HttpGet("IsDataCached")]
        public IActionResult IsDataCached(string key)
        {
            var isCached = _cacheService.IsKeyCached(key);
            return Ok(new { IsCached = isCached });
        }

    }
}
