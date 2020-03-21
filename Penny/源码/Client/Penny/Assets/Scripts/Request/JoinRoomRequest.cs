using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class JoinRoomRequest : BaseRequest {
    private RoomListPanel roomListPanel;
    public override void Awake()
    {
        roomListPanel = GetComponent<RoomListPanel>();
        requestCode = RequestCode.Room;
        actionCode = ActionCode.JoinRoom;
        base.Awake();
    }
    public void SendRequest(int id)
    {
        Debug.Log("--请求加入的房间ID-----" + id + "-------");
        base.SendRequest(id.ToString());
    }
    public override void OnResponse(string data)
    {

        string[] strs = data.Split('|');
        string[] codeStrs = strs[0].Split('!');
        ReturnCode returnCode = (ReturnCode)int.Parse(codeStrs[0]);

        UserData ud1 = null;
        UserData ud2 = null;
        if (returnCode == ReturnCode.Success)
        {
            string[] udStrs=strs[1].Split('_');
            ud1 = new UserData(udStrs[0]);
            ud2 = new UserData(udStrs[1]);
            RoleType roleType = (RoleType)int.Parse(codeStrs[1]);
            gameFacade.SetOwnerRoleType(roleType);
        }
        roomListPanel.JoinResponse(returnCode, ud1, ud2);
    }
}
