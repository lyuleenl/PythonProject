using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    private PlayerController playerController;
    // Use this for initialization
    void Start () {
        playerController = GameObject.Find("PennyOwner").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
            playerController.jumpTakeOffSpeed = 10;
            Debug.Log("yyyyyyyyyyyyyyyyyyy");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.jumpTakeOffSpeed = 7;
            Debug.Log("nnnnnnnnnnnnn");
        }
    }
}
