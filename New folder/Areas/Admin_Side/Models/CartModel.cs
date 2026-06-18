namespace Aryavarta.Areas.Admin_Side.Models
{
    public class CartModel
    {
        public int? CartID { get; set; }

        public int ProductID { get; set; }

        public int UserID { get; set; }

        public int Quantity { get; set;}

        public int TotalPrice { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }
    }
}
