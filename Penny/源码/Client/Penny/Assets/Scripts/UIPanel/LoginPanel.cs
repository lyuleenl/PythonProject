using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common;

public class LoginPanel:BasePanel {
    private InputField usernameIF;
    private InputField passwordIF;
    private LoginRequest loginRequest;
    //private Text userInput;
    //private Text passInput;
    private void Start()
    {
        usernameIF = transform.Find("UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordInput").GetComponent<InputField>();
        loginRequest = GetComponent<LoginRequest>();

        transform.Find("BackButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);

        transform.Find("LoginButtonPanel").GetComponent<Button>().onClick.AddListener(OnLoginClick);
        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterClick);
    }
    public override void OnEnter()
    {
        base.OnEnter();
        EnterAnimation();
    }
    public override void OnPause()
    {
        HideAnimation();
    }
    public override void OnResume()
    {
        EnterAnimation();
    }
    public void OnCloseClick()
    {
        PlayButtonSound();
        uiMag.PopPanel();

    }
    public void OnLoginClick()
    {
        PlayButtonSound();
        string msg = "";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }else if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空";
        }
        if (msg != "")
        {
            uiMag.ShowMessage(msg);
        }
        loginRequest.SendRequest(usernameIF.text, passwordIF.text);//  发送到服务器端
    }
    public void OnLoginRequest(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            uiMag.ShowMessageSync("登陆成功");
            uiMag.PushPanelSync(UIPanelType.RoomList);      
        }
        else
        {
            uiMag.ShowMessageSync("登陆失败，请检查用户名和密码");
        }
    }
    public void OnRegisterClick()
    {
        PlayButtonSound();
        uiMag.PushPanel(UIPanelType.Register);
    }
    public override void OnExit()
    {
        base.OnExit();
        
        HideAnimation();
    }
    private void EnterAnimation()
    {
            gameObject.SetActive(true);
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 0.5f);
            transform.localPosition = new Vector3(0, 1000, 0);
            transform.DOLocalJump(Vector3.zero, 5, 3, 1);

    }
    private void HideAnimation()
    {
        transform.DOScale(0, 0.5f);
        Tween tween = transform.DOLocalJump(new Vector3(0, 1000, 0), 5, 3, 1);
        tween.OnComplete(() => gameObject.SetActive(false));
    }
}
