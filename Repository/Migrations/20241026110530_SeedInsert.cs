using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository_CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class SeedInsert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name", "Email", "Password", "RegistrationDate", "FitnessLevel", "TrialExpiration" },
                values: new object[,]
                {
                    { 1, "Alice Johnson", "alice.johnson@example.com", "hashedpassword1", DateTime.Now.Date, "beginner", DateTime.Now.AddMonths(1).Date },
                    { 2, "Bob Smith", "bob.smith@example.com", "hashedpassword2", DateTime.Now.Date, "intermediate", DateTime.Now.AddMonths(2).Date },
                    { 3, "Charlie Brown", "charlie.brown@example.com", "hashedpassword3", DateTime.Now.Date, "advanced", DateTime.Now.AddMonths(1).Date },
                    { 4, "Diana Prince", "diana.prince@example.com", "hashedpassword4", DateTime.Now.Date, "beginner", null },
                    { 5, "Eve Adams", "eve.adams@example.com", "hashedpassword5", DateTime.Now.Date, "advanced", DateTime.Now.AddMonths(3).Date }
                }
            );

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "UserId", "SubscriptionType", "StartDate", "EndDate", "Price" },
                values: new object[,]
                {
                    { 1, "beginner", new DateTime(2023, 1, 1), new DateTime(2024, 1, 1), 9.99 },
                    { 2, "intermediate", new DateTime(2023, 2, 1), new DateTime(2024, 2, 1), 19.99 },
                    { 3, "advanced", new DateTime(2023, 3, 1), new DateTime(2024, 3, 1), 29.99 },
                    { 4, "beginner", new DateTime(2023, 4, 1), new DateTime(2024, 4, 1), 14.99 },
                    { 5, "intermediate", new DateTime(2023, 5, 1), new DateTime(2024, 5, 1), 49.99 }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
               table: "Users",
               keyColumn: "UserId",
               keyValues: new object[] { 1, 2, 3, 4, 5 }
           );

            migrationBuilder.DeleteData(
                table: "Subscription",
                keyColumn: "SubscriptionId",
                keyValues: new object[] { 1, 2, 3, 4, 5 }
           );
        }
    }
}
