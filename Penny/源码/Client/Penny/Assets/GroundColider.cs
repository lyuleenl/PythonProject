using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundColider : MonoBehaviour {
    private BoxCollider2D boxCollider2D;
    private PlayerController playerController;
    private bool isOn;
    float m_time = 0;
    private Score score;
    void Start () {
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerController = GameObject.Find("PennyOwner").GetComponent<PlayerController>();
        score = GameObject.Find("Score").GetComponent<Score>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isOn)
        {
            m_time += Time.deltaTime;
            if (m_time > 1)
            {
                playerController.jumpTakeOffSpeed = 7;
                isOn = false;
                m_time = 0;
            }
        }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerController.jumpTakeOffSpeed = 10;
        score.count = 0;
        isOn = true;
    }
}
