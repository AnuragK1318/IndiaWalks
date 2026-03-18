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

        public async Task<RegionDto> AddRegionAsync(AddRegionRequestDto regionRequestDto)
        {
            //map dto to domain model
            var region=_mapper.Map<Region>(regionRequestDto);
            
            //Add tracker OR add to dbContext
            await _dbContext.AddAsync(region);

            //add the region in Database
            await _dbContext.SaveChangesAsync();

            //map the newly created domain model back to dto
            return _mapper.Map<RegionDto>(region);
        }

        public async Task<RegionDto> DeleteRegionAsync(int id)
        {
            var regionDomain=await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomain == null)
            {
                return null;
            }
            // 2. Remove from the Change Tracker
            _dbContext.Regions.Remove(regionDomain);

            //Actually delete the region
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<RegionDto>(regionDomain);
        }

        public async Task<List<RegionDto>> GetAllRegionsAsync()
        {
            var regionDomain= await _dbContext.Regions.ToListAsync();

            return _mapper.Map<List<RegionDto>>(regionDomain);
        }

        public async Task<RegionDto> GetRegionbyIdAsync(int id)
        {
            var regionDomain = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id==id);
            if (regionDomain == null)
            {
                Console.WriteLine("Unable to find region with id : {id} ");
                return null;
            }
            else
            {
                return _mapper.Map<RegionDto>(regionDomain);
            }
        }

        public async Task<RegionDto> updateRegionAsync(int id, UpdateRegionRequestDto updateRegionRequestDto)
        {
            //When you fetch existingRegion using FirstOrDefaultAsync,
            //Entity Framework starts "tracking" it. Any changes you make to
            //its properties (like existingRegion.Name = ...) are noted by EF.
            var existingRegion =await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }

            //map updateDto to domainModel
            _mapper.Map(updateRegionRequestDto,existingRegion);

            //save changes
            await _dbContext.SaveChangesAsync();

            //map again to send to frontend
            return _mapper.Map<RegionDto>(existingRegion);
        }
    }
}
