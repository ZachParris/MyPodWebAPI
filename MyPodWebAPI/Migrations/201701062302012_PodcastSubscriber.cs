namespace MyPodWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PodcastSubscriber : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Podcasts", name: "CustomUser_Id", newName: "Subscriber_Id");
            RenameIndex(table: "dbo.Podcasts", name: "IX_CustomUser_Id", newName: "IX_Subscriber_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Podcasts", name: "IX_Subscriber_Id", newName: "IX_CustomUser_Id");
            RenameColumn(table: "dbo.Podcasts", name: "Subscriber_Id", newName: "CustomUser_Id");
        }
    }
}
