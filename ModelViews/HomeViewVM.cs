using TinTucCongNghe.Models;

namespace TinTucCongNghe.ModelViews
{
    public class HomeViewVM
    {
        public List<Post> TinHots { get; set; }
        public List<Post> TinMois1 { get; set; }
        public List<Post> TinMois2 { get; set; }
        public List<PostHomeVM> Posts { get; set; }
        public List<Adv> QuangCaos { get; set; }

    }
}
