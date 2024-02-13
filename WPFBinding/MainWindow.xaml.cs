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
using System.IO;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace WPFBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ConnectionString = "Data Source=MojaBaza.db;Version=3;";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string idToSave = idBox.Text;
            string nameToSave = nameBox.Text;
            string ageToSave = ageBox.Text;

            if(
                !string.IsNullOrEmpty(ageToSave) && !string.IsNullOrWhiteSpace(ageToSave)
                && !string.IsNullOrEmpty(idToSave) && !string.IsNullOrWhiteSpace(idToSave)
                && !string.IsNullOrEmpty(nameToSave) && !string.IsNullOrWhiteSpace(nameToSave)
               ) 
            {
                using(SQLiteConnection conn = new SQLiteConnection(ConnectionString)) 
                { 
                    conn.Open();

                    string query = "INSERT INTO UserNames (id,name,age) VALUES (@saveId,@ageId,@nameId)";
                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@saveId", idToSave);
                        command.Parameters.AddWithValue("@ageId", ageToSave);
                        command.Parameters.AddWithValue("@nameId", nameToSave);
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show($"Dane został zapisane ({idToSave} {ageToSave} {nameToSave}) została zapisana");
            } else
            {
                MessageBox.Show("SDADSADSAD NIE DIZLAA");
            }
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = System.IO.Path.Combine(desktopPath, "mojedane.txt");
                File.WriteAllText(filePath, $"{idToSave} {nameToSave} {ageToSave}");
                MessageBox.Show("Wszystko zapisane");
            }   
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas zapisywanie " + ex.Message);
            }
        }
    }
}
