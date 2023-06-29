using CoffeeShop.Models;
using CoffeeShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace CoffeeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class GenreController : Controller
    {  
        private IGenreRepository _genreRepository;
        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public IActionResult Index()
        {
            List<Genre> lst = _genreRepository.GetAll();
            return View(lst);
        }
        public IActionResult Genres()
        {
            List<Genre> lst = _genreRepository.GetAll();
            return View("Genres", lst);
        }
        public IActionResult CreateGenre()
        {
            return View("CreateGenre", new Genre());
        }
        public IActionResult EditGenre(int id)
        {
            return View("EditGenre", _genreRepository.FindById(id));
        }

        [HttpPost]
        public IActionResult UpdateGenre(Genre genre)
        {
            if (ModelState.IsValid)
            {
                bool isGenreNameExist = _genreRepository.CheckNameGenre(genre.GenreName);
                if (isGenreNameExist)
                {
                    ModelState.AddModelError(string.Empty, "Genre name is exist!!!");
                    return View("EditGenre");
                }
                else
                {
                    _genreRepository.Update(genre);
                    return RedirectToAction("Index", "Genre");
                }
            }
            else
            {
                return View("EditGenre");
            }
            
        }
        public IActionResult DeleteGenre(int id)
        {
            _genreRepository.Delete(id);
            return RedirectToAction("Index", "Genre");
        }
        [HttpPost]
        public IActionResult SaveGenre(Genre genre)
        {
            if (ModelState.IsValid)
            {
                bool isGenreNameExist = _genreRepository.CheckNameGenre(genre.GenreName);
                if (isGenreNameExist)
                {
                    ModelState.AddModelError(string.Empty, "Genre name is exist!!!");
                    return View("CreateGenre");
                }
                else
                {
                    _genreRepository.Create(genre);
                    return RedirectToAction("Index", "Genre");
                }
            }
            else
            {
                return View("CreateGenre");
            }
        }
    }
}
