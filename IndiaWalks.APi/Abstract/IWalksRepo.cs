using IndiaWalks.APi.Domain;
using IndiaWalks.APi.DTOs;

namespace IndiaWalks.APi.Abstract
{
    public interface IWalksRepo
    {
        Task<Walks> createAsync(Walks walks);
        Task<List<Walks>> getAllWalksAsync(WalkListReqDto filter);  
        Task<Walks> getWalksByIdAsync(int id);
        Task<Walks> updateWalkAync(int id, Walks walk);
        Task<Walks> deleteWalkAsync(int id);
    }
}
