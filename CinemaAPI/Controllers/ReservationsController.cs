using CinemaAPI.Data;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private CinemaDbContext _dbContext;

        public ReservationsController(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post(Reservation reservationObj)
        {
            reservationObj.ReservationTime = DateTime.Now;
            _dbContext.Reservations.Add(reservationObj);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetReservations()
        {
            var reservations = _dbContext.Reservations
                                .Join(
                                        _dbContext.Users,
                                        r => r.UserId,
                                        c => c.Id,
                                        (r, c) => new { r, c }
                                     )
                                .Join(
                                        _dbContext.Movies,
                                        rs => rs.r.MovieId,
                                        m => m.Id,
                                        (rs, m) => new { rs.r, rs.c, m }

                                     )
                                .Select(rsv => new
                                {
                                    Id = rsv.r.Id,
                                    ReservationTime = rsv.r.ReservationTime,
                                    CustomerName = rsv.c.Name,
                                    MovieName = rsv.m.Name
                                });

            return Ok(reservations);
        }

    }
}
