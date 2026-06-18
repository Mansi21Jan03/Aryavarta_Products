using System.ComponentModel.DataAnnotations;

namespace Aryavarta.Areas.Admin_Side.Models
{
    public class MaterialModel
    {
        public int? MaterialID { get; set; }

        [Required]
        public string? MaterialType { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }

    public class AVP_ProductMaterialDropDownModel
    {
        public int? MaterialID { get; set; }

        public string? MaterialType { get; set; }
    }
}
