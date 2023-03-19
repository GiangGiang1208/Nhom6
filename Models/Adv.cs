using System;
using System.Collections.Generic;

namespace TinTucCongNghe.Models
{
    public partial class Adv
    {
        public int AdversitingId { get; set; }
        public string? Title { get; set; }
        public string? UrlLink { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? Thumb { get; set; }
        public string? Img1 { get; set; }
        public string? Img2 { get; set; }
    }
}
