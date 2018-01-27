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
                    if (connection == null)
                    {
                        throw new Exception();
                    }
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
            }

            dcs.SaveConfiguration(dcd);
        }
    }
}
