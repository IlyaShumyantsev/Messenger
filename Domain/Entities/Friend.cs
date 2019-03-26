namespace Domain.Entities
{
    public partial class Friend
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int FriendId { get; set; }
    }
}
