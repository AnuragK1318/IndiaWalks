using IndiaWalks.APi.Domain;
using IndiaWalks.APi.DTOs;

namespace IndiaWalks.APi.Abstract
{
    public interface IRegion
    {
        Task<List<Region>> GetAllRegionsAsync(RegionListRequestDto filter);
        Task<Region> GetRegionbyIdAsync(int id);
        Task<Region> AddRegionAsync(Region region);
        Task<Region> updateRegionAsync(int id,Region region);
        Task<Region> DeleteRegionAsync(int id);
    }
}
