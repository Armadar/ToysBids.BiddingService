using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToysBids.BiddingService.Data;
using ToysBids.BiddingService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ToysBids.BiddingService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BiddingController : ControllerBase
    {
        private readonly BiddingContext _context;

        public BiddingController(BiddingContext context)
        {
            _context = context;
        }
        // GET: api/Auctions/5
        [HttpPost("bidding")]
        public async Task<ActionResult<Auction>> Bidding([FromBody] Auction auction)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return BadRequest(new { message = "You shall not pass" });
            }
            Bid bid = Convert(auction);
            //Auction result = new Auction() { MinimumAmount = auction.MinimumAmount + 1000, auctionBundleId = 1000 };
            _context.Bid.Add(bid);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBid", new { id = auction.Id }, bid);

            /*
            if (result == null)
            {
                return NotFound();
            }

            return new ObjectResult(result);
            */
        }

        public Bid Convert(Auction from)
        {
            Bid result = new Bid();
            result.ID = from.Id;
            result.UserID = from.SellerID;
            result.PublicationID = from.auctionBundleId;
            result.Amount = from.MinimumAmount;
            result.CreatedOn = DateTime.Now;
            return result;
        }


        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Bid>> GetBid(long id)
        {
            var user = await _context.Bid.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBids()
        {
            try
            {
                return await _context.Bid.ToListAsync();
            }
            catch (Exception err)
            {
                string m = err.Message;
                throw;
            }
            
        }
    }
}