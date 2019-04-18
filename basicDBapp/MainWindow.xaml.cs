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
using System.Data.OleDb;
using System.Data;

namespace basicDBapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void QueryandFill()
        {
            string curFolder = System.AppDomain.CurrentDomain.BaseDirectory;
            string CS = "Provider = Microsoft.ACE.OLEDB.12.0;Data Source =" + curFolder + "databse.Accdb;";
            using (var conn1 = new OleDbConnection(CS))
            {
                try
                {
                    conn1.Open();
                    OleDbDataAdapter DA = new OleDbDataAdapter("SELECT * from clients", conn1);
                    var DT = new DataSet();
                    DA.Fill(DT, "*");
                    DataTable table = new DataTable();
                    table = DT.Tables[0];

                    dgMain.DataContext = table.DefaultView;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
                finally
                { conn1.Close(); }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            QueryandFill();
        }

        private void btnEntry_Click(object sender, RoutedEventArgs e)
        {
            entry E = new entry();
            E.ShowDialog();
        }

        public int ID;
        private void dgMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var drv = dgMain.SelectedItem as DataRowView;
            if (drv == null) { } else { ID = Convert.ToInt32(drv[9]); }
        }

        private void delete(int ID)
        {
            string curFolder = System.AppDomain.CurrentDomain.BaseDirectory;
            string CS = "Provider = Microsoft.ACE.OLEDB.12.0;Data Source =" + curFolder + "databse.Accdb;";
            using (var conn1 = new OleDbConnection(CS))
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = ("DELETE FROM clients WHERE ID = " + ID + ";");
                    cmd.Connection = conn1;
                    conn1.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted!");
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
                finally
                { conn1.Close(); }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            delete(ID);
            QueryandFill();
        }
    }
}
