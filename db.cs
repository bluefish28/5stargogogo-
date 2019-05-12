using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
public class db
{
    public static SqlConnection CreateConnection()
    {
        SqlConnection con = new SqlConnection("Data Source=MSI\\MYSQL;Initial Catalog=user'sdata;Integrated Security=True");
        return con;
    }
}
