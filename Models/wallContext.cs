using Microsoft.EntityFrameworkCore;

namespace TheWall.Models
{  
   public class wallContext : DbContext
   {
        public wallContext(DbContextOptions<wallContext> options) : base(options){}
        public DbSet<Users> Users {get;set;}
        public DbSet<Messages> Messages {get;set;}
        public DbSet<Comments> Comments {get;set;}
   }
}