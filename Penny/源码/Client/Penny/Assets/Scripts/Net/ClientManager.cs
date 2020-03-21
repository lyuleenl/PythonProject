using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using Common;

/// <summary>
/// 用来管理跟服务器端Socket连接
/// </summary>
public class ClientManager :BaseManager {
    private const string IP = "127.0.0.1";//云服务器公网IP
    private const int PORT = 6699;
    private Socket clientSocket;
    private Message msg=new Message();
    public ClientManager(GameFacade facade) : base(facade) { }
    public override void OnInit()
    {
        base.OnInit();
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            clientSocket.Connect(IP, PORT);
            Start();
        }
        catch (Exception e)
        {

            Debug.LogWarning("无法连接到服务端，请检查网络配置" + e);
        }
        
    }
    private void Start()
    {
        
        clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize,SocketFlags.None,ReceiveCallBack,null);
    }
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            if (clientSocket == null || clientSocket.Connected == false) return;
            int count =clientSocket.EndReceive(ar);
            msg.ReadMessage(count, OnProcessDataCallBack);
            Start();
        }
        catch (Exception e)
        {

            Debug.LogWarning("结束接收出现异常" + e);
        }

    }
    private void OnProcessDataCallBack(ActionCode actionCode,string data)
    {
        facade.HandleResponse(actionCode, data);
    }
    public void SendRequest(RequestCode requestCode,ActionCode actionCode,string data)
    {
        Debug.Log("wwww" + data + "-----ClientManager----");
        byte[] bytes = Message.PackData(requestCode, actionCode, data);
        clientSocket.Send(bytes);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            clientSocket.Close();
        }
        catch (Exception e)
        {
             
            Debug.LogWarning("无法关闭与服务器端的连接"+e);
        }
    }
}
