using CinemaAPI.Data;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private CinemaDbContext _dbContext;
        public MoviesController(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<MoviesController>
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return _dbContext.Movies;
        }

        // GET api/<MoviesController>/5
        [HttpGet("{id}")]
        public Movie Get(int id)
        {
            var movie = _dbContext.Movies.Find(id);
            return movie;
        }

        // POST api/<MoviesController>
        //[HttpPost]
        //public void Post([FromBody] Movie movie)
        //{
        //    _dbContext.Movies.Add(movie);
        //    _dbContext.SaveChanges();
        //}

        [HttpPost]
        public IActionResult Post([FromForm] Movie movieObj)
        {
            
            var guid = Guid.NewGuid();
            var filePath = Path.Combine("wwwroot", guid + ".jpg");
            if (movieObj.Image != null)
            {
                var fileStream = new FileStream(filePath, FileMode.Create);
                movieObj.Image.CopyTo(fileStream);
            }
            _dbContext.Movies.Add(movieObj);
            _dbContext.SaveChanges();
            return Ok();
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Movie movieObj)
        {
            var movie = _dbContext.Movies.Find(id);
            movie.Name = movieObj.Name;
            movie.Language = movieObj.Language;
            movie.Rating = movieObj.Rating;
            _dbContext.SaveChanges();
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var movie = _dbContext.Movies.Find(id);
            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();
        }
    }
}
