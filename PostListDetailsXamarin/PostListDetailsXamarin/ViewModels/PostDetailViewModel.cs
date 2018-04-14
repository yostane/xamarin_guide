using System;
using RandomListXamarin.ViewModels;
using RandomListXamarin.Model;
namespace PostListDetailsXamarin.ViewModels
{
    public class PostDetailViewModel : BaseViewModel
    {
        private Post post;
        public Post Post
        {
            get => post;
            set => SetProperty(ref post, value);
        }

        public PostDetailViewModel(Post post)
        {
            this.Post = post;
        }
    }
}
