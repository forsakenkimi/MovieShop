using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class ProblemDetails
    {
        public string? type { get; set; }
        public string? title { get; set; }

        public int? status { get; set; }

        public string? detail { get; set; }

        public string? instance { get; set; }
    }
}
