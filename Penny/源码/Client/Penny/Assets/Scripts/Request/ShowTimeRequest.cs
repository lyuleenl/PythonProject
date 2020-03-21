using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class ShowTimeRequest : BaseRequest {
    private GamePanel gamePanel;
    public int time;
    public override void Awake()
    {
        Debug.Log("-------SHOWTIME---- -");
        actionCode = ActionCode.ShowTime;
        gamePanel = GetComponent<GamePanel>();
        base.Awake();
    }
    public override void OnResponse(string data)
    {
        time = int.Parse(data);
        gamePanel.ShowTimeSync(time);
    }
}
