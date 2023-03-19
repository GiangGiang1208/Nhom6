using System;
using System.Collections.Generic;

namespace TinTucCongNghe.Models
{
    public partial class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        public int CatId { get; set; }
        public string? CatName { get; set; }
        public string? Alias { get; set; }
        public bool IsPublish { get; set; }
        public int? Ordering { get; set; }
        public string? Thumb { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
