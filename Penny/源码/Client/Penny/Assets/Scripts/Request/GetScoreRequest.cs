using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GetScoreRequest : BaseRequest {
    private Score score;
    public override void Awake()
    {
        score = GameObject.Find("Score").GetComponent<Score>();
        requestCode = RequestCode.Game;
        actionCode = ActionCode.GetScore;
        base.Awake();
    }
    public override void SendRequest()
    {
            Debug.Log("~~~~~~~" + score+ "~~~~~~~~");
            base.SendRequest(score.count.ToString());
    }
}
