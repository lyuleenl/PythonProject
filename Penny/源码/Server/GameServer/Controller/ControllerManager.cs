using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Reflection;
using GameServer.Servers;

namespace GameServer.Controller
{
    class ControllerManager
    {
        private Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();
        private Server server;
        public ControllerManager(Server server)
        {
            this.server = server;
            InitController();
        }
        void InitController()
        {
            DefaultController defaultController = new DefaultController();
            controllerDict.Add(defaultController.RequestCode, defaultController);
            controllerDict.Add(RequestCode.User, new UserController());
            controllerDict.Add(RequestCode.Room, new RoomController());
            controllerDict.Add(RequestCode.Game, new GameController());
        }
        public void HandleRequest(RequestCode requestCode,ActionCode actionCode,string data,Client client)
        {
            Console.WriteLine(data+"-----接收到的请求----");
            BaseController controller;
            bool isGet=controllerDict.TryGetValue(requestCode,out controller);  //返回bool值  表示是否获取成功
            if (isGet == false)
            {
                Console.WriteLine("异常：无法得到["+requestCode+"]所对应的controller,请求异常");return;    
            }
            string methodName = Enum.GetName(typeof(ActionCode), actionCode);//将枚举名转换为字符串
            MethodInfo mi=controller.GetType().GetMethod(methodName);   //得到方法信息
            if (mi == null)
            {
                Console.WriteLine("[warning]在Controller["+controller.GetType()+"]中没有对应的处理方法：["+methodName+"]");return;
            }
            object[] parameters = new object[] { data,client,server};
            object o= mi.Invoke(controller,parameters);
            if(o==null||string.IsNullOrEmpty(o as string)) { return; }
            server.SendResponse(client, actionCode, o as string);
        }
    }
}
