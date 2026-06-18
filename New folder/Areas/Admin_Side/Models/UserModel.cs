namespace Aryavarta.Areas.Admin_Side.Models
{
    public class UserModel
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public int Pincode { get; set; }

        public string Address { get; set; }

        public string MobileNo { get; set; }

        public string EmailID { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

    }

    public class AVP_UserDropDown
    {
        public int UserID { get; set; }

        public string? UserName { get; set; }
    }

}
