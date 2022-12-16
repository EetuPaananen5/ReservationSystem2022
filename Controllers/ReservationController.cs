using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem2022.Middleware;
using ReservationSystem2022.Models;
using ReservationSystem2022.Services;

namespace ReservationSystem2022.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class ReservationController : ControllerBase
        {
            private readonly ReservationContext _context;
            private readonly IReservationService _service;
            private readonly IUserAuthenticationService _authenticationService;

            public ReservationController(ReservationContext context, IReservationService service, IUserAuthenticationService authenticationService)
            {
               _context = context;
                _service = service;
                _authenticationService = authenticationService;
            }

        //GET: api/reservstions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservation()
        {
            return Ok(await _service.GetReservationsAsync());
        }

        //GET: api/reservstions/5
        [HttpGet("{id}")]

        public async Task<ActionResult<Reservation>>GetReservation(long id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if(reservation == null)
            {
                return NotFound();
            }
            return reservation;

        }

        //PUT: api/reservstions/5
        [HttpPut("id")]
        [Authorize]

        public async Task<IActionResult> PutReservation(long id, ReservationDTO reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            //tarkista onko oikeus muokata
            bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, reservation);
            if (!isAllowed)
            {
                return Unauthorized();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id)) //Reservstionexists error heitti alas funktion
                {
                    return NotFound();
                }
                else
                {
                    throw;

                }
            }
            return NoContent();
        }

  

        //POST: api/reservations
        [HttpPost]
        [Authorize]

            public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservation)
        {
            bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, reservation);
            if (!isAllowed)
            {
                return Unauthorized();
            }
            reservation = await _service.CreateReservationAsync(reservation);
            if(reservation == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        //Delete: api/reservstions/5
        [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteReservation(long id)
        {
            var reservation = await _context.Reservations.FindAsync(id);    
            if (reservation == null)
            {
                return NotFound();
            }
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

         private bool ReservationExists(long id)
        {
            throw new NotImplementedException();
        }


    }
    }



