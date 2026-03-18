using IndiaWalks.APi.Domain;
using IndiaWalks.APi.DTOs;

namespace IndiaWalks.APi.Abstract
{
    public interface IRegion
    {
        Task<List<RegionDto>> GetAllRegionsAsync();
        Task<RegionDto> GetRegionbyIdAsync(int id);
        Task<RegionDto> AddRegionAsync(AddRegionRequestDto regionRequestDto);
        Task<RegionDto> updateRegionAsync(int id,UpdateRegionRequestDto updateRegionRequestDto);
        Task<RegionDto> DeleteRegionAsync(int id);
    }
}
