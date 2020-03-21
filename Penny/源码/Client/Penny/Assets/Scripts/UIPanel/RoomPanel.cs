using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;
public class RoomPanel : BasePanel {
    private Text playerBlueName;
    private Text playerBlueGames;
    private Text playerBlueWinnings;

    private Text playerRedName;
    private Text playerRedGames;
    private Text playerRedWinnings;

    private Transform bluePlayer;
    private Transform redPlayer;
    private Transform start;
    private Transform back;

    //private CreatRoomRequest creatRoomReq;
    private UserData ud;
    private UserData ud1;
    private UserData ud2;
    private bool isPop=false;

    private QuitRoomRequest quitRoomRequest;
    private StartGameRequest startGameRequest;

    public GameObject roomPanelObj;

    private void Start()
    {
        playerBlueName = transform.Find("BluePlayer/PlayerName").GetComponent<Text>();
        playerBlueGames = transform.Find("BluePlayer/Games").GetComponent<Text>();
        playerBlueWinnings = transform.Find("BluePlayer/Winnings").GetComponent<Text>();
        playerRedName = transform.Find("RedPlayer/PlayerName").GetComponent<Text>();
        playerRedGames = transform.Find("RedPlayer/Games").GetComponent<Text>();
        playerRedWinnings = transform.Find("RedPlayer/Winnings").GetComponent<Text>();
        quitRoomRequest = GetComponent<QuitRoomRequest>();
        startGameRequest = GetComponent<StartGameRequest>();
        //creatRoomReq = GetComponent<CreatRoomRequest>();
        //roomPanelObj = GetComponent<Transform>();

        bluePlayer = transform.Find("BluePlayer").GetComponent<Transform>();
        redPlayer = transform.Find("RedPlayer").GetComponent<Transform>();
        start = transform.Find("Start").GetComponent<Transform>();
        back = transform.Find("Back").GetComponent<Transform>();

        transform.Find("Start").GetComponent<Button>().onClick.AddListener(OnClickStart);
        transform.Find("Back").GetComponent<Button>().onClick.AddListener(OnClickBack);
        EnterAnimation();
    }
    private void Update()
    {
        if (ud != null)
        {
            SetBlueInfor(ud.UserName, ud.TotalCount, ud.WinCount);
            ud = null;
            ClearRedInfor();
        }
        if (ud1 != null)
        {
            SetBlueInfor(ud1.UserName, ud1.TotalCount, ud1.WinCount);
            if (ud2 != null)
                SetRedInfor(ud2.UserName, ud2.TotalCount, ud2.WinCount);
            else
                ClearRedInfor();
            ud1 = null;ud2 = null;
        }
        if (isPop)
        {
            uiMag.PopPanel();
            isPop = false;
        }
    }
    public void SetAllInforSync(UserData ud1,UserData ud2)
    {
        this.ud1 = ud1;
        this.ud2 = ud2;
    }
    public void SetInforSync()
    {
      ud = facade.GetUserData();
    }
    public void SetBlueInfor(string userName,int totalCount,int winCount)
    {
        this.playerBlueName.text = userName;
        this.playerBlueGames.text = "总场数："+totalCount;
        this.playerBlueWinnings.text = "胜场："+winCount;

    }
    public void ClearRedInfor()
    {
        this.playerRedName.text = "";
        this.playerRedGames.text = "等待玩家加入";
        this.playerRedWinnings.text = "";
    }
    public void SetRedInfor(string userName, int totalCount, int winCount)
    {
        this.playerRedName.text =userName;
        this.playerRedGames.text = "总场数："+totalCount;
        this.playerRedWinnings.text = "胜场："+winCount;
    }
    public override void OnEnter()
    {
        //if(creatRoomReq==null)
        //    creatRoomReq = GetComponent<CreatRoomRequest>();
        //creatRoomReq.SendRequest();
        if (bluePlayer!=null)
        EnterAnimation();
    }
    public override void OnExit()
    {
        ExitAnimation();
    }
    public override void OnPause()
    {
       ExitAnimation();
    }
    public override void OnResume()
    {
        EnterAnimation();
    }
    private void OnClickStart()
    {
        startGameRequest.SendRequest();
    }
    public void OnStartResponse(ReturnCode returnCode)
    {
        if (playerRedName.text != "")
        {
            if (returnCode == ReturnCode.Fail)
            {
                uiMag.ShowMessageSync("只有房主才可以开始游戏");
            }
            else
            {
                uiMag.PushPanelSync(UIPanelType.Game);
                facade.StartGameSync();
            }
        }
        else
            uiMag.ShowMessageSync("请先等待玩家加入");
    }
    private void OnClickBack()
    {
        PlayButtonSound();
        quitRoomRequest.SendRequest();
    }
    public void OnBackResponse()
    {
        // uiMag.PopPanel();
        isPop = true;
    }
    private void EnterAnimation()
    {
        gameObject.SetActive(true); 
        bluePlayer.localPosition = new Vector3(-1000, 0, 0);
        bluePlayer.DOLocalMoveX(-96, 0.5f);

        redPlayer.localPosition = new Vector3(1000, 0, 0);
        redPlayer.DOLocalMoveX(97, 0.5f);

        start.localScale = Vector3.zero;
        start.DOScale(1, 0.5f);
        back.localScale = Vector3.zero;
        back.DOScale(1, 0.5f);
    }
    private void ExitAnimation()
    {
        bluePlayer.DOLocalMoveX(-1000, 0.5f);

        redPlayer.DOLocalMoveX(1000, 0.5f);

        start.DOScale(0, 0.5f);

        back.DOScale(0, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }

}
