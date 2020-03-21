using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour {
    public Text roomOwner;
    public Text ownerGames;
    public Text ownerWinnings;
    public Button joinRoom;
    public Image roomImage;

    private int id;
    private RoomListPanel listPanel;
    private void OnEnable()
    {
        roomImage.color = RoomColorRandom();
    }
    private void Start()
    {
       
        if (joinRoom != null)
        {
            joinRoom.onClick.AddListener(OnJoinClick);
        }
    }
    private void OnJoinClick()
    {
        listPanel.OnJoinClick(id);
    }
    public void SetRoomInfor(int id,string userName,string totalCount,string winCount,RoomListPanel listPanel)
    {
        this.id = id;
        this.roomOwner.text = userName;
        this.ownerGames.text= "总场次："+totalCount;
        this.ownerWinnings.text = "胜场："+winCount;
        this.listPanel = listPanel;
    }
    public void SetRoomInfor(int id,string userName, int totalCount, int winCount,RoomListPanel listPanel)
    {
        this.id = id;
        this.roomOwner.text = userName;
        this.ownerGames.text = "总场次：" + totalCount;
        this.ownerWinnings.text = "胜场：" + winCount;
        this.listPanel = listPanel;
    }
    private Color RoomColorRandom()
    {
        Color[] color = new Color[7];
        color[0] = new Color(78/255f,120 / 255f, 82 / 255f);
        color[1] = new Color(100 / 255f, 71 / 255f, 73 / 255f);
        color[2] = new Color(138 / 255f, 167 / 255f, 145 / 255f);
        color[3] = new Color(218 / 255f, 151 / 255f, 100 / 255f);
        color[4] = new Color(111 / 255f, 205 / 255f, 50 / 255f);
        color[5] = new Color(227 / 255f, 93 / 255f, 235 / 255f);
        color[6] = new Color(135 / 255f, 202 / 255f, 203 / 255f);
        int colorCount= Random.Range(0, 7);
        return color[colorCount];
    } 
    public void ChangeColor()//TODO
    {
        GameObject.Destroy(this.gameObject);
    }
}
