namespace Aryavarta.Areas.Admin_Side.Models
{
    public class FeedbackModel
    {
        public int? FeedbackID { get; set; }

        public int UserID { get; set; }

        public int ProductID { get; set; }

        public IFormFile File { get; set; }

        public string Image { get; set; }

        public string FeedbackMsg { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }
    }
}
