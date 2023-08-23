/// <summary>
/// Класс OrderService представляет службу для работы с заказами.
/// Этот класс предоставляет бизнес-логику для создания, извлечения и обработки заказов.
/// 
/// В контексте паттерна "Агрегатор", данный класс служит мостом между пользовательским интерфейсом
/// и базой данных, делегируя операции с базой данных репозиторию заказов.
/// </summary>
namespace org.example.agregator
{

	public class OrderService
	{

		// Репозиторий для работы с заказами в базе данных
		private OrderRepository repository;

		/// <summary>
		/// Конструктор класса OrderService.
		/// </summary>
		/// <param name="repository"> Репозиторий для работы с заказами в базе данных. </param>
		public OrderService(OrderRepository repository)
		{
			this.repository = repository;
		}

		/// <summary>
		/// Метод для создания и сохранения заказа в базе данных.
		/// </summary>
		/// <param name="order"> Объект заказа, который необходимо сохранить. </param>
		/// <exception cref="SQLException"> Если возникнет проблема с базой данных. </exception>
		public virtual void createOrder(Order order)
		{
			// Дополнительная логика создания заказа может быть реализована здесь, например, валидация данных заказа
			repository.save(order);
		}

		/// <summary>
		/// Метод для получения заказа по его идентификатору.
		/// </summary>
		/// <param name="id"> Идентификатор заказа. </param>
		/// <returns> Объект заказа или null, если заказ не найден. </returns>
		/// <exception cref="SQLException"> Если возникнет проблема с базой данных. </exception>
		public virtual Order getOrderById(int id)
		{
			return repository.getById(id);
		}

		// Другие методы для работы с заказами могут быть добавлены здесь.
		// Например, методы для обновления заказа, удаления заказа, получения списка всех заказов и так далее.
	}

}
