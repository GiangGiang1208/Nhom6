using TinTucCongNghe.Models;

namespace TinTucCongNghe.ModelViews
{
    public class PostHomeVM
    {
        public Category category { get; set; }
        public List<Post> lsPosts { get; set; }
    }
}
