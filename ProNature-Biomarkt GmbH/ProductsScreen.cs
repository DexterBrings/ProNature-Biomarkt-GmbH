using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProNature_Biomarkt_GmbH
{
    public partial class ProductsScreen : Form
    {
        // Datenbankverbindung
        private SqlConnection databaseConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\dexte\OneDrive\Dokumente\ProNature-Biomarkt-GmbH.mdf;Integrated Security = True; Connect Timeout = 30");

        //Hilfsvariable
        private int lastSelectedProductKey;
        public ProductsScreen()
        {
            InitializeComponent();
            
            ShowProducts();
            
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(textBoxName.Text == ""
                || textBoxBrand.Text == ""
                || comboBoxCategory.Text == ""
                || textBoxPrice.Text == "")
            {
                MessageBox.Show("Bitte fülle alle Felder aus.");
            }

            string name = textBoxName.Text; ;
            string brand = textBoxBrand.Text;
            string category = comboBoxCategory.Text;
            string price = textBoxPrice.Text;

            ExecuteQuery(string.Format("insert into Products values('{0}', '{1}', '{2}', '{3}')", name, brand, category, price));
            
            ClearAllFields();
            ShowProducts();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(lastSelectedProductKey == 0) {
                MessageBox.Show("Bitte wähle einen Artikel aus.");
                return;
            }

            string query = string.Format("update Products set Name='{0}', Brand='{1}', Category='{2}', Price='{3}' where Id={4}",
                textBoxName.Text, textBoxBrand.Text, comboBoxCategory.Text, textBoxPrice.Text, lastSelectedProductKey);
            ExecuteQuery(query);
            
            ClearAllFields();
            ShowProducts();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lastSelectedProductKey == 0) {
                MessageBox.Show("Bitte wähle einen Artikel aus.");
                return;
            }

            ExecuteQuery(string.Format("delete from Products where ID={0};", lastSelectedProductKey));

            ClearAllFields();
            ShowProducts();
        }

        private void ExecuteQuery(string query)
        {
            databaseConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, databaseConnection);
            sqlCommand.ExecuteNonQuery();
            databaseConnection.Close();
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
            comboBoxCategory.SelectedItem = null;
        }

        private void productsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxName.Text = productsDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            textBoxBrand.Text = productsDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            comboBoxCategory.Text = productsDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            textBoxPrice.Text = productsDataGridView.SelectedRows[0].Cells[4].Value.ToString();

            lastSelectedProductKey = (int)productsDataGridView.SelectedRows[0].Cells[0].Value;
        }
    }
}
