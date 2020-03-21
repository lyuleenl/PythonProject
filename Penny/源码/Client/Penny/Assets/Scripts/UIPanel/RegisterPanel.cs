using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;

public class RegisterPanel : BasePanel {
    private InputField usernameIF;
    private InputField passwordIF;
    private InputField rePasswordIF;
    private RegisterRequest registerRequest;
    private void Start()
    {
        registerRequest= GetComponent<RegisterRequest>();
        usernameIF = transform.Find("UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordInput").GetComponent<InputField>();
        rePasswordIF = transform.Find("RePasswordInput").GetComponent<InputField>();
        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnClickRegister);
        transform.Find("BackButton").GetComponent<Button>().onClick.AddListener(OnClickBack);
    }
    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f);
        transform.localPosition = new Vector3(0, 1000, 0);
        transform.DOLocalJump(Vector3.zero, 5, 3, 1);
    }
    private void OnClickRegister()
    {
        PlayButtonSound();
        string msg = "";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg = "用户名不能为空";
        }else if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg = "密码不能为空";
        }else if (rePasswordIF.text != passwordIF.text)
        {
            msg = "两次输入的密码不一致";
        }
        if (msg != "")
        {
            uiMag.ShowMessage(msg);return;
        }
        Debug.Log("----向服务端发送注册信息-----");
        registerRequest.SendRequest(usernameIF.text, passwordIF.text);        // 向服务端发送注册信息
    }
    public void OnRegisterRequest(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            uiMag.ShowMessageSync("注册成功");
        }else
        {
            uiMag.ShowMessageSync("该用户名已注册");
        }
    }
    private void OnClickBack()
    {
        PlayButtonSound();
        transform.DOScale(0, 0.5f);
        Tween tween = transform.DOLocalJump(new Vector3(0, 1000, 0), 5, 3, 1);
        tween.OnComplete(() => uiMag.PopPanel());

    }
    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }

}
