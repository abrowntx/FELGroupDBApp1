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
using System.Windows.Shapes;
using System.Data.OleDb;
using System.Data;

namespace basicDBapp
{
    /// <summary>
    /// Interaction logic for entry.xaml
    /// </summary>
    public partial class entry : Window
    {
        public entry()
        {
            InitializeComponent();
            tbDate.Text = DateTime.Today.ToShortDateString();
            cmbDep.Items.Add("Transfer Pricing");
            cmbDep.Items.Add("Finance and Securities");
            cmbDep.Items.Add("Anti-Trust");
            cmbDep.SelectedIndex = 0;
        }

        private void EnterData()
        {
            string curFolder = System.AppDomain.CurrentDomain.BaseDirectory;
            string CS = "Provider = Microsoft.ACE.OLEDB.12.0;Data Source =" + curFolder + "databse.Accdb;";
            using (var conn1 = new OleDbConnection(CS))
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = ("INSERT INTO clients " +
                        "(name,email,phone,department,title,biolink,casework,publications,dateadded) " +
                        "VALUES(" +
                        "'" + tbName.Text + "'," +
                        "'" + tbEmail.Text + "'," +
                        "'" + tbPhone.Text + "'," +
                        "'" + cmbDep.SelectedValue.ToString() + "'," +
                        "'" + tbTitle.Text + "'," +
                        "'" + tbBio.Text + "'," +
                        "'" + tbCasework.Text + "'," +
                        "'" + tbPub.Text + "'," +
                        "'" + tbDate.Text + "'" +
                        ");");
                    cmd.Connection = conn1;
                    conn1.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Success!");
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
                finally
                { conn1.Close(); }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            EnterData();
            this.Close();
        }
    }
}
