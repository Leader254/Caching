using InMemoryCachingDemo.Data;
using InMemoryCachingDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCachingDemo.Repository
{
    public class LocationRepository
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        public LocationRepository(AppDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            var cacheKey = "Countries";
            if (!_cache.TryGetValue(cacheKey, out List<Country>? countries))
            {
                countries = await _context.Countries
                    .AsNoTracking()
                    .ToListAsync();

                // priority
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetPriority(CacheItemPriority.High);
                _cache.Set(cacheKey, countries, cacheEntryOptions);
            }
            return countries ?? new List<Country>();
        }

        public void RemoveCountriesFromCache()
        {
            var cacheKey = "Countries";
            _cache.Remove(cacheKey);
        }

        public async Task AddCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            RemoveCountriesFromCache();
        }

        public async Task UpdateCountry(Country updatedCountry)
        {
            _context.Countries.Update(updatedCountry);
            await _context.SaveChangesAsync();
            RemoveCountriesFromCache();
        }

        public async Task<List<State>> GetStatesAsync(int countryId)
        {
            string cacheKey = $"States_{countryId}";
            if (!_cache.TryGetValue(cacheKey, out List<State>? states))
            {
                states = await _context.States
                    .Where(s => s.CountryId == countryId)
                    .AsNoTracking()
                    .ToListAsync();
                // Cache expiration resets after each access
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30))
                    .SetPriority(CacheItemPriority.Normal);

                _cache.Set(cacheKey, states, cacheEntryOptions);
            }
            return states ?? new List<State>();
        }

        public async Task<List<City>> GetCitiesAsync(int stateId)
        {
            string cacheKey = $"Cities_{stateId}";
            if (!_cache.TryGetValue(cacheKey, out List<City>? cities))
            {
                cities = await _context.Cities
                    .Where(c => c.StateId == stateId)
                    .AsNoTracking()
                    .ToListAsync();

                // Cache expires after a fixed duration, regardless of access frequency.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30))
                    .SetPriority(CacheItemPriority.Low);

                _cache.Set(cacheKey, cities, cacheEntryOptions);
            }
            return cities ?? new List<City>();
        }

    }
}
