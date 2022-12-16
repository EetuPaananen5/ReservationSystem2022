using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class ItemsController : ControllerBase
    {
      // private readonly ReservationContext _context;
        private readonly IItemService _service;
        private readonly IUserAuthenticationService _authenticationService;

        public ItemsController( IItemService service, IUserAuthenticationService authenticationService)
        {
            // _context = context;     ReservationContext context,
            _service = service;
            _authenticationService = authenticationService;
        }

        // GET: api/Items
        /// <summary>
        /// Get all items from database
        /// </summary>
        /// <returns> list of items </returns>

        [HttpGet]
        [Authorize]
       // [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
        {
            return Ok(await _service.GetItemsAsync());
        }


        // GET: api/Items//user/username
        /// Get all items from database
        /// <returns> list of items </returns>

        [HttpGet("user/{username}")]
        [Authorize]
  
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems(String username)
        {    
            return Ok(await _service.GetItemsAsync(username));
        }


        // GET: api/Items/query
        /// Get all items with matcing given query
        /// <returns> list of items </returns>

        [HttpGet("{query}")]
        [Authorize]
        
        public async Task<ActionResult<IEnumerable<ItemDTO>>> QueryItems(String query)
        {
            return Ok(await _service.QueryItemsAsync(query));
        }




        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<ItemDTO>> GetItem(long id)
        {
            var item = await _service.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutItem(long id, ItemDTO item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            //tarkista onko oikeus muokata
            bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, item);
            if (!isAllowed)
            {
                return Unauthorized();
            }

            ItemDTO updatedItem = await _service.UpdateItemAsync(item);

            if(updatedItem == null)
            {
                return NotFound();
            }
            return NoContent();

            /*
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
       */ }

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        
        public async Task<ActionResult<ItemDTO>> PostItem(ItemDTO item)
        {
            //tarkista onko oikeus muokata
            bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, item);
            if (!isAllowed)
            {
                return Unauthorized();
            }

            ItemDTO newItem = await _service.CreateItemAsync(item);
            if (newItem == null)
            {
                return Problem();
            }

            return CreatedAtAction("GetItem", new { id = newItem.Id }, newItem); //luo id
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteItem(long id)
        {
            //tarkista onko oikeus muokata
            ItemDTO item = new ItemDTO();
            item.Id = id;
            bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, item);

            if (!isAllowed)
            {
                return Unauthorized();
            }

            if (await _service.DeleteItemAsync(id))
            {
                return Ok();             
            }
            return NotFound();
          
            
        }

       /* private bool ItemExists(long id)
        {
            return _context.Items.Any(e => e.Id == id);
        }*/
    }


      


}


