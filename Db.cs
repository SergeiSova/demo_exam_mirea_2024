using System.Data.SqlClient;

namespace Demo
{
    internal static class Db
    {
        private const string DefaultConnection =
            @"Server=(localdb)\MSSQLLocalDB;Database=Sova;TrustServerCertificate=True;Trusted_Connection=True;";

        public static string ConnectionString =>
            Environment.GetEnvironmentVariable("DEMO_DB_CONNECTION") ?? DefaultConnection;

        public static SqlConnection Open()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        public static object Value(string? text)
        {
            return string.IsNullOrWhiteSpace(text) ? DBNull.Value : text.Trim();
        }

        public static object Value(DateTime? date)
        {
            return date.HasValue ? date.Value.Date : DBNull.Value;
        }
    }
}
