using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common;

public class RoomListPanel : BasePanel {
    private RectTransform playerInfor;
    private RectTransform roomList;
    private VerticalLayoutGroup layoutGroup;
    private GameObject roomItemPrefab;
    private ShowListRequest showList;
    private List<UserData> udList = null;
    private CreatRoomRequest creatRoomRequest;
    private JoinRoomRequest joinRoomRequest;

    private UserData ud1 = null;
    private UserData ud2 = null;
    private void Start()
    {
        playerInfor = transform.Find("PlayerInfor").GetComponent<RectTransform>();
        roomList = transform.Find("RoomList").GetComponent<RectTransform>();
        layoutGroup = transform.Find("RoomList/ScrollRect/RoomGroup").GetComponent<VerticalLayoutGroup>();
        roomItemPrefab = Resources.Load("UIPanel/RoomItem") as GameObject;
        transform.Find("RoomList/BackButton").GetComponent<Button>().onClick.AddListener(OnClickClose);
        transform.Find("PlayerInfor/CreatRoomButton").GetComponent<Button>().onClick.AddListener(OnClickCreatRoom);
        transform.Find("RoomList/RefreshButton").GetComponent<Button>().onClick.AddListener(OnClickRefresh);
        showList = GetComponent<ShowListRequest>();
        creatRoomRequest = GetComponent<CreatRoomRequest>();
        joinRoomRequest = GetComponent<JoinRoomRequest>();
        EnterAnimation();
        ShowPlayerInfor();
    }
    private void Update()
    {
        if (udList != null)
        {
            LoadRoomItem(udList);
            udList = null;
        }
        if (ud1 != null && ud2 != null)
        {
            BasePanel panel = uiMag.PushPanel(UIPanelType.Room);
            (panel as RoomPanel).SetAllInforSync(ud1, ud2);
            ud1 = null;ud2 = null;
        }
    }
    public override void OnEnter()
    {
        ShowPlayerInfor();
        if (playerInfor!=null)
        EnterAnimation();
        if(showList==null)
        showList = GetComponent<ShowListRequest>();
        showList.SendRequest();
    }
    public override void OnExit()
    {
        HideAnimation();
    }
    public override void OnPause()
    {
        HideAnimation();
    }
    public override void OnResume()
    {
        EnterAnimation();
        showList.SendRequest();
    }
    private void OnClickClose()
    {
        PlayButtonSound();
        uiMag.PopPanel();
    }
    private void OnClickCreatRoom()
    {
        BasePanel panel=uiMag.PushPanel(UIPanelType.Room);
        creatRoomRequest.SetRoomPanel(panel);
        creatRoomRequest.SendRequest();
    }
    private void OnClickRefresh()
    {
        showList.SendRequest();
    }
    private void EnterAnimation()
    {
        gameObject.SetActive(true);

        playerInfor.localPosition = new Vector3(-1000, 0,0);
        playerInfor.DOLocalMoveX(-201, 0.5f);

        roomList.localPosition = new Vector3(1000, 0,0);
        roomList.DOLocalMoveX(81, 0.5f);
    }
    private void HideAnimation()
    {
        playerInfor.DOLocalMoveX(-1000, 0.5f);

        roomList.DOLocalMoveX(1000, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }
    private void ShowPlayerInfor()
    {
        UserData ud = facade.GetUserData();
        transform.Find("PlayerInfor/PlayerName").GetComponent<Text>().text = ud.UserName;
        transform.Find("PlayerInfor/Games").GetComponent<Text>().text = "总场次："+ud.TotalCount.ToString();
        transform.Find("PlayerInfor/Winnings").GetComponent<Text>().text = "胜利次数:" + ud.WinCount.ToString();
    }
    public void LoadRoomItemSync(List<UserData> userList)
    {
        this.udList = userList;
    }
    private void LoadRoomItem(List<UserData> userList)
    {  
        RoomItem[] ri=layoutGroup.GetComponentsInChildren<RoomItem>();
        foreach (RoomItem item in ri)
        {
            item.ChangeColor();
        }

        int count = userList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject roomItem = GameObject.Instantiate(roomItemPrefab);
            roomItem.transform.SetParent(layoutGroup.transform);
            UserData ud = userList[i];
            roomItem.GetComponent<RoomItem>().SetRoomInfor(ud.UserId, ud.UserName, ud.TotalCount, ud.WinCount,this);
        }
        int roomCount = GetComponentsInChildren<RoomItem>().Length;
         Vector2 group=layoutGroup.GetComponent<RectTransform>().sizeDelta;
        layoutGroup.GetComponent<RectTransform>().sizeDelta = new Vector2(group.x, roomCount * (roomItemPrefab.GetComponent<RectTransform>().sizeDelta.y + layoutGroup.spacing));
    }
    public void OnJoinClick(int id)
    {
        joinRoomRequest.SendRequest(id);
    }
    public void JoinResponse(ReturnCode returnCode,UserData ud1,UserData ud2)
    {
        switch (returnCode)
        {
            case ReturnCode.Success:
                this.ud1 = ud1;
                this.ud2 = ud2;
                break;
            case ReturnCode.Fail:
                uiMag.ShowMessageSync("加入房间失败");
                break;
            case ReturnCode.NotFound:
                uiMag.ShowMessageSync("房间不存在");
                break;
        }
    }

    public void OnUpdateResultResponse(int totalCount, int winCount)
    {
        facade.UpdateResult(totalCount, winCount);
        ShowPlayerInfor();
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        LoadRoomItem(1);
    //    }
    //}
}
