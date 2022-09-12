﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace ClassGenerator
{
    internal class SqlHandler
    {
        private string ConnectionString { get; set; } = "";

        public SqlHandler(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public DataTable? ReadDataTable(string Query)
        {
            try
            {
                DataTable DataTable = new DataTable();
                SqlConnection Connection = new SqlConnection(ConnectionString);
                using (SqlCommand Command = new SqlCommand(Query, Connection))
                {
                    using (Connection)
                    {
                        Connection.Open();
                        using (var DataReader = Command.ExecuteReader())
                        {
                            DataTable.Load(DataReader);
                        }
                    }
                }
                return DataTable;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("DataHandler > Sqlhandler > Read " + Ex.Message);
                return null;
            }
        }

        public DataRow ReadDataRow(string Query)
        {
            try
            {
                DataTable DataTable = new DataTable();
                SqlConnection Connection = new SqlConnection(ConnectionString);
                using (SqlCommand Command = new SqlCommand(Query, Connection))
                {
                    using (Connection)
                    {
                        Connection.Open();
                        using (var DataReader = Command.ExecuteReader())
                        {
                            DataTable.Load(DataReader);
                        }
                    }
                }
                return DataTable.Rows[0];
            }
            catch (Exception Ex)
            {
                throw new Exception("DataHandler > Sqlhandler > Read " + Ex.Message);
            }
        }

        public bool Write(string Query)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                using (SqlCommand Command = new SqlCommand(Query, Connection))
                {
                    using (Connection)
                    {
                        Connection.Open();
                        Command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception Ex)
            {
                throw new Exception("DataHandler > Sqlhandler > Write " + Ex.Message);
            }
        }

        public string ExecuteScalar(string Query)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                string ReturnValue = "";
                using (SqlCommand Command = new SqlCommand(Query, Connection))
                {
                    using (Connection)
                    {
                        Connection.Open();
                        var Value = Command.ExecuteScalar();
                        ReturnValue = Value != null ? Value.ToString() : "";
                    }
                }
                return ReturnValue;
            }
            catch (Exception Ex)
            {
                throw new Exception("DataHandler > Sqlhandler > ExecuteScalar " + Ex.Message);
            }
        }

        public bool ExecuteNonQuery(string Query)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                using (SqlCommand Command = new SqlCommand(Query, Connection))
                {
                    using (Connection)
                    {
                        Connection.Open();
                        Command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception Ex)
            {
                throw new Exception("DataHandler > Sqlhandler > ExecuteNonQuery " + Ex.Message);
            }
        }
    }
}
