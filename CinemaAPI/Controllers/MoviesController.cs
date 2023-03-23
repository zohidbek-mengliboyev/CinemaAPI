using CinemaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private List<Movie> movies = new List<Movie>
        {
            new Movie() { Id = 0, Name = "Mission Impossible 7", Language = "English" },
            new Movie() { Id = 1, Name = "The Matrix 4", Language = "English" },
        };

        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return movies;
        }
    }
}
