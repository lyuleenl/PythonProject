using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class UpdateRoomRequest : BaseRequest {
    private RoomPanel roomPanel;
    public override void Awake()
    {
        roomPanel = GetComponent<RoomPanel>();
        requestCode = RequestCode.Room;
        actionCode = ActionCode.UpdateRoom;
        base.Awake();
    }
    public override void SendRequest()
    {
        base.SendRequest("UpdateRoom");
    }
    public override void OnResponse(string data)
    {
        UserData ud1 = null;
        UserData ud2 = null;
            string[] udStrs = data.Split('_');
            ud1 = new UserData(udStrs[0]);
        if(udStrs.Length>1)
            ud2 = new UserData(udStrs[1]);
        roomPanel.SetAllInforSync(ud1, ud2);
    }
}
