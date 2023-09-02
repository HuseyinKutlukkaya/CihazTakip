using cihaztakip.entity;

namespace cihaztakip.webui.Models
{
    public class UserListViewModel
    {
        public List<UserData> Users { get; set; }=new List<UserData>();

    }

    public class UserData
    {
        public User User { get; set; }
        public string Role { get; set; }
    }
}
