namespace org.example.repository
{
    public class Product
    {
        private int id;
        private string name;
        private double price;

        public Product(int id, string name, double price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }

        public virtual int Id
        {
            get
            {
                return id;
            }
        }

        public virtual string Name
        {
            get
            {
                return name;
            }
        }

        public virtual double Price
        {
            get
            {
                return price;
            }
        }
    }
}
