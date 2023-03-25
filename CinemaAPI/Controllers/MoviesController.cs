using CinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private static List<Movie> movies = new List<Movie>
        {
            new Movie() { Id = 0, Name = "Mission Impossible 7", Language = "English" },
            new Movie() { Id = 1, Name = "The Matrix 4", Language = "English" },
        };

        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return movies;
        }

        [HttpPost]
        public void Post([FromBody] Movie movie)
        {
            movies.Add(movie);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Movie movie)
        {
            movies[id] = movie;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            movies.RemoveAt(id);
        }

    }
}
