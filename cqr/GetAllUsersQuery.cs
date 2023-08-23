using Microsoft.Data.Sqlite;
using System.Data;

namespace org.example.cqr
{

	/// <summary>
	/// Класс "GetAllUsersQuery".
	/// Отвечает за извлечение списка пользователей из базы данных.
	/// Реализует интерфейс Query<List<String>>.
	/// </summary>
	public class GetAllUsersQuery : Query<IList<string>>
	{

		// Соединение с базой данных
		private readonly SqliteConnection connection;

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="connection"> Соединение с базой данных. </param>
		public GetAllUsersQuery(SqliteConnection connection)
		{
			this.connection = connection;
		}

		/// <summary>
		/// Выполняет запрос на извлечение всех пользователей из базы данных.
		/// </summary>
		/// <returns> Список имен пользователей. </returns>
		public virtual IList<string> execute()
		{
			IList<string> users = new List<string>();
			string sql = "SELECT name FROM users";

			try
			{
                connection.Open();

                SqliteCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(reader["name"].ToString() ?? "");	
				}
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

			return users;
		}
	}
}
