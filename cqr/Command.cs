namespace org.example.cqr
{
	/// <summary>
	/// Интерфейс "Command".
	/// Представляет команду — действие, которое изменяет состояние системы.
	/// Например, добавление, обновление или удаление записи в базе данных.
	/// </summary>
	public interface Command
	{
		void execute();
	}
}
