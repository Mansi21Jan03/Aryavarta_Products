using System.ComponentModel.DataAnnotations;

namespace Aryavarta.Areas.Admin_Side.Models
{
    public class FinishModel
    {
        public int? FinishID { get; set; }

        [Required]
        public string? FinishType { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }

    public class AVP_ProductFinishDropDown
    {
        public int? FinishID { get; set; }

        public string? FinishType { get; set; }
    }
}
