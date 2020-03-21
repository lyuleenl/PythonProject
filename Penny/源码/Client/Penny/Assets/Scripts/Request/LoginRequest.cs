using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Text;

public class LoginRequest : BaseRequest {
    private LoginPanel loginPanel;
   // private StringBuilder sb = new StringBuilder();
    // Use this for initialization
    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        loginPanel = GetComponent<LoginPanel>();
        base.Awake();
    }
    void Start () {

	}
    public void SendRequest(string username,string password)
    {
        // sb.Append(username).Append(",").Append(password);
        //Debug.Log(sb+"-------LoginRequest------");
        string data = username + "," + password;
        Debug.Log(data + "-------LoginRequest------");
        base.SendRequest(data);
    }
    public override void OnResponse(string data)
    {
        string[] strs = data.Split(',');
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        loginPanel.OnLoginRequest(returnCode);
        if (returnCode == ReturnCode.Success)
        {
            string userName = strs[1];
            int totalCount = int.Parse(strs[2]);
            int winCount = int.Parse(strs[3]);
            UserData ud = new UserData(userName, totalCount, winCount);
            gameFacade.SetUserData(ud);
        }
    }
}
