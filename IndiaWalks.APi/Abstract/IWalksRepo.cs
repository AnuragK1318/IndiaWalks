using IndiaWalks.APi.Domain;

namespace IndiaWalks.APi.Abstract
{
    public interface IWalksRepo
    {
        Task<Walks> createAsync(Walks walks);
        Task<List<Walks>> getAllWalksAsync();
        Task<Walks> getWalksByIdAsync(int id);
        Task<Walks> updateWalkAync(int id, Walks walk);
        Task<Walks> deleteWalkAsync(int id);
    }
}
