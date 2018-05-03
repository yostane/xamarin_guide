using System;
using Microsoft.EntityFrameworkCore;
using RandomListXamarin.Model;
using System.Threading.Tasks;
namespace EntityFrameworkXamarin.Helpers
{
	public class PostDatabaseContext : DbContext
	{
		/// <summary>
		/// Manipulate the posts table
		/// </summary>
		/// <value>The property that allows to access the Posts table</value>
		public DbSet<Post> Posts { get; set; }

		#region initialization
		public static PostDatabaseContext Create()
		{

		}

		protected const string DatabasePath = "database.sqlite";

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// Specify the path of the database here
			optionsBuilder.UseSqlite($"Filename={DatabasePath}");
		}
		#endregion
	}
}
