using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using System.Text;
using System.Linq;

public class Message : MonoBehaviour {
    private byte[] data = new byte[1024];
    private int startIndex = 0;
    //在数组中存取多少字节
                               //public void AddCount(int count)
                               //{
                               //    startIndex += count;
                               //}
    public byte[] Data
    {
        get { return data; }    
    }
    public int StartIndex
    {
        get { return startIndex; }
    }
    public int RemainSize
    {
        get { return data.Length - startIndex; }
    }
    public void ReadMessage(int newDataAmount, Action<ActionCode, string> processDataCallBack)//Action<回掉函数的参数>回调函数名
    {
        startIndex += newDataAmount;
        while (true)
        {
            if (startIndex <= 4) return;
            int count = BitConverter.ToInt32(data, 0);
            if ((startIndex - 4) >= count)
            {
                //string s = Encoding.UTF8.GetString(data, 4, count);
                //Console.WriteLine("解析出一条数据：" + s);
               ActionCode  actionCode = (ActionCode)BitConverter.ToInt32(data, 4);//ToInt32只解析4个数据
                string s = Encoding.UTF8.GetString(data, 8, count -4);//数据长度4 requestCode 4   从第8个数据开始解析，解析count-4个
                processDataCallBack(actionCode, s);
                Array.Copy(data, count + 4, data, 0, startIndex - count - 4);//覆盖数据
                startIndex -= (count + 4);
            }
            else
            {
                break;
            }
        }
    }
    //public static byte[] PackData(ActionCode actionCode, string data)
    //{
    //    byte[] requestCodeBytes = BitConverter.GetBytes((int)actionCode);
    //    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
    //    int dataAmount = requestCodeBytes.Length + dataBytes.Length;
    //    byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
    //    byte[] newByte = dataAmountBytes.Concat(requestCodeBytes).ToArray();
    //    return newByte.Concat(dataBytes).ToArray();
    //}
    public static byte[] PackData(RequestCode requestCode,ActionCode actionCode, string data)
    {
        byte[] requestCodeBytes = BitConverter.GetBytes((int)requestCode);
        byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        int dataAmount = requestCodeBytes.Length + dataBytes.Length+actionCodeBytes.Length;
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
        return dataAmountBytes.Concat(requestCodeBytes).ToArray().Concat(actionCodeBytes).ToArray().Concat(dataBytes).ToArray();

    }
}
