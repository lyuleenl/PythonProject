using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour {
    private Text scoreText;
    public int count=0;
    private GetScoreRequest getScoreRequest;
	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
	}
	public void AddScore()
    {
        count++;
    }
	// Update is called once per frame
	void Update () {
        scoreText.text = "分数：" + count;
	}
}
