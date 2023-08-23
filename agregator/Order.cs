namespace org.example.agregator
{
	/// <summary>
	/// Класс Order представляет заказ, который состоит из одного или нескольких продуктов (элементов заказа).
	/// Этот класс агрегирует в себе несколько объектов OrderItem, что соответствует паттерну "Агрегатор".
	/// 
	/// В контексте паттерна "Агрегатор", данный класс играет роль "Контейнера", который собирает в себе
	/// несколько объектов, предоставляя удобный интерфейс для работы с ними.
	/// </summary>

	public class Order
	{

		// Список элементов заказа
		private IList<OrderItem> items;

		/// <summary>
		/// Конструктор, инициализирующий пустой список элементов заказа.
		/// </summary>
		public Order()
		{
			this.items = new List<OrderItem>();
		}

		/// <summary>
		/// Геттер для получения списка элементов заказа.
		/// </summary>
		/// <returns> список элементов заказа </returns>
		public virtual IList<OrderItem> Items
		{
			get
			{
				return items;
			}
			set
			{
				this.items = value;
			}
		}


		/// <summary>
		/// Метод для добавления элемента заказа в текущий заказ.
		/// </summary>
		/// <param name="item"> элемент заказа для добавления </param>
		public virtual void addItem(OrderItem item)
		{
			this.items.Add(item);
		}

		/// <summary>
		/// Метод для расчета общей стоимости заказа, учитывая цену и количество каждого продукта.
		/// </summary>
		/// <returns> общая стоимость заказа </returns>
		public virtual double TotalPrice
		{
			get
			{
				return items.Select(item => item.Product.Price * item.Quantity).Sum();
			}
		}

		public override string ToString()
		{
			return "Order{" + "items=" + String.Join(",", items)  + '}';
		}
	}

}
