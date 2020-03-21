using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;
namespace GameServer.Controller
{
    class GameController:BaseController
    {
        public GameController()
        {
            requestCode = RequestCode.Game;
        }
        public string StartGame(string data, Client client, Server server)
        {
            Console.WriteLine("--------Start");
            if (client.IsOwner())
            {
                Room room = client.Room;
                room.BroadcastMessage(client, ActionCode.StartGame, ((int)ReturnCode.Success).ToString());//给其他客户端发送
                room.StartTime();
                return ((int)ReturnCode.Success).ToString();//给当前客户端发送
            }

            else
                return ((int)ReturnCode.Fail).ToString();

        }


        public string QuitBattle(string data, Client client, Server server)
        {
            Room room = client.Room;

            if (room != null)
            {
                room.BroadcastMessage(null, ActionCode.QuitBattle, "r");
                room.Close();
            }
            return null;
        }

        public string GetScore(string data, Client client, Server server)
        {
            int score = int.Parse(data);
            client.SetScore(score);
            Room room = client.Room;
            if (room == null) return null;
            room.TakeGem(score, client);
            return null;
        }
    }
}
