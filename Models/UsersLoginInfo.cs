namespace HotelManagementSystem.Models
{
    public class UsersLoginInfo
    {
        public string UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }
        
    }
}
