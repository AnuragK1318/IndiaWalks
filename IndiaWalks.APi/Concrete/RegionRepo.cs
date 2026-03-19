using AutoMapper;
using IndiaWalks.APi.Abstract;
using IndiaWalks.APi.Context;
using IndiaWalks.APi.Domain;
using IndiaWalks.APi.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace IndiaWalks.APi.Concrete
{
    public class RegionRepo : IRegion
    {
        private readonly IndiaWalksDbContext _dbContext;
        private readonly IMapper _mapper;
        public RegionRepo(IndiaWalksDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper= mapper;
            
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            var regionDomain= await _dbContext.Regions.ToListAsync();

            return regionDomain;
        }

        public async Task<Region> GetRegionbyIdAsync(int id)
        {
            var regionDomain = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomain == null)
            {
                return null;
            }
                return regionDomain;
        }

        public async Task<Region> AddRegionAsync(Region regionRequestDto)
        {

            //Add tracker OR add to dbContext
            await _dbContext.AddAsync(regionRequestDto);

            //add the region in Database
            await _dbContext.SaveChangesAsync();

            //map the newly created domain model back to dto
            return regionRequestDto;
        }

        public async Task<Region> updateRegionAsync(int id, Region updateRegionRequestDto)
        {
            //When you fetch existingRegion using FirstOrDefaultAsync,
            //Entity Framework starts "tracking" it. Any changes you make to
            //its properties (like existingRegion.Name = ...) are noted by EF.
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Name = updateRegionRequestDto.Name;
            existingRegion.Code = updateRegionRequestDto.Code;
            existingRegion.Area = updateRegionRequestDto.Area;
            existingRegion.Lat = updateRegionRequestDto.Lat;
            existingRegion.Long = updateRegionRequestDto.Long;
            existingRegion.Population = updateRegionRequestDto.Population;

            //save changes
            await _dbContext.SaveChangesAsync();

            return existingRegion;
        }

        public async Task<Region> DeleteRegionAsync(int id)
        {
            var regionDomain = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain == null)
            {
                return null;
            }
            // 2. Remove from the Change Tracker
            _dbContext.Regions.Remove(regionDomain);

            //Actually delete the region
            await _dbContext.SaveChangesAsync();

            return regionDomain;
        }
    }
}
