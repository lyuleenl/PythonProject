using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehaviour : MonoBehaviour
{
	[Header("References")]
	public GameObject gemVisuals;
	public GameObject collectedParticleSystem;
	public CircleCollider2D gemCollider2D;

    
	private float durationOfCollectedParticleSystem;
    private Score score;
	void Start()
	{
		durationOfCollectedParticleSystem = collectedParticleSystem.GetComponent<ParticleSystem>().main.duration;
        score = GameObject.Find("Score").GetComponent<Score>();
	}

	void OnTriggerEnter2D(Collider2D theCollider)
	{
		if (theCollider.CompareTag ("Player")) {
            score.AddScore();
            GemCollected();
           // GameFacade.Instance.SendScore(1);
			
		}
	}

	void GemCollected()
	{
		gemCollider2D.enabled = false;
		gemVisuals.SetActive (false);
		collectedParticleSystem.SetActive (true);
		Invoke ("DeactivateGemGameObject", durationOfCollectedParticleSystem);

	}

	void DeactivateGemGameObject()
	{
		gameObject.SetActive (false);
	}
}
