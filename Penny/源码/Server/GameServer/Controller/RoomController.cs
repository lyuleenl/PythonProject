using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;
namespace GameServer.Controller
{
    class RoomController:BaseController
    {
        public RoomController()
        {
            requestCode = RequestCode.Room; 
        }
        public string CreatRoom(string data,Client client,Server server)
        {
            server.CreatRoom(client);
            return ((int)ReturnCode.Success).ToString() + "," + ((int)RoleType.Owner).ToString();
        }
        public string RoomList(string data, Client client, Server server)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Room  room in server.GetRoomList())
            {
                if (room.IsRoomWaitingjoin())
                {
                    sb.Append(room.GetRoomOwnerData()+".");
                }
                
            }
            if (sb.Length == 0)
            {
                sb.Append("0");
            }
            else
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
        public string JoinRoom(string data, Client client, Server server)
        {

            int id = int.Parse(data);
            Room room = server.GetRoomById(id);
            if (room == null)
            {
                return ((int)ReturnCode.NotFound).ToString();
            }
            else if(room.IsRoomWaitingjoin())
            {
                Console.WriteLine("------JoinRoom----");
                room.AddClient(client);
                string roomData = room.GetAllData();
                room.BroadcastMessage(client, ActionCode.UpdateRoom, roomData);
                return ((int)ReturnCode.Success).ToString()+"!" + ((int)RoleType.Rival).ToString()+ "|" + roomData;//ReturnCode|ud.id, ud.name,ud.totalcount,ud.wincount_ud.id,ud.name,ud.totalcount,ud.wincount
            }else if (room.IsRoomWaitingjoin() == false)
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            return null;
        }
        public string QuitRoom(string data,Client client,Server server)
        {
            bool isOwner = client.IsOwner();
            Room room = client.Room;
            if (isOwner)
            {
                room.BroadcastMessage(client, ActionCode.QuitRoom, ((int)ReturnCode.Success).ToString());
                room.CloseRoom();
                return ((int)ReturnCode.Success).ToString();
            }
            else
            {
                
                client.Room.RemoveClient(client);
                
                room.BroadcastMessage(client, ActionCode.UpdateRoom, room.GetRoomOwnerData());
                return ((int)ReturnCode.Success).ToString();
            }
        }

    }
}
