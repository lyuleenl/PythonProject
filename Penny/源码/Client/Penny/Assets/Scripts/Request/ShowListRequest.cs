using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class ShowListRequest : BaseRequest {
    private RoomListPanel roomList;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.RoomList;
        roomList = GetComponent<RoomListPanel>();
        base.Awake();
    }
    public override void SendRequest()
    {
        base.SendRequest("---ShowListRoom---");
    }
    public override void OnResponse(string data)
    {
        List<UserData> userList = new List<UserData>();
        if (data != "0")
        {
            string[] userDatas = data.Split('.');
            foreach (string ud in userDatas)
            {
                string[] strs = ud.Split(',');
                userList.Add(new UserData(int.Parse(strs[0]), strs[1], int.Parse(strs[2]), int.Parse(strs[3])));
            }
        }
        roomList.LoadRoomItemSync(userList);
    }
}
