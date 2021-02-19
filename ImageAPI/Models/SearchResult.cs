using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageAPI.Models
{
    public class Picture
    {
        public string id { get; set; }
        public string cropped_picture { get; set; }
    }

    public class ImageSearchPaginatedResult
    {
        public List<Picture> pictures { get; set; }
        public int page { get; set; }
        public int pageCount { get; set; }
        public bool hasMore { get; set; }
    }

    public class ImageSearchDetailResult
    {
        public string id { get; set; }
        public string author { get; set; }
        public string camera { get; set; }
        public string tags { get; set; }
        public string cropped_picture { get; set; }
        public string full_picture { get; set; }

        public bool FieldMatches(string searchTerm)
        {
            return (author?.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                   (camera?.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                   (tags?.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ?? false) ;
        }
    }
}
