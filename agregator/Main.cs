/// <summary>
///Этот класс Main является точкой входа в программу.
/// Он создает объекты различных классов
/// (OrderRepository, OrderService, Product, Order, OrderItem)
/// и демонстрирует, как они взаимодействуют в рамках паттерна
/// "Агрегатор". Создается заказ, добавляется в него товар,
/// сохраняется с использованием сервиса заказов,
/// а затем выводится последний сохраненный заказ с помощью репозитория.
/// </summary>

namespace org.example.agregator
{

	public class Program
	{

		public static void Main(string[] args)
		{
			try
			{
                // Инициализация репозитория и базы данных.
                OrderRepository repo = new OrderRepository("Data Source=shop.db;");

				// Создание экземпляра сервиса заказов, передавая ему репозиторий для работы с базой данных.
				OrderService service = new OrderService(repo);

				// Создание и инициализация продукта.
				Product book = new Product(1, "Книга", 10.0);

				// Создание нового заказа.
				Order order = new Order();

				// Добавление продукта в заказ.
				OrderItem orderItem = new OrderItem(book, 2);
				order.addItem(orderItem);

				// Сохранение заказа в базе данных с помощью сервиса.
				service.createOrder(order);

				// Получение и вывод последнего заказа из базы данных.
				Order lastOrder = repo.LastOrder; // Этот метод вам необходимо реализовать в классе OrderRepository
				if (lastOrder != null)
				{
					Console.WriteLine("Последний сохраненный заказ:");
					Console.WriteLine(lastOrder.ToString());
				}
				else
				{
					Console.WriteLine("В базе данных нет заказов.");
				}

			}
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
	}
}
