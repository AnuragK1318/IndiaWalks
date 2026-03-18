using IndiaWalks.APi.Abstract;
using IndiaWalks.APi.Context;
using IndiaWalks.APi.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace IndiaWalks.APi.Concrete
{
    public class WalksRepo : IWalksRepo
    {
        private readonly IndiaWalksDbContext _dbContext;
        public WalksRepo(IndiaWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Walks> createAsync(Walks walks)
        {
            //add walk to DB
            await _dbContext.AddAsync(walks);
            //Save changes to DB
            await _dbContext.SaveChangesAsync();
            //return walks domain
            return walks;
        }

        public async Task<List<Walks>> getAllWalksAsync()
        {
            return await _dbContext.Walks
                .Include("Region")
                .Include("Difficulty")
                .ToListAsync();
        }

        public async Task<Walks> getWalksByIdAsync(int id)
        {
            var walksDomain= await _dbContext.Walks
                .Include("Region")
                .Include("Difficulty")
                .FirstOrDefaultAsync(x => x.Id == id);

            if(walksDomain == null)
            {
                return null;
            }
            return walksDomain;
            
        }

        public async Task<Walks> updateWalkAync(int id, Walks walk)
        {
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = walk.Name;
            existingWalk.Length = walk.Length;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.DifficultyId = walk.DifficultyId;

            await _dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<Walks> deleteWalkAsync(int id)
        {
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if(existingWalk == null)
            {
                return null;
            }
            _dbContext.Walks.Remove(existingWalk);
            _dbContext.SaveChanges();
            return existingWalk;
        }
    }
}
