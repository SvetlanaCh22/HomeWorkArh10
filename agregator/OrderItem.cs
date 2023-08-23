/// <summary>
/// Класс OrderItem представляет элемент заказа. Элемент заказа включает в себя продукт
/// и количество этого продукта в заказе.
/// 
/// В контексте паттерна "Агрегатор", данный класс является одним из составных частей заказа (Order).
/// Он служит для представления информации о конкретном продукте и его количестве в заказе.
/// </summary>
namespace org.example.agregator
{
	public class OrderItem
	{

		// Продукт, на который ссылается данный элемент заказа
		private Product product;

		// Количество продукта в заказе
		private long quantity;

		/// <summary>
		/// Конструктор, создающий элемент заказа на основе продукта и его количества.
		/// </summary>
		/// <param name="product">  Продукт, который необходимо добавить в заказ </param>
		/// <param name="quantity"> Количество продукта в заказе </param>
		public OrderItem(Product product, long quantity)
		{
			this.product = product;
			this.quantity = quantity;
		}

		/// <summary>
		/// Геттер для получения продукта, связанного с данным элементом заказа.
		/// </summary>
		/// <returns> продукт, связанный с элементом заказа </returns>
		public virtual Product Product
		{
			get
			{
				return product;
			}
			set
			{
				this.product = value;
			}
		}

		/// <summary>
		/// Геттер для получения количества продукта в данном элементе заказа.
		/// </summary>
		/// <returns> количество продукта в элементе заказа </returns>
		public virtual long Quantity
		{
			get
			{
				return quantity;
			}
			set
			{
				this.quantity = value;
			}
		}

		public override string ToString()
		{
			return "OrderItem{" + "product=" + product + ", quantity=" + quantity + '}';
		}
	}
}
