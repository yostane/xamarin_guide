using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RandomListXamarin.Model;
using Xamarin.Forms;
namespace EntityFrameworkXamarin.Helpers {
	public class PostDatabaseContext : DbContext {
		/// <summary>
		/// Manipulate the posts table
		/// </summary>
		/// <value>The property that allows to access the Posts table</value>
		public DbSet<Post> Posts { get; set; }

		private const string databaseName = "database.db";
		protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
			String databasePath = "";
			switch (Device.RuntimePlatform) {
				case Device.iOS:
					SQLitePCL.Batteries_V2.Init ();
					databasePath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "..", "Library", databaseName);;
					break;
				case Device.Android:
					databasePath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), databaseName);
					break;
				default:
					throw new NotImplementedException ("Platform not supported");
			}
			// Specify that we will use sqlite and the path of the database here
			optionsBuilder.UseSqlite ($"Filename={databasePath}");
		}
	}
}