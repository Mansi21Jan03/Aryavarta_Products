namespace Aryavarta.Areas.Admin_Side.Models
{
    public class ProductsModel
    {
        public int? ProductID { get; set; }

        public int  MaterialID { get; set; }

        public int FinishID { get; set; }

        public string SeriesNo { get; set; }

        public int  ProductTypeID { get; set; }

        public int ProductPrice { get; set; }

        public IFormFile File { get; set; }

        public string ProductImg { get; set; }

        public int AvailableStock { get; set; }

        public DateTime CreationDate { get; set; }
        
        public DateTime ModificationDate { get; set; }
    }

    public class PR_AVP_Products_SelectForDropDown {

        public int? ProductID { get; set; }

        public string? SeriesNo { get; set; }

        public int ProductPrice { get; set; }

    }

    public class AVP_Products_SelectDropDownForSeriesNo
    {
        public int? ProductID { get; set; }

        public string SeriesNo { get; set; }
    }

    public class AVP_Products_SelectDropDownForProductImg
    {
        public int? ProductID { get; set; }

        public string ProductImg { get; set; }
    }

    public class AVP_Products_SelectDropDownForProductPrice
    {
        public int ProductID { get; set; }

        public int ProductPrice { get; set; }
    }
}
