using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Common;
using MySql.Data.MySqlClient;
using GameServer.Tool;
using GameServer.Model;
using GameServer.DAO;
namespace GameServer.Servers
{
    class Client
    {
        private Socket clientSocket;
        private Server server;
        private MySqlConnection mysqlConn;

        private User user;
        private Result result;
        private Room room;
        private ResultDAO resultDAO = new ResultDAO();
        public int Score
        {
            get;set;
        }


        public bool AllGem(int gem)
        {
            
            Console.WriteLine("@@@@@"+Score);
            Score = Math.Min(Score, 50);
            
            if (Score >= 50) return true;
            return false;
        }
        public void SetUserData(User User,Result Result)
        {
            this.user = User;
            this.result = Result;
        }
        public Room Room
        {
            set { room = value; }
            get { return room; }
        }
        public string GetUserData()
        {
            return user.Id+","+user.Username + "," + result.TotalCount + "," + result.WinCount;
        }
        public MySqlConnection MysqlConn
        {
            get { return mysqlConn; }
        }
        private Message msg = new Message();
        public Client()
        {

        }
        public Client(Socket clientSocket,Server server)
        {
            this.clientSocket = clientSocket;  //this.  代表全局变量
            this.server = server;
            mysqlConn = ConnHelper.Connect();
        }
        public void Start()
        {
            if (clientSocket == null || clientSocket.Connected == false) return;
            clientSocket.BeginReceive(msg.Data,msg.StartIndex,msg.RemainSize, SocketFlags.None, ReceiveCallBack, null);
        }
        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                if (clientSocket == null || clientSocket.Connected == false) return;
                int count = clientSocket.EndReceive(ar);
                if (count == 0)
                {
                    Close();
                }
                msg.ReadMessage(count,OnProcessMessage);
                Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Close();
            }

        }
        private void OnProcessMessage(RequestCode requestCode,ActionCode actionCode,string data)
        {

            server.HandleRequest(requestCode, actionCode, data, this);  
        }
        private void Close()
        {
            ConnHelper.CloseConnection(mysqlConn);
            if (clientSocket != null)
                clientSocket.Close();
            if (room != null)
            {
                room.Close(this);
            }
            server.RemoveClient(this);

        }
        public void Send(ActionCode actionCode,string data)
        {
            byte[] bytes = Message.PackData(actionCode, data);
            clientSocket.Send(bytes);
        }
        public int GetUserId()
        {
            return user.Id;
        }
        public bool IsOwner()
        {
            return room.IsOwner(this);
        }





        public void SetScore(int gem)
        {
            //return Score >= 50;
            Score += gem;
            
        }
        public int GetScore()
        {
            return Score;
        }



        public void UpdateResult(bool isVictory)
        {
            UpdateResultToDB(isVictory);
            UpdateResultToClient();
        }
        private void UpdateResultToDB(bool isVictory)
        {
            result.TotalCount++;
            if (isVictory)
            {
                result.WinCount++;
            }
            resultDAO.UpdateOrAddResult(mysqlConn, result);
        }
        private void UpdateResultToClient()
        {
            Send(ActionCode.UpdateResult, string.Format("{0},{1}", result.TotalCount, result.WinCount));
        }
    }
}
