using Entities;
using Interface;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private ICharacterRepository _character;
        private IGenderRepository _gender;
        private IMovieRepository _movie;
        private ISeriesRepository _series;
        private IUserRepository _user;

        public ICharacterRepository Character
        {
            get
            {
                if (_character == null)
                {
                    _character = new CharacterRepository(_repoContext);
                }
                return _character;
            }
        }
        public IGenderRepository Gender
        {
            get
            {
                if (_gender == null)
                {
                    _gender = new GenderRepository(_repoContext);
                }
                return _gender;
            }
        }
        public IMovieRepository Movie
        {
            get
            {
                if (_movie == null)
                {
                    _movie = new MovieRepository(_repoContext);
                }
                return _movie;
            }
        }
        public ISeriesRepository Series
        {
            get
            {
                if (_series == null)
                {
                    _series = new SeriesRepository(_repoContext);
                }
                return _series;
            }
        }
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }
        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}