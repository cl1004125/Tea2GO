using Microsoft.EntityFrameworkCore;

namespace Tea2GO.Models
{
    public class DrinkContext : DbContext
    {
        public DrinkContext (DbContext<DrinkContext> options)
        : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>().HasKey(s => new {s.CourseID, s.StudentID});
        }
        public Dbset<Drink> Drink {get; set;}
        public Dbset<Order> Order {get; set;}
        public DbSet<DrinkOption> DrinkOption {get; set;}
    }
}