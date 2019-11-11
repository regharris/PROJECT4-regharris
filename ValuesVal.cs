using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ugh.Models;
using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlTypes;

namespace ugh
{
    public class ValuesVal
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;
        public ValuesVal()
        {
            string connect = "server=localhost;user=root;database=myProducts;port=3306;password=password";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = connect;
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT * FROM products";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader exec = cmd.ExecuteReader();

                while (exec.Read())
                {
                    Console.WriteLine(exec[0] + " - " + exec[2] + " - " + exec[1] + " - -" + exec[4]);
                }
                exec.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            Console.WriteLine("Done.");
        }

        public Values getValues(int id)
        {
            Values v = new Values();
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
            string sqlString = $"SELECT * FROM products WHERE id ={id}";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySQLReader = cmd.ExecuteReader();

            if (mySQLReader.Read())
            {
                v.id = mySQLReader.GetInt32(0);
                v.productName = mySQLReader.GetString(1);
                v.productCategory = mySQLReader.GetString(2);
                v.productDescription = mySQLReader.GetString(3);
                v.productImage = mySQLReader.GetString(4);

                return v;

            }
            else
            {
                return null;
            }



        }
        public ArrayList allValues()
        {
            ArrayList valueArray = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
            string sqlString = $"SELECT * FROM products";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySQLReader = cmd.ExecuteReader();

            while (mySQLReader.Read())
            {
                Values v = new Values();
                v.id = mySQLReader.GetInt32(0);
                v.productName = mySQLReader.GetString(1);
                v.productCategory = mySQLReader.GetString(2);
                v.productDescription = mySQLReader.GetString(3);
                v.productImage = mySQLReader.GetString(4);

                valueArray.Add(v);

            }
            return valueArray;



        }

        public int allPeople(Values personToSave)
        {

            string query = $"INSERT INTO products (productName,productCategory,productDescription,productImage) VALUES('{personToSave.productName}','{personToSave.productCategory}','{personToSave.productDescription}','{personToSave.productImage}')";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();

            long id = cmd.LastInsertedId;
            return (int)id;
        }

        public bool deleteVal(int id)
        {

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
            string sqlString = $"SELECT * FROM products WHERE id ={id}";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySQLReader = cmd.ExecuteReader();

            if (mySQLReader.Read())
            {
                mySQLReader.Close();
                sqlString = $"DELETE FROM products WHERE id ={id}";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();
                return true;

            }
            else
            {
                return false;
            }


        }
        public bool updateVal(int id, Values personToSave)
        {
            Values v = new Values();
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
            string sqlString = $"SELECT * FROM products WHERE id ={id}";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySQLReader = cmd.ExecuteReader();

            if (mySQLReader.Read())
            {
                mySQLReader.Close();
                sqlString = $"UPDATE products SET productName = '{personToSave.productName}',productCategory='{personToSave.productCategory}', productDescription=' {personToSave.productDescription}',productImage='{personToSave.productImage}' WHERE id ={id} ";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();
                return true;

            }
            else
            {
                return false;
            }


        }



    }


}