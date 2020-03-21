using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Threading;
namespace GameServer.Servers
{
    enum RoomState
    {
        WaitingJoin,
        WaitingStart,
        Start,
        End
    }
    class Room
    {
        private int inital_score = 0;
        private List<Client> clientRoom = new List<Client>();
        private RoomState state = RoomState.WaitingJoin;
        private Server server;

        private bool isRun=false;
        public Room(Server server)
        {
            this.server = server;
        }
        public bool IsRoomWaitingjoin()
        {
            return state == RoomState.WaitingJoin;
        }
        public void AddClient(Client client)
        {
            client.Score = inital_score;
            clientRoom.Add(client);
            client.Room = this;
            if (clientRoom.Count > 1)
            {
                state = RoomState.Start;
            }
        }
        public void RemoveClient(Client client)
        {
            client.Room = null;
            clientRoom.Remove(client);
            if (clientRoom.Count > 1)
            {
                state = RoomState.WaitingStart;
            }
            else
            {
                state = RoomState.WaitingJoin;
            }
        }
        public string GetRoomOwnerData()
        {
            return clientRoom[0].GetUserData();
        }
        public void Close(Client client)
        {
            if (client == clientRoom[0])
                server.RemoveRoom(this);
            else
                server.RemoveClient(client);
        } 
        public int GeOwnerId()
        {
            if (clientRoom.Count > 0)
            {
                return clientRoom[0].GetUserId();
            }
            return -1;
        }
        public string GetAllData()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Client  client in clientRoom)
            {
                sb.Append(client.GetUserData() + "_");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
        public void BroadcastMessage(Client exClient,ActionCode actionCode,string data)
        {
            foreach (Client  client in clientRoom)
            {
                if (client != exClient)
                {
                    server.SendResponse(client, actionCode, data);
                }
            }
        }
        public bool IsOwner(Client client)
        {
            return client == clientRoom[0];
        }
        public void CloseRoom()
        {
            foreach (Client client in clientRoom)
            {
                client.Room = null;
            }
            server.RemoveRoom(this);
        }
        public void StartTime()
        {
            isRun = true;
            new Thread(RunTime).Start();
        }
        private void RunTime()
        {

                Thread.Sleep(1000);

                for (int i = 30; i > 0; i--)
                {
                Console.WriteLine("#####"+isRun+"######");
                if (isRun)
                {
                    BroadcastMessage(null, ActionCode.ShowTime, i.ToString());
                    Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
                }
                BroadcastMessage(null, ActionCode.Play, "PLAY");
           
        }


        //获得金币计算
        public void TakeGem(int gem, Client excludeClient)
        {
            isRun = false;
            Console.WriteLine("------TAKEGEM---"+gem+"----");
            bool isAchieve = false;
            foreach (Client client in clientRoom)
            {
                if (client != excludeClient)
                {
                //如果其中一个角色得到50分，要结束游戏
                if (client.AllGem(gem))
                    {
                        
                      isAchieve = true;
                      client.UpdateResult(true);
                      Console.WriteLine("Success");
                      client.Send(ActionCode.GameOver, ((int)ReturnCode.Success).ToString());
                        excludeClient.UpdateResult(false);
                        Console.WriteLine("Fail");
                        client.Send(ActionCode.GameOver, ((int)ReturnCode.Fail).ToString());
                    }

                }
                //else if (excludeClient.AllGem(gem))
                //{
                //    isRun = false;
                //    isAchieve = true;
                //    client.UpdateResult(false);
                //    Console.WriteLine("Fail");
                //    client.Send(ActionCode.GameOver, ((int)ReturnCode.Fail).ToString());
                //    excludeClient.UpdateResult(true);
                //    Console.WriteLine("Success");
                //    client.Send(ActionCode.GameOver, ((int)ReturnCode.Success).ToString());
                //}
            }
            if (isAchieve) return;
            int currentScore = excludeClient.GetScore();
            foreach (Client client in clientRoom)
            {
                if (client != excludeClient)
                {
                    if (client.GetScore() > currentScore)
                    {
                        client.UpdateResult(true);
                        Console.WriteLine("Success");
                        client.Send(ActionCode.GameOver, ((int)ReturnCode.Success).ToString());
                        excludeClient.UpdateResult(false);
                        Console.WriteLine("Fail");
                        excludeClient.Send(ActionCode.GameOver, ((int)ReturnCode.Fail).ToString());
                    }
                    else
                    {
                        client.UpdateResult(false);
                        Console.WriteLine("Fail");
                        client.Send(ActionCode.GameOver, ((int)ReturnCode.Fail).ToString());
                        excludeClient.UpdateResult(true);
                        Console.WriteLine("Success");
                        excludeClient.Send(ActionCode.GameOver, ((int)ReturnCode.Success).ToString());
                    }
                }
            }
            //if (isAchieve == false) return;

           /* foreach (Client client in clientRoom)
            {

                if (client.IsAchieve())
                {
                    client.UpdateResult(false);
                    Console.WriteLine("Success");
                    client.Send(ActionCode.GameOver, ((int)ReturnCode.Success).ToString());
                }
                else
                {
                    client.UpdateResult(true);
                    Console.WriteLine("Fail");
                    client.Send(ActionCode.GameOver, ((int)ReturnCode.Fail).ToString());
                }
            }*/
            Close();
        }




        public void Close()
        {
            foreach (Client client in clientRoom)
            {
                client.Room = null;
            }
            server.RemoveRoom(this);
        }
    }
}
