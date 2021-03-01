namespace RoleApiClientIdentity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.APPLICATION_GROUPS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NAME = c.String(maxLength: 250),
                        DESCRIPTION = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.APPLICATION_ROLES",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        GroupId = c.Int(),
                        Description = c.String(maxLength: 250),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.APPLICATION_GROUPS", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.APPLICATION_USER_ROLES",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        APPLICATION_USER_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.APPLICATION_USERS", t => t.APPLICATION_USER_Id)
                .ForeignKey("dbo.APPLICATION_ROLES", t => t.IdentityRole_Id)
                .Index(t => t.APPLICATION_USER_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.APPLICATION_MODULE",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NAME = c.String(),
                        URL = c.String(),
                        PARENT_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.APPLICATION_ROLE_GROUPS",
                c => new
                    {
                        GROUP_ID = c.Int(nullable: false),
                        ROLE_ID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GROUP_ID, t.ROLE_ID })
                .ForeignKey("dbo.APPLICATION_GROUPS", t => t.GROUP_ID, cascadeDelete: false)
                .ForeignKey("dbo.APPLICATION_ROLES", t => t.ROLE_ID, cascadeDelete: false)
                .Index(t => t.GROUP_ID)
                .Index(t => t.ROLE_ID);
            
            CreateTable(
                "dbo.APPLICATION_USER_GROUPS",
                c => new
                    {
                        USER_ID = c.String(nullable: false, maxLength: 128),
                        GROUP_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.USER_ID, t.GROUP_ID })
                .ForeignKey("dbo.APPLICATION_GROUPS", t => t.GROUP_ID, cascadeDelete: true)
                .ForeignKey("dbo.APPLICATION_USERS", t => t.USER_ID, cascadeDelete: true)
                .Index(t => t.USER_ID)
                .Index(t => t.GROUP_ID);
            
            CreateTable(
                "dbo.APPLICATION_USERS",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EM_ID = c.Int(),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EMPLOYEES", t => t.EM_ID)
                .Index(t => t.EM_ID);
            
            CreateTable(
                "dbo.APPLICATION_USER_CLAIMS",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        APPLICATION_USER_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.APPLICATION_USERS", t => t.APPLICATION_USER_Id)
                .Index(t => t.APPLICATION_USER_Id);
            
            CreateTable(
                "dbo.EMPLOYEES",
                c => new
                    {
                        EM_ID = c.Int(nullable: false, identity: true),
                        EM_TYPE_ID = c.Int(),
                        REG_ID = c.Int(),
                        DEP_ID = c.Int(),
                        EM_CODE = c.String(maxLength: 15),
                        EM_NAME = c.String(maxLength: 100),
                        EM_GENDER = c.String(maxLength: 1),
                        EM_BIRTHDATE = c.DateTime(storeType: "date"),
                        EM_IDENTITY_NUMBER = c.String(maxLength: 15),
                        EM_ADDRESS = c.String(maxLength: 400),
                        EM_PHONE = c.String(maxLength: 15),
                        EM_EMAIL = c.String(maxLength: 50),
                        EM_IMAGE = c.String(maxLength: 400),
                        EM_TIME_CHECK = c.Boolean(),
                        FIRST_FINGER = c.String(),
                        SECOND_FINGER = c.String(),
                        EM_STATUS = c.Boolean(),
                        EDIT_STATUS = c.Boolean(),
                        PIN = c.String(maxLength: 8),
                        PRIVILEGE = c.String(maxLength: 1),
                        GA_ID = c.Int(),
                        EM_IMAGE2 = c.Binary(),
                    })
                .PrimaryKey(t => t.EM_ID);
            
            CreateTable(
                "dbo.APPLICATION_USER_LOGINS",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        APPLICATION_USER_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.APPLICATION_USERS", t => t.APPLICATION_USER_Id)
                .Index(t => t.APPLICATION_USER_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.APPLICATION_USER_ROLES", "IdentityRole_Id", "dbo.APPLICATION_ROLES");
            DropForeignKey("dbo.APPLICATION_USER_GROUPS", "USER_ID", "dbo.APPLICATION_USERS");
            DropForeignKey("dbo.APPLICATION_USER_ROLES", "APPLICATION_USER_Id", "dbo.APPLICATION_USERS");
            DropForeignKey("dbo.APPLICATION_USER_LOGINS", "APPLICATION_USER_Id", "dbo.APPLICATION_USERS");
            DropForeignKey("dbo.APPLICATION_USERS", "EM_ID", "dbo.EMPLOYEES");
            DropForeignKey("dbo.APPLICATION_USER_CLAIMS", "APPLICATION_USER_Id", "dbo.APPLICATION_USERS");
            DropForeignKey("dbo.APPLICATION_USER_GROUPS", "GROUP_ID", "dbo.APPLICATION_GROUPS");
            DropForeignKey("dbo.APPLICATION_ROLE_GROUPS", "ROLE_ID", "dbo.APPLICATION_ROLES");
            DropForeignKey("dbo.APPLICATION_ROLE_GROUPS", "GROUP_ID", "dbo.APPLICATION_GROUPS");
            DropForeignKey("dbo.APPLICATION_ROLES", "GroupId", "dbo.APPLICATION_GROUPS");
            DropIndex("dbo.APPLICATION_USER_LOGINS", new[] { "APPLICATION_USER_Id" });
            DropIndex("dbo.APPLICATION_USER_CLAIMS", new[] { "APPLICATION_USER_Id" });
            DropIndex("dbo.APPLICATION_USERS", new[] { "EM_ID" });
            DropIndex("dbo.APPLICATION_USER_GROUPS", new[] { "GROUP_ID" });
            DropIndex("dbo.APPLICATION_USER_GROUPS", new[] { "USER_ID" });
            DropIndex("dbo.APPLICATION_ROLE_GROUPS", new[] { "ROLE_ID" });
            DropIndex("dbo.APPLICATION_ROLE_GROUPS", new[] { "GROUP_ID" });
            DropIndex("dbo.APPLICATION_USER_ROLES", new[] { "IdentityRole_Id" });
            DropIndex("dbo.APPLICATION_USER_ROLES", new[] { "APPLICATION_USER_Id" });
            DropIndex("dbo.APPLICATION_ROLES", new[] { "GroupId" });
            DropTable("dbo.APPLICATION_USER_LOGINS");
            DropTable("dbo.EMPLOYEES");
            DropTable("dbo.APPLICATION_USER_CLAIMS");
            DropTable("dbo.APPLICATION_USERS");
            DropTable("dbo.APPLICATION_USER_GROUPS");
            DropTable("dbo.APPLICATION_ROLE_GROUPS");
            DropTable("dbo.APPLICATION_MODULE");
            DropTable("dbo.APPLICATION_USER_ROLES");
            DropTable("dbo.APPLICATION_ROLES");
            DropTable("dbo.APPLICATION_GROUPS");
        }
    }
}
