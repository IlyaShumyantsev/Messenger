namespace Domain.Entities
{
    using System.Data.Entity;

    public partial class EFDbContext : DbContext
    {
        public EFDbContext() : base("name=EFDbContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<EFDbContext>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EFDbContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<EFDbContext>());
        }

        public virtual DbSet<FriendRequest> FriendRequests { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<IncomingMessage> IncomingMessages { get; set; }
        public virtual DbSet<OutgoingMessage> OutgoingMessages { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) { }
    }
}
