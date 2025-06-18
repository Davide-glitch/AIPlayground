using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPlayground.BusinessLogic.DTOs
{
    public class RunUserRatingUpdateDto
    {
        public int RunId { get; set; }
        public int UserRating { get; set; }
    }
}
