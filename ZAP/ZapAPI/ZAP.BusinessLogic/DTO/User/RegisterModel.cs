namespace ZAP.BusinessLogic.DTO.User
{
    public class RegisterModel : UserModel
    {
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Password { get; set; }
    }
}
