using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;
public class GamePanel : BasePanel {
    private Text timer;
    private Text back;
    public int time = -1;
    private Button successBtn;
    private Button failBtn;
    private QuitBattleRequest quitBattleRequest;
    private GetScoreRequest getScoreRequest;
    private GameTime gameTime;

    Button tempBtn = null;

    private Score score;
    private PlayerInfor playerInfor;
    private RoomPanel roomPanel;

    private GroundColider groundColider;
    public override void OnEnter()
    {
        //gameObject.SetActive(true);
        GameObject.Find("Grid").gameObject.SetActive(true);
        //GameObject.Find("PennyPixel").gameObject.SetActive(true);
        //backGround.SetActive(true);
        //grid.SetActive(true);
        score = GameObject.Find("Score").GetComponent<Score>();
        base.OnEnter();
    }
    private void Start()
    {
        timer=transform.Find("Time").GetComponent<Text>();
        timer.gameObject.SetActive(false);
        back = transform.Find("Back").GetComponent<Text>();
        back.gameObject.SetActive(false);


        successBtn = transform.Find("SuccessButton").GetComponent<Button>();
        successBtn.onClick.AddListener(OnResultClick);
        successBtn.gameObject.SetActive(false);
        failBtn = transform.Find("FailButton").GetComponent<Button>();
        failBtn.onClick.AddListener(OnResultClick);
        failBtn.gameObject.SetActive(false);
        //GameObject.Find("Grid").gameObject.SetActive(true);
        quitBattleRequest = GetComponent<QuitBattleRequest>();
        getScoreRequest = GetComponent<GetScoreRequest>();
    }
    private void Update()
    {
        if (time > -1)
        {
            ShowTime(time);
            time = -1;
        }
    }

    public void ShowTimeSync(int time)
    {
        this.time = time;
        //getScoreRequest.SendRequest();
    }
    public void ShowTime(int time)
    {
        if (time == 1 || score.count==50)
        {
            //Debug.Log(score.count);
            timer.gameObject.SetActive(false);
            getScoreRequest.SendRequest();
        }        
        timer.gameObject.SetActive(true);
        timer.text = time.ToString();
        timer.transform.localScale = Vector3.one;
        Color tempColor = timer.color;
        tempColor.a = 1;
        timer.color = tempColor;
        timer.transform.DOScale(2, 0.3f).SetDelay(0.3f);
        timer.DOFade(0, 0.3f).SetDelay(0.3f).OnComplete(() => timer.gameObject.SetActive(false));
        facade.PlayNormalAudio(AudioManager.Sound_Alert);
    }
    public void OnGameOverResponse(ReturnCode returnCode)
    {
        tempBtn = null;

        switch (returnCode)
        {
            case ReturnCode.Success:
                tempBtn = successBtn;
                break;
            case ReturnCode.Fail:
                tempBtn = failBtn;
                break;
        }
        tempBtn.gameObject.SetActive(true);
        tempBtn.transform.localScale = Vector3.zero;
        tempBtn.transform.DOScale(1, 0.5f);
    }

    private void OnResultClick()
    {
        tempBtn.gameObject.SetActive(false);
        uiMag.PopPanel();
        uiMag.PopPanel();
        GameFacade.Instance.ReLoad();
        facade.GameOver();
    }

    private void OnExitClick()
    {
        quitBattleRequest.SendRequest();
    }
    public void OnExitResponse()
    {
        OnResultClick();
    }
}
