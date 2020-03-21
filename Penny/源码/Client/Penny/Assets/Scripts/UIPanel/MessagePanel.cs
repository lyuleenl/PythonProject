using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel {
    private Text text;
    private float showTime=1;
    private string message;
    private void Update()
    {
        if (message != null)
        {
            ShowMessage(message);
            message = null;
        }
    }
    public override void OnEnter()//当面板被实例化出来之后调用
    {
        base.OnEnter();
        text = GetComponent<Text>();
        text.enabled = false;
        uiMag.InjectMsgPanel(this);
    }
    public void ShowMessageSync(string msg)
    {
        message = msg;
    }
    public void ShowMessage(string msg)
    {
        text.CrossFadeAlpha(1,0.02f , false);
        text.text = msg;
        text.enabled = true;
        Invoke("Hide", showTime);
    }
    private void Hide()
    {
        text.CrossFadeAlpha(0, 1, false);
    }
}
