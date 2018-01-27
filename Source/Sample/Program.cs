//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace Microsoft.Data.ConnectionUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DataConnectionDialog dcd = new DataConnectionDialog();
            DataConnectionConfiguration dcs = new DataConnectionConfiguration(null);
            dcs.LoadConfiguration(dcd);

            if (DataConnectionDialog.Show(dcd) == DialogResult.OK)
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(dcd.SelectedDataProvider.Name);
                using (var connection = factory.CreateConnection())
                {
                    connection.ConnectionString = dcd.ConnectionString;
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM INFORMATION_SCHEMA.TABLES";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["name"]);
                        }
                    }
                }

                // load tables
                //using (SqlConnection connection = new SqlConnection(dcd.ConnectionString))
                //{
                //    connection.Open();
                //    SqlCommand cmd = new SqlCommand("SELECT * FROM sys.Tables", connection);

                //    using (SqlDataReader reader = cmd.ExecuteReader())
                //    {
                //        while (reader.Read())
                //        {
                //            Console.WriteLine(reader.HasRows);
                //        }
                //    }

                //}
            }

            dcs.SaveConfiguration(dcd);
        }
    }
    // Sample 1: 
    //[STAThread]
    //static void Main(string[] args)
    //{
    //	DataConnectionDialog dcd = new DataConnectionDialog();
    //	DataConnectionConfiguration dcs = new DataConnectionConfiguration(null);
    //	dcs.LoadConfiguration(dcd);

    //	if (DataConnectionDialog.Show(dcd) == DialogResult.OK)
    //	{
    //              DbProviderFactory factory = DbProviderFactories.GetFactory(dcd.SelectedDataProvider.Name);
    //              using (var connection = factory.CreateConnection())
    //              {
    //                  connection.ConnectionString = dcd.ConnectionString;
    //                  connection.Open();
    //                  var command = connection.CreateCommand();
    //                  command.CommandType = CommandType.Text;
    //                  command.CommandText = "SELECT * FROM INFORMATION_SCHEMA.TABLES";
    //                  using (var reader = command.ExecuteReader())
    //                  {
    //                      while (reader.Read())
    //                      {
    //                          Console.WriteLine(reader["name"]);
    //                      }
    //                  }
    //              }

    //		// load tables
    //              //using (SqlConnection connection = new SqlConnection(dcd.ConnectionString))
    //              //{
    //              //    connection.Open();
    //              //    SqlCommand cmd = new SqlCommand("SELECT * FROM sys.Tables", connection);

    //              //    using (SqlDataReader reader = cmd.ExecuteReader())
    //              //    {
    //              //        while (reader.Read())
    //              //        {
    //              //            Console.WriteLine(reader.HasRows);
    //              //        }
    //              //    }

    //              //}
    //	}

    //	dcs.SaveConfiguration(dcd);
    //}

    // Sample 2: 
    //[STAThread]
    //static void Main(string[] args)
    //{
    //    DataConnectionDialog dcd = new DataConnectionDialog();
    //    DataConnectionConfiguration dcs = new DataConnectionConfiguration(null);
    //    dcs.LoadConfiguration(dcd);
    //    //dcd.ConnectionString = "Data Source=ziz-vspro-sql05;Initial Catalog=Northwind;Persist Security Info=True;User ID=sa;Password=Admin_007";


    //    if (DataConnectionDialog.Show(dcd) == DialogResult.OK)
    //    {
    //        // load tables
    //        using (SqlConnection connection = new SqlConnection(dcd.ConnectionString))
    //        {
    //            connection.Open();
    //            SqlCommand cmd = new SqlCommand("SELECT * FROM sys.Tables", connection);

    //            using (SqlDataReader reader = cmd.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    Console.WriteLine(reader.HasRows);
    //                }
    //            }

    //        }
    //    }

    //    dcs.SaveConfiguration(dcd);
    //}

}
