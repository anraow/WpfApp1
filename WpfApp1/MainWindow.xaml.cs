using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Instrument[] itemsInStock = new[]{
             new Instrument
                {
                    ID = 1001,
                    Name = "Молоток",
                    ProducedBy = "Gigant",
                    Amount = 32,
                    Price = 541,
                    StoreID = 101
                },
                new Instrument
                {
                    ID = 1002,
                    Name = "Дрель",
                    ProducedBy = "Ryobi",
                    Amount = 21,
                    Price = 4643,
                    StoreID = 101
                },
                new Instrument
                {
                    ID = 1003,
                    Name = "Бензопила",
                    ProducedBy = "Denzel",
                    Amount = 15,
                    Price = 8890,
                    StoreID = 102
                },
                new Instrument
                {
                    ID = 1004,
                    Name = "Перфоратор",
                    ProducedBy = "Makita",
                    Amount = 8,
                    Price = 10778,
                    StoreID = 102
                },
                new Instrument
                {
                    ID = 1004,
                    Name = "Тример",
                    ProducedBy = "Ryobi",
                    Amount = 12,
                    Price = 9708,
                    StoreID = 102
                }
        };

        private Store[] stores = new[] {
                new Store{
                        ID = 101,
                        Name = "Для Дома",
                        AmountEmployers = 4
                },
                new Store{
                        ID = 102,
                        Name = "Для Сада",
                        AmountEmployers = 6
                }
            };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxN.Text = "50";
            textBoxM.Text = "330";

            // Заполнить список listBoxInstr элементами
            foreach (Instrument i in itemsInStock)
            {
                listBoxInstr.Items.Add(i.Name);
            }
        }

        private void listBoxInstr_SelectionChanged(object  sender, SelectionChangedEventArgs e)
        {
            textBlockInfo.Text = itemsInStock[listBoxInstr.SelectedIndex].ToString();
        }

        // Получить данные о всех товарах
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var result = from i in itemsInStock select i;
            string s = "";
            foreach (var r in result)
            {
                s += String.Format("{0} ID - {1} {2}шт. {3}руб. Производитель - {4} ID магазина - {5} \n", r.Name, r.ID, r.Amount, r.Price, r.ProducedBy, r.StoreID);
            }
            MessageBox.Show(s, "Данные о всех инструментах");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse(textBoxN.Text);
            var result = from i in itemsInStock where i.Price > n select i.Name;
            string s = "";
            foreach (var r in result) s += string.Format("- {0}\n", r);
            MessageBox.Show(s, "Наименования инструмента с ценой более " + n + " руб.");
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            int A = int.Parse(textBoxA.Text);
            int B = int.Parse(textBoxB.Text);
            var result = from i in itemsInStock where i.Amount > A & i.Amount < B select i.Name;
            string s = String.Format("{0} шт.", result.Count());
            MessageBox.Show(s, "Число наименований инструмента с количеством от " + A + " до " + B + " - ");
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            int M = int.Parse(textBoxM.Text);
            var result = from i in itemsInStock where i.Amount < M select i;
            string s = "";
            foreach (var r in result)
            {
                s += string.Format("- {0} {1}\n", r.ProducedBy, r.Name);
            }
            MessageBox.Show(s, "Производитель и наименование инструмента с количеством менее ");
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            var result = from i in itemsInStock select i;
            double sumCost = 0;
            string s = "";
            foreach (var r in result)
            {
                sumCost += r.Price * r.Amount;
                s += string.Format("{0} - {1}\n", r.Name, sumCost);
            }
            MessageBox.Show(s, " Суммарная стоимость инструмента по каждому наименованию");
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            var groups = from i in itemsInStock group i by i.StoreID;
            string s = "";
            foreach (var group in groups)
            {
                s += string.Format("Товары отдела {0}: \n", group.Key);
                foreach (var elem in group)
                {
                    s += String.Format("{0}\n",elem.Name);
                }
            }
            MessageBox.Show(s, "Все инструменты, сгрупированные кодом отделов (group)");
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            var result = from i in itemsInStock
                         join s in stores on i.StoreID equals s.ID
                         select new
                         {
                             name = i.Name,
                             price = i.Price,
                             nameStore = s.Name,
                             amount = s.AmountEmployers,
                         };
            string str = "";
            foreach (var r in result)
            {
                str += string.Format("{0}: {1} руб., отдел {2}, сотрудников в отделе {3}.\n", r.name, r.price, r.nameStore, r.amount);
            }
            MessageBox.Show(str, "Наименования и цены инструментов с указанием названия отдела и числа сотрудников (join)");
        }
    }

    internal class Instrument
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? ProducedBy { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public int StoreID { get; set; }
    }

    internal class Store
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public int AmountEmployers { get; set; }
    }

}
