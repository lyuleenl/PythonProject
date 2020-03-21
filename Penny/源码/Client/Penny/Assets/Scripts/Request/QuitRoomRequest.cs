using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class QuitRoomRequest : BaseRequest {
    private RoomPanel roomPanel;
    public override void Awake()
    {
        roomPanel = GetComponent<RoomPanel>();
        requestCode = RequestCode.Room;
        actionCode = ActionCode.QuitRoom;
        base.Awake();
    }
    public override void SendRequest()
    {
        base.SendRequest("QuitRoom");
    }
    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        if (returnCode == ReturnCode.Success)
        {
            roomPanel.OnBackResponse();
        }
    }
}
