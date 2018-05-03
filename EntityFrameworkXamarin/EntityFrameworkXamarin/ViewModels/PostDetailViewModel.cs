using RandomListXamarin.ViewModels;
using RandomListXamarin.Model;

namespace PostListDetailsXamarin.ViewModels
{
	public class PostDetailViewModel : BaseViewModel
	{
		private Post _post;
		public Post Post
		{
			get => _post;
			set
			{
				SetProperty(ref _post, value);
			}
		}
	}
}
