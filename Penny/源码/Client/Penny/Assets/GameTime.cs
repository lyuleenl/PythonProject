using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameTime : MonoBehaviour {
    private Text gameTime;
    private int timer = 30;
    private float totalTime = 0;
	// Use this for initialization
	void Start () {
        gameTime = GetComponent<Text>();
	}
	private void CountTime()
    {
        totalTime += Time.deltaTime;
        if (totalTime > 1)
        {
            if(timer>0)
                timer--;
            else
            {

            }
            gameTime.text = "倒计时：" + timer;
            totalTime = 0;
        }
    }
	// Update is called once per frame
	void Update () {
        CountTime();
	}
}
