/// <summary>
/// Класс Product представляет продукт, который может быть добавлен в заказ.
/// Этот класс содержит информацию о продукте, такую как идентификатор, название и стоимость.
/// </summary>
namespace org.example.agregator
{

	public class Product
	{
		private long id;
		private string name;
		private double price;

		/// <summary>
		/// Конструктор класса Product.
		/// </summary>
		/// <param name="id">    Идентификатор продукта. </param>
		/// <param name="name">  Название продукта. </param>
		/// <param name="price"> Стоимость продукта. </param>
		public Product(long id, string name, double price)
		{
			this.id = id;
			this.name = name;
			this.price = price;
		}

		// Геттеры и сеттеры
		public virtual long Id
		{
			get
			{
				return id;
			}
			set
			{
				this.id = value;
			}
		}


		public virtual string Name
		{
			get
			{
				return name;
			}
			set
			{
				this.name = value;
			}
		}


		public virtual double Price
		{
			get
			{
				return price;
			}
			set
			{
				this.price = value;
			}
		}


		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			if (o == null || this.GetType() != o.GetType())
			{
				return false;
			}
			Product product = (Product) o;
			return id == product.id && product.price.CompareTo(price) == 0 && name == product.name;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(id, name, price);
		}

		public override string ToString()
		{
			return "Product{" + "id=" + id + ", name='" + name + '\'' + ", price=" + price + '}';
		}
	}
}
