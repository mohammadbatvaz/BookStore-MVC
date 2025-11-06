using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class NewCategoryViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string EmojiIcon { get; set; }
        public string BackgroundColorHEX { get; set; }
    }
}
