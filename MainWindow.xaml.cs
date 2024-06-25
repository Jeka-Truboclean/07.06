using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _07._06
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Product
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }
            public DateTime DateAdded { get; set; }
        }

        private List<Product> products = new List<Product>();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            string name = ProductNameTextBox.Text;
            if (decimal.TryParse(ProductPriceTextBox.Text, out decimal price) && !string.IsNullOrEmpty(name))
            {
                string description = ProductDescriptionTextBox.Text;
                Product newProduct = new Product
                {
                    Name = name,
                    Price = price,
                    Description = description,
                    DateAdded = DateTime.Now
                };
                products.Add(newProduct);
                DisplayProducts();
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Please enter valid product details.");
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Expander selectedExpander = null;
            foreach (UIElement element in ProductListPanel.Children)
            {
                if (element is Expander expander && expander.IsExpanded)
                {
                    selectedExpander = expander;
                    break;
                }
            }

            if (selectedExpander != null)
            {
                Product selectedProduct = selectedExpander.DataContext as Product;
                products.Remove(selectedProduct);
                DisplayProducts();
            }
            else
            {
                MessageBox.Show("Please expand a product to select it for deletion.");
            }
        }

        private void DisplayProducts()
        {
            ProductListPanel.Children.Clear();
            foreach (var product in products)
            {
                Expander expander = new Expander
                {
                    Header = product.Name,
                    DataContext = product,
                    Content = new TextBlock
                    {
                        Text = $"Price: {product.Price:C}\nDescription: {product.Description}\nDate Added: {product.DateAdded:G}"
                    }
                };
                ProductListPanel.Children.Add(expander);
            }
        }

        private void ClearInputFields()
        {
            ProductNameTextBox.Clear();
            ProductPriceTextBox.Clear();
            ProductDescriptionTextBox.Clear();
        }
    }
}