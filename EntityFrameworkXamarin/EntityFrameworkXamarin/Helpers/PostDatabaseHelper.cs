using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using RandomListXamarin.Model;
using Microsoft.EntityFrameworkCore;
namespace EntityFrameworkXamarin.Helpers
{
	public class PostDatabaseHelper<T> where T : PostDatabaseContext
	{
		public PostDatabaseContext CrateContext()
		{
			PostDatabaseContext postDatabaseContext = (T)Activator.CreateInstance(typeof(T));
			postDatabaseContext.Database.EnsureCreated();
			postDatabaseContext.Database.Migrate();
			return postDatabaseContext;
		}


		#region data access
		public async Task AddOrUpdatePostsAsync(List<Post> posts)
		{
			using (var context = CrateContext())
			{

			}
		}
		#endregion
	}
}
