using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Model;
using MySql.Data.MySqlClient;
namespace GameServer.DAO
{
    class ResultDAO
    {
        public Result GetResultByUserId(MySqlConnection conn,int userId)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from result where userid=@userid", conn);
                cmd.Parameters.AddWithValue("userid", userId);
                reader = cmd.ExecuteReader();//将cmd语句送到数据库接口 并返回一个MySqlDataReader用来判断是否读取成功
                Console.WriteLine("数据库正在读取");
                if (reader.Read())
                {
                    Console.WriteLine("读取成功！！！");
                    int id = reader.GetInt32("id");
                    int totalCount = reader.GetInt32("totalCount");
                    int winCount = reader.GetInt32("wincount");
                    Result result = new Result(id, userId, totalCount, winCount);
                    return result;
                }
                else
                {
                    Result result = new Result(-1, userId, 0, 0);
                    return result;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("在GetResultByUserId中出现异常" + e);
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="res"></param>
        public void UpdateOrAddResult(MySqlConnection conn, Result res)
        {
            try
            {
                MySqlCommand cmd = null;

                if (res.Id <= -1)
                {
                    cmd = new MySqlCommand("insert into result set totalcount=@totalcount,wincount=@wincount,userid=@userid", conn);
                }
                else
                {
                    cmd = new MySqlCommand("update result set totalcount=@totalcount,wincount=@wincount where userid=@userid ", conn);
                }
                cmd.Parameters.AddWithValue("totalcount", res.TotalCount);
                cmd.Parameters.AddWithValue("wincount", res.WinCount);
                cmd.Parameters.AddWithValue("userid", res.UserId);
                cmd.ExecuteNonQuery();
                if (res.Id <= -1)
                {
                    Result tempRes = GetResultByUserId(conn, res.UserId);
                    res.Id = tempRes.Id;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在UpdateOrAddResult的时候出现异常：" + e);
            }
        }
    }
}
