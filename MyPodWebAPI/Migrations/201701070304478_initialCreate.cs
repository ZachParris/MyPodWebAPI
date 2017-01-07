namespace MyPodWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Episodes",
                c => new
                    {
                        EpisodeId = c.Int(nullable: false, identity: true),
                        PodcastId = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Duration = c.String(),
                        AirDate = c.String(),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.EpisodeId)
                .ForeignKey("dbo.Podcasts", t => t.PodcastId, cascadeDelete: true)
                .Index(t => t.PodcastId);
            
            CreateTable(
                "dbo.Podcasts",
                c => new
                    {
                        PodcastId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                        FeedUrl = c.String(),
                    })
                .PrimaryKey(t => t.PodcastId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Post = c.String(),
                        BlogAuthor_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.BlogAuthor_Id)
                .Index(t => t.BlogAuthor_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.CustomUserPodcasts",
                c => new
                    {
                        CustomUser_Id = c.String(nullable: false, maxLength: 128),
                        Podcast_PodcastId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomUser_Id, t.Podcast_PodcastId })
                .ForeignKey("dbo.AspNetUsers", t => t.CustomUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Podcasts", t => t.Podcast_PodcastId, cascadeDelete: true)
                .Index(t => t.CustomUser_Id)
                .Index(t => t.Podcast_PodcastId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CustomUserPodcasts", "Podcast_PodcastId", "dbo.Podcasts");
            DropForeignKey("dbo.CustomUserPodcasts", "CustomUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Blogs", "BlogAuthor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Episodes", "PodcastId", "dbo.Podcasts");
            DropIndex("dbo.CustomUserPodcasts", new[] { "Podcast_PodcastId" });
            DropIndex("dbo.CustomUserPodcasts", new[] { "CustomUser_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Blogs", new[] { "BlogAuthor_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Episodes", new[] { "PodcastId" });
            DropTable("dbo.CustomUserPodcasts");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Blogs");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Podcasts");
            DropTable("dbo.Episodes");
        }
    }
}
