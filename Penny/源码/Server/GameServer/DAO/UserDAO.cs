using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using GameServer.Model;

namespace GameServer.DAO
{
    class UserDAO
    {
        public User VerifyUser(MySqlConnection conn,string username,string password)
        {
            MySqlDataReader reader=null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from user where username=@username and password=@password", conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                reader = cmd.ExecuteReader();//将cmd语句送到数据库接口 并返回一个MySqlDataReader用来判断是否读取成功
                Console.WriteLine("数据库正在读取");
                if (reader.Read())
                {
                    Console.WriteLine("读取成功！！！");
                    int id = reader.GetInt32("id");
                    User user = new User(id, username, password);
                    return user;
                }
                else
                {
                    Console.WriteLine("数据库中无此账号");
                    return null;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("在VerifyUser中出现异常"+e);
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
           
        }
        public bool GetUserByUsername(MySqlConnection conn, string username)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from user where username=@username ", conn);
                cmd.Parameters.AddWithValue("username", username);
                reader = cmd.ExecuteReader();//将cmd语句送到数据库接口 并返回一个MySqlDataReader用来判断是否读取成功
                Console.WriteLine("-----查询该信息是否已注册---");
                if (reader.HasRows)
                {
                    Console.WriteLine("--数据库中已有该用户---");
                    return false;
                }
                else
                {
                   
                    return true;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("在GetUser中出现异常" + e);
                return false;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
        public void RegisterUser(MySqlConnection conn, string username, string password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into user set username=@username , password=@password", conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                cmd.ExecuteNonQuery();
                Console.WriteLine("----正在注册用户信息---");
            }
            catch (Exception e)
            {

                Console.WriteLine("在RegisterUser中出现异常" + e);
            }
        }
    }
}
