using CoffeeShop.Models;

namespace CoffeeShop.Repository
{
    public interface IGenreRepository
    {
        public bool Create(Genre genre);
        public bool Update(Genre genre);
        public bool Delete(int Id);
        public List<Genre> GetAll();
        public Genre FindById(int id);

        public bool CheckNameGenre(string name);
    }
    public class GenreRepository : IGenreRepository
    {
        private OderDrinkingContext _ctx;
        public GenreRepository(OderDrinkingContext ctx)
        {
            _ctx = ctx;
        }

        public bool CheckNameGenre(string name)
        {
            Genre g = _ctx.Genres.Where(x => x.GenreName.Trim() == name.Trim()).FirstOrDefault();
            if (g == null)
                return false;
            else
                return true;
        }

        public bool Create(Genre genre)
        {
            _ctx.Genres.Add(genre);
            _ctx.SaveChanges();
            return true;
        }

        public bool Delete(int Id)
        {
            Genre g = _ctx.Genres.FirstOrDefault(x => x.GenreId == Id);
            _ctx.Genres.Remove(g);
            _ctx.SaveChanges();
            return true;
        }

        public Genre FindById(int id)
        {
            Genre g = _ctx.Genres.FirstOrDefault(x => x.GenreId == id);
            return g;
        }

        public List<Genre> GetAll()
        {
            return _ctx.Genres.ToList();
        }

        public bool Update(Genre genre)
        {
            Genre g = _ctx.Genres.FirstOrDefault(x => x.GenreId == genre.GenreId);
            if (g != null)
            {
                _ctx.Entry(g).CurrentValues.SetValues(genre);
                _ctx.SaveChanges();
            }
            return true;
        }
    }
}
