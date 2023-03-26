using CinemaAPI.Data;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
    }
}
