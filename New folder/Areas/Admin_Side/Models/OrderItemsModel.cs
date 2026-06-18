namespace Aryavarta.Areas.Admin_Side.Models
{
    public class OrderItemsModel
    {
        public int? OrderItemID { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public int UserID { get; set; }

        public decimal Price { get; set; }

        public decimal Total { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }
    }
}
