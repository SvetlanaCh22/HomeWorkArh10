using Microsoft.Data.Sqlite;

namespace org.example.cqr
{

	/// <summary>
	/// Класс "AddUserCommand".
	/// Реализует добавление пользователя в базу данных.
	/// Реализует интерфейс Command.
	/// </summary>
	public class AddUserCommand : Command
	{

		// Соединение с базой данных
		private readonly SqliteConnection connection;
		// Имя пользователя
		private readonly string userName;

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="connection"> Соединение с базой данных. </param>
		/// <param name="userName"> Имя пользователя для добавления. </param>
		public AddUserCommand(SqliteConnection connection, string userName)
		{
			this.connection = connection;
			this.userName = userName;
		}

		/// <summary>
		/// Выполнить добавление пользователя в базу данных.
		/// </summary>
		public virtual void execute()
		{
			string sql = "INSERT INTO users (name) VALUES (" + userName + ")";

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
