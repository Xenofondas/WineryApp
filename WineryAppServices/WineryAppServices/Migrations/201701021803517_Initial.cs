namespace WineryAppServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                {
                    AnswerId = c.Int(nullable: false, identity: true),
                    AnswerText = c.String(nullable: false),
                    QuestionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.AnswerId)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);

            CreateTable(
                "dbo.Questions",
                c => new
                {
                    QuestionID = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false),
                })
                .PrimaryKey(t => t.QuestionID);

            CreateTable(
                "dbo.UserResponses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    User_Id = c.Int(nullable: false),
                    Question_Id = c.Int(nullable: false),
                    Answer_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FbId = c.Long(nullable: false),
                    FirstName = c.String(),
                    LastName = c.String(),
                    UserEmail = c.String(),
                    Gender = c.String(),
                })
                .PrimaryKey(t => t.Id);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserResponses");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
