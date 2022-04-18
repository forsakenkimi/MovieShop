using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PurchaseMovieCardModel
    {
        public MovieCardModel movieCard { get; set; }

        public int purchaseId { get; set; }

        public int UserId { get; set; }

        public String PurchaseNumber { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime PurchaseDateTime { get; set; }
    }
}
