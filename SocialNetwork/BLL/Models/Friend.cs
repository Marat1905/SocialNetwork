namespace SocialNetwork.BLL.Models
{
    public class Friend
    {
        public int FriendId { get; set; }

        public string FirstName { get; set; }

        public string FriendEmail { get; set; }

        public Friend(int id,string name,string email)
        {
            FriendId = id;
            FirstName = name;
            FriendEmail = email;
        }
    }
 
}
