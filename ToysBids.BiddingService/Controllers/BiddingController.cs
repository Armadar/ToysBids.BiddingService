﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToysBids.BiddingService.Models;

namespace ToysBids.BiddingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiddingController : ControllerBase
    {
        // GET: api/Auctions/5
        [HttpPost("bidding")]
        public async Task<ActionResult<Auction>> Bidding([FromForm] Auction auction)
        {
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