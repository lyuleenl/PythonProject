using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class PlayRequest : BaseRequest {
    private bool isStartPlaying = false;
    public override void Awake()
    {
        actionCode = ActionCode.Play;
        base.Awake();
    }

    private void Update()
    {
        if (isStartPlaying)
        {
            gameFacade.Playing();
            isStartPlaying = false;
        }
    }

    public override void OnResponse(string data)
    {
        isStartPlaying = true;
    }
}
