using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using GameServer.Controller;
using Common;

namespace GameServer.Servers
{
    class Server
    {
        private IPEndPoint ipEndPoint;
        private Socket serverSocket;
        private List<Client> clistList=new List<Client>();
        private List<Room> roomList = new List<Room>();
        private ControllerManager controllerManager;
        public Server()
        {

        }
        public Server(string ipStr,int port)
        {
            controllerManager = new ControllerManager(this);
            SetIpEndPoint(ipStr, port);
        }
        public void  SetIpEndPoint(string ipStr,int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
        }
        public void Start()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(0);
            Console.WriteLine("--------------------");
            Console.WriteLine("---服务器正在监听端口---");
            Console.WriteLine("--------------------");
            serverSocket.BeginAccept(AcceptCallBack,null);
        }
        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket clientSocket = serverSocket.EndAccept(ar);
            Client client = new Client(clientSocket,this);   //这里的this代表Server
            client.Start();
            clistList.Add(client);
            serverSocket.BeginAccept(AcceptCallBack, null);
        }
        public void RemoveClient(Client client)
        {
            lock (clistList)//lock的作用是 当这段代码执行的时候 具有同样参数的线程无法访问
            {
                clistList.Remove(client);
            }
            
        }
        public void SendResponse(Client client,ActionCode actionCode,string data)
        {
            client.Send(actionCode, data);
            Console.WriteLine("-----服务器发送响应-----");
        }
        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            controllerManager.HandleRequest(requestCode, actionCode, data, client);
        }
        public void CreatRoom(Client client)
        {
            Room room = new Room(this);
            room.AddClient(client);
            roomList.Add(room);
        }
        public void RemoveRoom(Room room)
        {
            if (roomList != null && room != null)
            {
                roomList.Remove(room);
            }
        }
        public List<Room> GetRoomList()
        {
            return roomList;    
        }
        public Room GetRoomById(int Id)
        {
            foreach (Room  room in roomList)
            {
                if (room.GeOwnerId() == Id) return room;

            }
            return null;    
        }
    }
}
