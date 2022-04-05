using System.Threading.Tasks;

namespace Interface
{
    public interface IRepositoryWrapper
    {
        ICharacterRepository Character { get; }
        IGenderRepository Gender { get; }
        IMovieRepository Movie { get; }
        ISeriesRepository Series { get; }
        IUserRepository User { get; }
        Task SaveAsync();
    }
}
