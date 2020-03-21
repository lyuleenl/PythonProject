using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class CreatRoomRequest : BaseRequest {
    private RoomPanel roomPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.CreatRoom;
        roomPanel = GetComponent<RoomPanel>();
        base.Awake();
    }
    public void SetRoomPanel(BasePanel panel)
    {
        roomPanel = panel as RoomPanel;
    }
    public override void SendRequest()
    {
        base.SendRequest("CreatRoom");
    }
    public override void OnResponse(string data)
    {
        string[] strs = data.Split(',');
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        RoleType roleType = (RoleType)int.Parse(strs[1]);
        gameFacade.SetOwnerRoleType(roleType);
        if (returnCode == ReturnCode.Success)
        {
            roomPanel.SetInforSync();
        }
    }
}
