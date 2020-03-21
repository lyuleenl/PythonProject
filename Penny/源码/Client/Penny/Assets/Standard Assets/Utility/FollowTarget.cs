using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0f, 1f, -8f);


        private void LateUpdate()
        {
            transform.position = target.position + offset;
        }
    }
}
