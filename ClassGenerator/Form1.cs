using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ClassGenerator
{
    public partial class Form1 : Form
    {
        private SqlConnectionStringBuilder sqlConnectionStringBuilder { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void ConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox Combo = (ComboBox)sender;
            switch (Combo.SelectedIndex)
            {
                case 0://Integrated security
                    CredentialsGroupBox.Enabled = false;
                    break;
                case 1://Username and password
                    CredentialsGroupBox.Enabled = true;
                    break;
            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            CatalogComboBox.Items.Clear();
            bool ValidationError = false;
            ValidationError = string.IsNullOrEmpty(Server.Text) ? true : ValidationError;
            ValidationError = ConnectionType.SelectedIndex == -1 ? true : ValidationError;
            if (ValidationError)
            {
                MessageBox.Show("Please enter the required information");
            }
            else
            {
                sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
                sqlConnectionStringBuilder.DataSource = Server.Text;
                sqlConnectionStringBuilder.IntegratedSecurity = ConnectionType.SelectedIndex == 0 ? true : false;

                if (ConnectionType.SelectedIndex == 1)
                {
                    sqlConnectionStringBuilder.UserID = Username.Text;
                    sqlConnectionStringBuilder.Password = ConnectionPassword.Text;
                }

                SqlHandler Sql = new SqlHandler(sqlConnectionStringBuilder.ConnectionString);
                var Catalogs = Sql.ReadDataTable("SELECT name FROM sys.databases order by name");
                if (Catalogs != null)
                {
                    foreach (DataRow Table in Catalogs.Rows)
                    {
                        CatalogComboBox.Items.Add(Table["name"].ToString());
                    }

                    GenerateCodeGroupBox.Enabled = true;
                }
            }
        }



        private void GenerateCodeButton_Click(object sender, EventArgs e)
        {
            bool ValidationError = false;
            ValidationError = TableName.SelectedIndex == -1 ? true : ValidationError;
            ValidationError = string.IsNullOrEmpty(NameSpace.Text) ? true : ValidationError;
            if (ValidationError)
            {
                MessageBox.Show("Please enter all required information");
            }
            else
            {
                if (sqlConnectionStringBuilder != null)
                {
                    string Table = TableName.Text;

                    StringBuilder Sql = new StringBuilder();
                    Sql.Append(" SELECT COLUMN_NAME, DATA_TYPE");
                    Sql.Append(" FROM INFORMATION_SCHEMA.COLUMNS");
                    Sql.Append(" WHERE TABLE_NAME = N'" + Table + "'");
                    SqlHandler SqlHandler = new SqlHandler(sqlConnectionStringBuilder.ConnectionString);
                    var Columns = SqlHandler.ReadDataTable(Sql.ToString());
                    if(Columns != null)
                    {
                        CodeGenerator Generator = new CodeGenerator(Table, NameSpace.Text, Columns);
                        OutputBox.Text = Generator.GenerateClass();
                    }
                }
                else
                {
                    MessageBox.Show("Connection string is empty");
                }
            }
        }

        private void CatalogComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableName.Items.Clear();
            sqlConnectionStringBuilder.InitialCatalog = CatalogComboBox.SelectedItem.ToString();
            SqlHandler Sql = new SqlHandler(sqlConnectionStringBuilder.ConnectionString);

            var Tables = Sql.ReadDataTable("SELECT TABLE_NAME FROM information_schema.tables order by TABLE_NAME");
            if(Tables != null)
            {
                foreach (DataRow Table in Tables.Rows)
                {
                    TableName.Items.Add(Table["Table_Name"].ToString());
                }
            }
        }
    }
}