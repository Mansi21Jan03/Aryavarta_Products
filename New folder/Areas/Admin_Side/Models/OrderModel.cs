

using System.ComponentModel.DataAnnotations;

namespace Aryavarta.Areas.Admin_Side.Models
{
    public class OrderModel
    {
        public int OrderID { get; set; }

        public int UserID { get; set; }

        public int? ProductID { get; set; }

        [Required]
        public string? SeriesNo { get; set; }

        public int Quantity { get; set; }

        public int ProductPrice { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }
    }
}
