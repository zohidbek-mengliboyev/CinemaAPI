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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reservationObj"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public IActionResult Post(Reservation reservationObj)
        {
            reservationObj.ReservationTime = DateTime.Now;
            _dbContext.Reservations.Add(reservationObj);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetReservationDetail(int id)
        {
            var reservationResult = _dbContext.Reservations
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
                                .Where(res => res.r.Id == id)
                                .Select(rsv => new
                                {
                                    Id = rsv.r.Id,
                                    ReservationTime = rsv.r.ReservationTime,
                                    CustomerName = rsv.c.Name,
                                    MovieName = rsv.m.Name,
                                    Email = rsv.c.Email,
                                    Quantity = rsv.r.Quantity,
                                    Price = rsv.r.Price,
                                    Phone = rsv.r.Phone,
                                    PlayingDate = rsv.m.PlayingDate,
                                    PlayingTime = rsv.m.PlayingTime
                                })
                                .FirstOrDefault();

            return Ok(reservationResult);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var reservation = _dbContext.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                _dbContext.Reservations.Remove(reservation);
                _dbContext.SaveChanges();
                return Ok("Record deleted");
            }
        }

    }
}
