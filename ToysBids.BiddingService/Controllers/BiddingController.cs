using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToysBids.BiddingService.Models;

namespace ToysBids.BiddingService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BiddingController : ControllerBase
    {
        // GET: api/Auctions/5
        [HttpPost("bidding")]
        public async Task<ActionResult<Auction>> Bidding([FromBody] Auction auction)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return BadRequest(new { message = "You shall not pass" });
            }

            Auction result = new Auction() { MinimumAmount = auction.MinimumAmount + 1000, auctionBundleId = 1000 };
            //var exchange = await _context.Publication.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return new ObjectResult(result);
        }
    }
}