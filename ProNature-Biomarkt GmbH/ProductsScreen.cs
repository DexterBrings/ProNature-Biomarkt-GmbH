using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProNature_Biomarkt_GmbH
{
    public partial class ProductsScreen : Form
    {
        // Datenbankverbindung
        private SqlConnection databaseConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\dexte\OneDrive\Dokumente\ProNature-Biomarkt-GmbH.mdf;Integrated Security = True; Connect Timeout = 30");
        public ProductsScreen()
        {
            InitializeComponent();
            
            ShowProducts();
            
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(textBoxName.Text == ""
                || textBoxBrand.Text == ""
                || comboBoxCategorie.Text == ""
                || textBoxPrice.Text == "")
            {
                MessageBox.Show("Bitte fülle alle Felder aus.");
            }

            string name = textBoxName.Text; ;
            string brand = textBoxBrand.Text;
            string categorie = comboBoxCategorie.Text;
            string price = textBoxPrice.Text;

            databaseConnection.Open();
            string query = string.Format("insert into Products values('{0}', '{1}', '{2}', '{3}')", name, brand, categorie, price);
            SqlCommand sqlCommand = new SqlCommand(query, databaseConnection);
            sqlCommand.ExecuteNonQuery();
            databaseConnection.Close();

            ClearAllFields();
            ShowProducts();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ClearAllFields();
            ShowProducts();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ClearAllFields();
            ShowProducts();
        }

        private void ShowProducts()
        {
            databaseConnection.Open();

            string query = "select * from Products";
            SqlDataAdapter adapter = new SqlDataAdapter(query, databaseConnection);

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            productsDataGridView.DataSource = ds.Tables[0];
            productsDataGridView.Columns[0].Visible = false;

            databaseConnection.Close();
        }

        private void ClearAllFields()
        {
            textBoxName.Text = "";
            textBoxBrand.Text = "";
            textBoxPrice.Text = "";
            comboBoxCategorie.SelectedItem = null;
        }

        private void productsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxName.Text = productsDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            textBoxBrand.Text = productsDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            comboBoxCategorie.Text = productsDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            textBoxPrice.Text = productsDataGridView.SelectedRows[0].Cells[4].Value.ToString();
        }
    }
}
