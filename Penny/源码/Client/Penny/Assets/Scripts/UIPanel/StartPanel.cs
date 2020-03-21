using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartPanel : BasePanel {
    private Button loginButton;
    private Animator btnAnimator;
    private void Start()
    {
        loginButton = transform.Find("LoginButton").GetComponent<Button>();
        btnAnimator = loginButton.GetComponent<Animator>();
        loginButton.onClick.AddListener(OnLoginClick);
    }
    public override void OnEnter()
    {
        base.OnEnter();

    }
    private void OnLoginClick()
    {
        PlayButtonSound();
        btnAnimator.enabled = false;
        loginButton.transform.DOScale(0, 0.5f).OnComplete(() => loginButton.gameObject.SetActive(false));
        uiMag.PushPanel(UIPanelType.Login);
    }
    public override void OnPause()
    {
        base.OnPause();
        btnAnimator.enabled = false;
        loginButton.transform.DOScale(0, 0.5f).OnComplete(() => loginButton.gameObject.SetActive(false));
    }
    public override void OnResume()
    {
        base.OnResume();
        loginButton.gameObject.SetActive(true);
        loginButton.transform.DOScale(1, 0.5f).OnComplete(() => btnAnimator.enabled = true);
    }
}
