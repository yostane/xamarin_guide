using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using RandomListXamarin.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xamarin.Forms.Internals;
using System.Collections.Immutable;
namespace EntityFrameworkXamarin.Helpers
{
	public class PostDatabaseHelper<T> where T : PostDatabaseContext
	{

		protected PostDatabaseContext CrateContext()
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
				// add posts that do not exist in the database
				var newPosts = posts.Where(
					post => context.Posts.Any(dbPost => dbPost.Id == post.Id) == false
				);
				await context.Posts.AddRangeAsync(newPosts);
				await context.SaveChangesAsync();
			}
		}

		public async Task<List<Post>> getPostsAsync()
		{
			using (var context = CrateContext())
			{
				//We use OrderByDescending because Posts are generally displayed from most recent to oldest
				return await context.Posts
									.AsNoTracking()
									.OrderByDescending(post => post.Id)
									.ToListAsync();
			}
		}
		#endregion
	}
}
