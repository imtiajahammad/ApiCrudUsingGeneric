using ApiCrudUsingGeneric.Models;
using Microsoft.EntityFrameworkCore;
using System;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		/*base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Employee>().HasData(
			new Employee
			{
				Name = "seedName1",
				Designation = "seedPeon1",
				Age = 71
			});
		//add-migration name
		//update-database 
		//remove-migration
		*/

		modelBuilder.Seed();
		
    }

    public DbSet<Student> Students { get; set; }
	public DbSet<Teacher> Teachers { get; set; }
	public DbSet<Movie> Movies { get; set; }
	public DbSet<Notification> Notifications { get; set; }
	public DbSet<Employee> Employees { get; set; }

}
public static class ModelBuilderExtensions
{
	public static void Seed(this ModelBuilder modelBuilder)
    {
		//base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Employee>().HasData(
			new Employee
			{
				Id=1,
				Name = "seedName1",
				Designation = "seedPeon1",
				Age = 19
			});
	}
}
