using System.Data.SqlClient;

namespace WUBank.Utils.Database
{
    /// <summary>
    /// Класс для работы с базой данных
    /// </summary>
    public static class DatabaseUtils
    {
        private static string ConnectionString = "Server=(localdb)\\MSSQLLocalDB; " +
           "Database=WUBank; User Id=Amlex; " +
           "Password=123456q; Trusted_Connection=True; MultipleActiveResultSets=true";

        /// <summary>
        /// Использование SQL выражения
        /// </summary>
        /// <param name="sqlExpression"></param>
        /// <returns>таблица - результат выполненного запроса</returns>
        public static IEnumerable<Dictionary<string, object>> GetTable(string sqlExpression)
        {
            List<Dictionary<string, object>> Table = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row.Add(reader.GetName(i), reader.GetValue(i));
                        }
                        Table.Add(row);
                    }
                }
            }
            return Table;
        }

        /// <summary>
        /// Получение таблицы, используя SqlDataReader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>таблица - результат reader'а</returns>
        private static IEnumerable<Dictionary<string, object>> GetTable(SqlDataReader reader)
        {
            List<Dictionary<string, object>> Table = new List<Dictionary<string, object>>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(reader.GetName(i), reader.GetValue(i));
                    }
                    Table.Add(row);
                }
            }
            return Table;
        }
        /// <summary>
        /// Использование хранимой процедуры базы данных
        /// </summary>
        /// <param name="CrudName">Имя хранимой процедуры</param>
        /// <param name="param">Список параметров</param>
        /// <returns></returns>
        public static IEnumerable<Dictionary<string, object>> ExecuteCrud(string CrudName, Dictionary<string, object> param)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(CrudName, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var par in param)
                {
                    command.Parameters.Add(new SqlParameter
                    {
                        ParameterName = par.Key,
                        Value = par.Value
                    });
                }
                return GetTable(command.ExecuteReader());
            }
        }

        /// <summary>
        /// Вставка транзакции в таблицу Transactions
        /// </summary>
        /// <param name="win"></param>
        /// <param name="PlayerSteamID"></param>
        /// <param name="Bet"></param>
        /// <param name="WinAmount"></param>
        public static void InsertTransaction(bool win, string PlayerSteamID, decimal Bet, decimal WinAmount = 0)
        {
            if(win)
                GetTable($"INSERT INTO TRANSACTIONS (ID, Date, SteamID, Amount, Status, WinAmount) VALUES ((SELECT ISNULL(MAX(id) + 1, 0) FROM Transactions), GETDATE(), {PlayerSteamID}, {Bet}, N\'Выигрыш\', {WinAmount})");
            if (!win)
                GetTable($"INSERT INTO TRANSACTIONS (ID, Date, SteamID, Amount, Status) VALUES ((SELECT ISNULL(MAX(id) + 1, 0) FROM Transactions), GETDATE(), {PlayerSteamID}, {Bet}, N\'Проигрыш\')");
        }
    }
}
