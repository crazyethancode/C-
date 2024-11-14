using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data.SQLite;

namespace Lab_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=mybd.db;Version=3;"))
            {
                conn.Open();

                string sql = "INSERT INTO employees(surname,address) VALUES(@surname, @address)";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@surname", sr.Text);
                    cmd.Parameters.AddWithValue("@address", ad.Text);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            ////
            DataTable dt = new DataTable();

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=mybd.db;Version=3;"))
            {
                conn.Open();

                string sql = "SELECT * FROM employees";

                using (SQLiteCommand sqcmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader reader = sqcmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }

                conn.Close();
            }

            DataGrid.ItemsSource = dt.DefaultView;

            ///
            id.Text = "";
            sr.Text = "";
            ad.Text = "";

        }

        private void OutputButton_Click(object sender, RoutedEventArgs e)
        {
            sr.Text = "";
            ad.Text = "";

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=mybd.db;Version=3;"))
            {
                conn.Open();
                string query = "SELECT * FROM employees WHERE id = @value";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    int idValue;
                    if (Int32.TryParse(id.Text, out idValue))
                    {
                        cmd.Parameters.AddWithValue("@value", idValue);
                    }
                    else
                    {
                        // Обработка ошибки
                    }

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Обработка результатов
                            sr.Text = reader.GetString(reader.GetOrdinal("surname"));
                            ad.Text = reader.GetString(reader.GetOrdinal("address"));
                        }
                    }
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=mybd.db;Version=3;"))
                {
                    conn.Open();
                    string sql = "UPDATE employees SET surname = @surname, address = @address WHERE id = @id";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@surname", sr.Text);
                        cmd.Parameters.AddWithValue("@address", ad.Text);
                        cmd.Parameters.AddWithValue("@id", id.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

                //
                DataTable dt = new DataTable();

                using (SQLiteConnection conn = new SQLiteConnection("Data Source=mybd.db;Version=3;"))
                {
                    conn.Open();

                    string sql = "SELECT * FROM employees";

                    using (SQLiteCommand sqcmd = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataReader reader = sqcmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }

                    conn.Close();
                }

                DataGrid.ItemsSource = dt.DefaultView;

            }
            catch (Exception)
            { }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=mybd.db;Version=3;"))
            {
                conn.Open();

                string sql = "DELETE FROM employees WHERE id = @key";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@key", int.Parse(id.Text));
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
            ///
            DataTable dt = new DataTable();

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=mybd.db;Version=3;"))
            {
                conn.Open();

                string sql = "SELECT * FROM employees";

                //using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                using (SQLiteCommand sqcmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader reader = sqcmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }

                conn.Close();
            }

            DataGrid.ItemsSource = dt.DefaultView;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //////
            string cs = "Data Source=mybd.db";

            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS employees(id INTEGER PRIMARY KEY, surname TEXT, address TEXT)";
            cmd.ExecuteNonQuery();

            //cmd.CommandText = @"INSERT INTO monitors(brand, model, size, resolution) VALUES('Samsung', 'Odyssey G7', 32, '2560x1440'),
            //                                                                          ('LG', 'UltraGear', 27, '3840x2160'),
            //                                                                          ('Dell', 'Alienware', 34, '3440x1440'),
            //                                                                          ('ASUS', 'ROG Swift', 24, '1920x1080'),
            //                                                                          ('Acer', 'Predator X27', 27, '3840x2160')";
            //cmd.ExecuteNonQuery();

            cmd.CommandText = "SELECT * FROM employees LIMIT 1";
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                //Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetString(2)} {rdr.GetInt32(3)} {rdr.GetString(4)}");
                //Button1.Content = rdr.GetString(1);
            }

            DataTable dt = new DataTable();

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=mybd.db;Version=3;"))
            {
                conn.Open();

                string sql = "SELECT * FROM employees";

                //using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                using (SQLiteCommand sqcmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader reader = sqcmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }

                conn.Close();
            }

            DataGrid.ItemsSource = dt.DefaultView;

        }
    }
}
