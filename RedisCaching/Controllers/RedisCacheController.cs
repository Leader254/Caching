using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace RedisCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisCacheController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IConfiguration _configuration;

        public RedisCacheController(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer, IConfiguration configuration)
        {
            this._distributedCache = distributedCache;
            this._redisConnection = connectionMultiplexer;
            _configuration = configuration;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCachedKeysAndValues()
        {
            try
            {
                var server = _redisConnection.GetServer(_redisConnection.GetEndPoints().First());

                var keys = server.Keys().ToArray();

                string instanceName = _configuration["RedisCacheOptions:InstanceName"] ?? string.Empty;
                var cacheKeys = new List<string>(); 

                foreach (var key in keys)
                {
                    var keyWithoutPrefix = key.ToString().Replace($"{instanceName}", "");
                    cacheKeys.Add(keyWithoutPrefix);
                }

                return Ok(new { data = cacheKeys });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {message = "Failed to retrieve cache entries", error = ex.Message});
            }
        }
        [HttpGet("{key}")]
        public async Task<IActionResult> GetCacheEntryByKey(string key)
        {
            try
            {
                var value = await _distributedCache.GetStringAsync(key);
                if (value == null)
                {
                    return NotFound(new { message = "Cache entry not found" });
                }
                return Ok(new { Key = key, Value = value });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to retrieve cache entry", error = ex.Message });
            }
        }

        [HttpDelete("all")]
        public IActionResult ClearCacheEntries()
        {
            try
            {
                var server = _redisConnection.GetServer(_redisConnection.GetEndPoints().First());

                var db = _redisConnection.GetDatabase();
                foreach (var key in server.Keys())
                {
                    string instanceName = _configuration["RedisCacheOptions:InstanceName"] ?? string.Empty;
                    var keyWithoutPrefix = key.ToString().Replace($"{instanceName}:", "");
                    db.KeyDelete(key);
                }
                return Ok(new { message = "All cache entries cleared." });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { message = "Failed to clear cache", error = ex.Message });
            }
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> ClearCacheEntryByKey(string key)
        {
            try
            {
                await _distributedCache.RemoveAsync(key);
                return Ok(new { message = $"Cache entry '{key} cleared'" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to clear cache entry", error = ex.Message });
            }
        }
    }
}
