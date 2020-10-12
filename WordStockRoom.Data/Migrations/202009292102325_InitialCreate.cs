namespace WordStockRoom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        LanguageId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        LanguageFamily = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.LanguageId);
            
            CreateTable(
                "dbo.Word",
                c => new
                    {
                        WordId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        WordName = c.String(nullable: false),
                        Translation = c.String(nullable: false),
                        PartOfSpeech = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WordId)
                .ForeignKey("dbo.Language", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.Sentence",
                c => new
                    {
                        SentenceId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        SentenceContent = c.String(nullable: false),
                        SentenceTranslation = c.String(nullable: false),
                        WordId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SentenceId)
                .ForeignKey("dbo.Word", t => t.WordId, cascadeDelete: true)
                .Index(t => t.WordId);
            
            CreateTable(
                "dbo.Video",
                c => new
                    {
                        VideoId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        URL = c.String(nullable: false),
                        WordId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VideoId)
                .ForeignKey("dbo.Word", t => t.WordId, cascadeDelete: true)
                .Index(t => t.WordId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Video", "WordId", "dbo.Word");
            DropForeignKey("dbo.Sentence", "WordId", "dbo.Word");
            DropForeignKey("dbo.Word", "LanguageId", "dbo.Language");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Video", new[] { "WordId" });
            DropIndex("dbo.Sentence", new[] { "WordId" });
            DropIndex("dbo.Word", new[] { "LanguageId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Video");
            DropTable("dbo.Sentence");
            DropTable("dbo.Word");
            DropTable("dbo.Language");
        }
    }
}
