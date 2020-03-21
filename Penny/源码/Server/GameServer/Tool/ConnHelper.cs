﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GameServer.Tool
{
    class ConnHelper
    {
        public  const string CONNECTIONSTRING = "datasource=127.0.0.1;port=3306;database=game;user=root;password=root;";
        public static MySqlConnection Connect()
        {
            MySqlConnection conn = new MySqlConnection(CONNECTIONSTRING);
            try
            {
                conn.Open();
                Console.WriteLine("---数据库连接成功---");
                return conn;
            }
            catch (Exception e)
            {

                Console.WriteLine("连接数据库时发生异常：" + e);
                return null;//*
            }

        }
        public static void CloseConnection(MySqlConnection conn)
        {
            if(conn!=null)
            conn.Close();
            else
            {
                Console.WriteLine("MySqlConnection不能为空");
            }
        }
    }
}
