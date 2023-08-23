using Microsoft.Data.Sqlite;

namespace org.example.cqr
{

    /// <summary>
    /// Класс "CQRSExample".
    /// Демонстрация применения паттерна CQRS.
    /// </summary>
    public class CQRSExample
    {

        public static void Main(string[] args)
        {
            // URL соединения с базой данных SQLite
            string url = "Data Source=sample.db;";

            try
            {
                using (SqliteConnection connection = new SqliteConnection(url))
                {
                    // Создание таблицы пользователей, если она не существует
                    createUsersTable(connection);

                    // Добавление пользователя
                    Command addUser = new AddUserCommand(connection, "John Doe");
                    addUser.execute();

                    // Запрос всех пользователей
                    Query<IList<string>> query = new GetAllUsersQuery(connection);
                    IList<string> users = query.execute();

                    // Вывод пользователей
                    foreach (string user in users)
                    {
                        Console.WriteLine(user);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Создание таблицы пользователей, если она не существует.
        /// </summary>
        /// <param name="connection"> Соединение с базой данных. </param>
        private static void createUsersTable(SqliteConnection connection)
        {
            string sql = "CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY, name TEXT NOT NULL)";

            try
            {
                connection.Open();
                SqliteCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
