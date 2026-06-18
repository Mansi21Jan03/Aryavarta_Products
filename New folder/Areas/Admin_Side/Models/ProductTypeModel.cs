using System.ComponentModel.DataAnnotations;

namespace Aryavarta.Areas.Admin_Side.Models
{
    public class ProductTypeModel
    {
        public int? ProductTypeID { get; set; }

        [Required]
        public string? ProductType { get; set; }

        public IFormFile File { get; set; }

        [Required]
        public string? ProductTypeImg { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }

    public class AVP_ProductTypeDropDownModel
    {
        public int? ProductTypeID { get; set; }

        public string? ProductType { get; set; }
    }
}
