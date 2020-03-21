using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityStandardAssets.Utility;
public class CameraManager : BaseManager
{
    private GameObject cameraGo;
    private FollowTarget followTarget;
    private Vector3 originalPosition;
    private Vector3 originalRotation;

    public CameraManager(GameFacade facade) : base(facade) { }

    public override void OnInit()
    {
        cameraGo = Camera.main.gameObject;
        followTarget = cameraGo.GetComponent<FollowTarget>();
    }

    //public override void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        FollowTarget(null);
    //    }
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        WalkthroughScene(); 
    //    }
    //}

    public void FollowRole()
    {
        originalPosition = cameraGo.transform.position;
        originalRotation = cameraGo.transform.eulerAngles;

        Quaternion targetQuaternion = Quaternion.LookRotation(followTarget.target.position - cameraGo.transform.position);
        cameraGo.transform.DORotateQuaternion(targetQuaternion, 1f).OnComplete(delegate ()
        {
            followTarget.enabled = true;
        });
    }
}
