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

            //Start
            databaseConnection.Open();

            string query = "select * from Products";
            SqlDataAdapter adapter = new SqlDataAdapter(query, databaseConnection);

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            productsDataGridView.DataSource = ds.Tables[0];

            databaseConnection.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
