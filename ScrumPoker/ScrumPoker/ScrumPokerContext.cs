using System.Data.Entity;
using ScrumPoker.Models;

namespace ScrumPoker
{
    public class ScrumPokerContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Issue> Issues { get; set; }
    }
}