using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class BaseRequest : MonoBehaviour {
    protected RequestCode requestCode=RequestCode.None;
    protected ActionCode actionCode = ActionCode.None;
    protected GameFacade gameFacade;
	// Use this for initialization
	public virtual void Awake () {
        Debug.Log("-------" + this + "-----");
        GameFacade.Instance.AddRequest(actionCode, this);
        gameFacade = GameFacade.Instance;
	}
    protected void SendRequest(string data)
    {
        Debug.Log("-------所发送的请求------"+data + "-------所发送的请求------");
        gameFacade.SendRequest(requestCode, actionCode, data);
    }
    public virtual void SendRequest() { }
    public virtual void OnResponse(string data) {  }
    public virtual void OnDestroy()
    {
       // GameFacade.Instance.RemoveRequest(actionCode);
    }
}
