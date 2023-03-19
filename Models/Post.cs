using System;
using System.Collections.Generic;

namespace TinTucCongNghe.Models
{
    public partial class Post
    {
        public int PostId { get; set; }
        public int? CatId { get; set; }
        public string? Title { get; set; }
        public string? Scontent { get; set; }
        public string? Contents { get; set; }
        public string? Alias { get; set; }
        public bool IsPublish { get; set; }
        public bool IsHot { get; set; }
        public bool IsNew { get; set; }
        public bool HomeFlag { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string? Thumb { get; set; }

        public virtual Category? Cat { get; set; }
    }
}
