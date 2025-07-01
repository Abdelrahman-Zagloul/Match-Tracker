using MatchTracker.Models;

namespace MatchTracker.Repository.Interfaces
{
    public interface IStadiumRepository
    {
        void Add(Stadium stadium);
        void Update(int id, Stadium newStadium);
        bool Delete(int id);
        bool Clear();
        Stadium? GetById(int id);
        List<Stadium> GetAll();
        List<Team> GetTeamsWithoutStadium();


    }
}
