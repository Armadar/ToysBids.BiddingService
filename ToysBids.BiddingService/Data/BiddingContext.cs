using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToysBids.BiddingService.Data
{
    public class BiddingContext : DbContext
    {
        public BiddingContext(DbContextOptions<BiddingContext> options) : base(options)
        {
        }
        public DbSet<Bid> Bid { get; set; }
    }
    public class Bid
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public long PublicationID { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}