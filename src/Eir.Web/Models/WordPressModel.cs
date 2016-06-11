using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eir.Web.Models
{
    public class WordPressModel
    {
        [Required]
        public string WordPressPlugin { get; set; }

        public void gg()
        {
            WordPressPlugin = "afsdf";
        }
    }
}
