namespace eRNI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.tblActions",
                c => new
                    {
                        actionID = c.Int(nullable: false, identity: true),
                        actionDate = c.DateTime(nullable: false),
                        actionDescription = c.String(nullable: false, maxLength: 500),
                        projectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.actionID)
                .ForeignKey("dbo.tblProjects", t => t.projectID, cascadeDelete: true)
                .Index(t => t.projectID);
            
            CreateTable(
                "dbo.tblProjects",
                c => new
                    {
                        projectID = c.Int(nullable: false, identity: true),
                        projectAdditionalInfo = c.String(),
                        projectInflow = c.DateTime(nullable: false),
                        projectStatus = c.Int(nullable: false),
                        projectLastActivity = c.DateTime(),
                        projectLeader = c.String(nullable: false, maxLength: 80),
                        projectManager = c.String(nullable: false, maxLength: 80),
                        projectSapNo = c.String(nullable: false, maxLength: 20),
                        projectTask = c.String(nullable: false, maxLength: 400),
                        projectPriority = c.Boolean(nullable: false),
                        projectCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.projectID)
                .ForeignKey("dbo.tblProjectCategories", t => t.projectCategoryID, cascadeDelete: true)
                .Index(t => t.projectCategoryID);
            
            CreateTable(
                "dbo.tblInvoices",
                c => new
                    {
                        invoiceID = c.Int(nullable: false, identity: true),
                        invoiceAdditionalInfo = c.String(),
                        invoiceIssueDate = c.DateTime(nullable: false),
                        invoiceNettoValue = c.Decimal(nullable: false, storeType: "money"),
                        invoiceTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        invoiceNo = c.String(nullable: false, maxLength: 50),
                        invoiceSapRegisterNo = c.String(nullable: false, maxLength: 20),
                        invoiceSapRegistrationDate = c.DateTime(nullable: false),
                        invoiceSellerName = c.String(nullable: false, maxLength: 255),
                        invoiceTitle = c.String(nullable: false, maxLength: 255),
                        projectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.invoiceID)
                .ForeignKey("dbo.tblProjects", t => t.projectID, cascadeDelete: true)
                .Index(t => t.projectID);
            
            CreateTable(
                "dbo.tblLocalizations",
                c => new
                    {
                        localizationID = c.Int(nullable: false, identity: true),
                        localizationLandRegister = c.String(nullable: false, maxLength: 50),
                        localizationMapNo = c.String(maxLength: 10),
                        localizationPlotNo = c.String(nullable: false, maxLength: 9),
                        localizationPrecinct = c.String(nullable: false, maxLength: 80),
                        localizationRegulationStatus = c.Int(nullable: false),
                        localizationStreets = c.String(maxLength: 255),
                        placeID = c.Int(nullable: false),
                        projectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.localizationID)
                .ForeignKey("dbo.tblPlaces", t => t.placeID, cascadeDelete: true)
                .ForeignKey("dbo.tblProjects", t => t.projectID, cascadeDelete: true)
                .Index(t => t.placeID)
                .Index(t => t.projectID);
            
            CreateTable(
                "dbo.tblDevices",
                c => new
                    {
                        deviceID = c.Int(nullable: false, identity: true),
                        deviceLenght = c.Decimal(nullable: false, precision: 18, scale: 2),
                        deviceWidth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        deviceVoltage = c.Int(nullable: false),
                        deviceCategoryID = c.Int(nullable: false),
                        localizationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.deviceID)
                .ForeignKey("dbo.tblDeviceCategories", t => t.deviceCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.tblLocalizations", t => t.localizationID, cascadeDelete: true)
                .Index(t => t.deviceCategoryID)
                .Index(t => t.localizationID);
            
            CreateTable(
                "dbo.tblDeviceCategories",
                c => new
                    {
                        deviceCategoryID = c.Int(nullable: false, identity: true),
                        deviceCategoryName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.deviceCategoryID);
            
            CreateTable(
                "dbo.tblRegulations",
                c => new
                    {
                        regulationID = c.Int(nullable: false, identity: true),
                        regulationAdditionalInfo = c.String(),
                        regulationSapElement = c.String(nullable: false, maxLength: 10),
                        regulationCost = c.Decimal(storeType: "money"),
                        deviceID = c.Int(nullable: false),
                        regulationCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.regulationID)
                .ForeignKey("dbo.tblDevices", t => t.deviceID, cascadeDelete: true)
                .ForeignKey("dbo.tblRegulationCategories", t => t.regulationCategoryID, cascadeDelete: true)
                .Index(t => t.deviceID)
                .Index(t => t.regulationCategoryID);
            
            CreateTable(
                "dbo.tblRegulationCategories",
                c => new
                    {
                        regulationCategoryID = c.Int(nullable: false, identity: true),
                        regulationCategoryName = c.String(nullable: false, maxLength: 80),
                    })
                .PrimaryKey(t => t.regulationCategoryID);
            
            CreateTable(
                "dbo.tblRegulationDocuments",
                c => new
                    {
                        regulationDocumentID = c.Int(nullable: false, identity: true),
                        regulationDocumentDate = c.DateTime(nullable: false),
                        regulationDocumentSignature = c.String(nullable: false, maxLength: 100),
                        regulationDocumentSource = c.String(nullable: false, maxLength: 100),
                        regulationDocumentType = c.String(nullable: false, maxLength: 50),
                        regulationDocumentLink = c.String(maxLength: 400),
                        additionalInformation = c.String(maxLength: 400),
                        regulationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.regulationDocumentID)
                .ForeignKey("dbo.tblRegulations", t => t.regulationID, cascadeDelete: true)
                .Index(t => t.regulationID);
            
            CreateTable(
                "dbo.tblOwnerships",
                c => new
                    {
                        ownershipID = c.Int(nullable: false, identity: true),
                        localizationID = c.Int(nullable: false),
                        ownerID = c.Int(),
                    })
                .PrimaryKey(t => t.ownershipID)
                .ForeignKey("dbo.tblLocalizations", t => t.localizationID, cascadeDelete: true)
                .ForeignKey("dbo.tblOwners", t => t.ownerID)
                .Index(t => t.localizationID)
                .Index(t => t.ownerID);
            
            CreateTable(
                "dbo.tblOwners",
                c => new
                    {
                        ownerID = c.Int(nullable: false, identity: true),
                        ownerAdditionalInfo = c.String(),
                        ownerName = c.String(nullable: false, maxLength: 255),
                        ownerHouseNo = c.String(nullable: false, maxLength: 10),
                        ownerAptNo = c.String(maxLength: 10),
                        ownerPhone = c.String(maxLength: 20),
                        ownerCellPhone = c.String(maxLength: 20),
                        ownerPostOffice = c.String(nullable: false, maxLength: 80),
                        ownerZipCode = c.String(nullable: false, maxLength: 6),
                        ownerEmail = c.String(maxLength: 100),
                        ownerCity = c.String(nullable: false, maxLength: 80),
                        streetID = c.Int(),
                    })
                .PrimaryKey(t => t.ownerID)
                .ForeignKey("dbo.tblStreets", t => t.streetID)
                .Index(t => t.streetID);
            
            CreateTable(
                "dbo.tblStreets",
                c => new
                    {
                        streetID = c.Int(nullable: false, identity: true),
                        streetName = c.String(nullable: false, maxLength: 80),
                    })
                .PrimaryKey(t => t.streetID);
            
            CreateTable(
                "dbo.tblPlaces",
                c => new
                    {
                        placeID = c.Int(nullable: false, identity: true),
                        placeName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.placeID);
            
            CreateTable(
                "dbo.tblProjectCategories",
                c => new
                    {
                        projectCategoryID = c.Int(nullable: false, identity: true),
                        projectCategryName = c.String(nullable: false, maxLength: 80),
                    })
                .PrimaryKey(t => t.projectCategoryID);
            
            CreateTable(
                "dbo.tblPropertyDocuments",
                c => new
                    {
                        propertyDocumentID = c.Int(nullable: false, identity: true),
                        propertyDocumentAdditionalInfo = c.String(),
                        propertyDocumentSapRegisterNo = c.String(nullable: false, maxLength: 15),
                        propertyDocumentSapRegistrationDate = c.DateTime(nullable: false),
                        propertyDocumentType = c.String(nullable: false, maxLength: 80),
                        projectID = c.Int(),
                    })
                .PrimaryKey(t => t.propertyDocumentID)
                .ForeignKey("dbo.tblProjects", t => t.projectID)
                .Index(t => t.projectID);
            
            CreateTable(
                "dbo.tblKeyDocumentCategories",
                c => new
                    {
                        KeyDocumentCategoryID = c.Int(nullable: false, identity: true),
                        keyDocumentName = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.KeyDocumentCategoryID);
            
            CreateTable(
                "dbo.tblKeyDocuments",
                c => new
                    {
                        keyDocumentID = c.Int(nullable: false, identity: true),
                        keyDocumentDate = c.DateTime(nullable: false),
                        keyDocumentSign = c.String(nullable: false, maxLength: 100),
                        KeyDocumentCategoryID = c.Int(nullable: false),
                        keyDocumentObtainment = c.DateTime(nullable: false),
                        projectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.keyDocumentID)
                .ForeignKey("dbo.tblKeyDocumentCategories", t => t.KeyDocumentCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.tblProjects", t => t.projectID, cascadeDelete: true)
                .Index(t => t.KeyDocumentCategoryID)
                .Index(t => t.projectID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.tblKeyDocuments", "projectID", "dbo.tblProjects");
            DropForeignKey("dbo.tblKeyDocuments", "KeyDocumentCategoryID", "dbo.tblKeyDocumentCategories");
            DropForeignKey("dbo.tblPropertyDocuments", "projectID", "dbo.tblProjects");
            DropForeignKey("dbo.tblProjects", "projectCategoryID", "dbo.tblProjectCategories");
            DropForeignKey("dbo.tblLocalizations", "projectID", "dbo.tblProjects");
            DropForeignKey("dbo.tblLocalizations", "placeID", "dbo.tblPlaces");
            DropForeignKey("dbo.tblOwners", "streetID", "dbo.tblStreets");
            DropForeignKey("dbo.tblOwnerships", "ownerID", "dbo.tblOwners");
            DropForeignKey("dbo.tblOwnerships", "localizationID", "dbo.tblLocalizations");
            DropForeignKey("dbo.tblRegulationDocuments", "regulationID", "dbo.tblRegulations");
            DropForeignKey("dbo.tblRegulations", "regulationCategoryID", "dbo.tblRegulationCategories");
            DropForeignKey("dbo.tblRegulations", "deviceID", "dbo.tblDevices");
            DropForeignKey("dbo.tblDevices", "localizationID", "dbo.tblLocalizations");
            DropForeignKey("dbo.tblDevices", "deviceCategoryID", "dbo.tblDeviceCategories");
            DropForeignKey("dbo.tblInvoices", "projectID", "dbo.tblProjects");
            DropForeignKey("dbo.tblActions", "projectID", "dbo.tblProjects");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.tblKeyDocuments", new[] { "projectID" });
            DropIndex("dbo.tblKeyDocuments", new[] { "KeyDocumentCategoryID" });
            DropIndex("dbo.tblPropertyDocuments", new[] { "projectID" });
            DropIndex("dbo.tblOwners", new[] { "streetID" });
            DropIndex("dbo.tblOwnerships", new[] { "ownerID" });
            DropIndex("dbo.tblOwnerships", new[] { "localizationID" });
            DropIndex("dbo.tblRegulationDocuments", new[] { "regulationID" });
            DropIndex("dbo.tblRegulations", new[] { "regulationCategoryID" });
            DropIndex("dbo.tblRegulations", new[] { "deviceID" });
            DropIndex("dbo.tblDevices", new[] { "localizationID" });
            DropIndex("dbo.tblDevices", new[] { "deviceCategoryID" });
            DropIndex("dbo.tblLocalizations", new[] { "projectID" });
            DropIndex("dbo.tblLocalizations", new[] { "placeID" });
            DropIndex("dbo.tblInvoices", new[] { "projectID" });
            DropIndex("dbo.tblProjects", new[] { "projectCategoryID" });
            DropIndex("dbo.tblActions", new[] { "projectID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.tblKeyDocuments");
            DropTable("dbo.tblKeyDocumentCategories");
            DropTable("dbo.tblPropertyDocuments");
            DropTable("dbo.tblProjectCategories");
            DropTable("dbo.tblPlaces");
            DropTable("dbo.tblStreets");
            DropTable("dbo.tblOwners");
            DropTable("dbo.tblOwnerships");
            DropTable("dbo.tblRegulationDocuments");
            DropTable("dbo.tblRegulationCategories");
            DropTable("dbo.tblRegulations");
            DropTable("dbo.tblDeviceCategories");
            DropTable("dbo.tblDevices");
            DropTable("dbo.tblLocalizations");
            DropTable("dbo.tblInvoices");
            DropTable("dbo.tblProjects");
            DropTable("dbo.tblActions");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
