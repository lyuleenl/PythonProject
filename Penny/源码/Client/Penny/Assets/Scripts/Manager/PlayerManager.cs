using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityStandardAssets.Utility;

public class PlayerManager : BaseManager {
    public PlayerManager(GameFacade facade) : base(facade) { }
    private  UserData userData;
   // private Dictionary<RoleType, RoleData> roleDic = new Dictionary<RoleType, RoleData>();
    private Vector2 rolePos;
    //private RoleType ownerRoleType;
    private GameObject ownerGO;
    //private GameObject playerSyncRequest;
    private GameObject riverGO;
    private RoleData roleData;
    //private SmoothFollow smooth;
    //public void SetOwnerRoleType(RoleType roleType)
    //{
    //    //this.ownerRoleType = roleType;
    //}
    private GetScoreRequest getScoreRequest;

    private GameObject roleProfab;
    public UserData UserData
    {
        set { userData = value; }
        get { return userData; }
    }
    public override void OnInit()
    {
        roleProfab= Resources.Load("Role/PennyOwner") as GameObject;
        rolePos = new Vector2(-18f, 0.5f);
        //smooth = GameObject.FindWithTag("MainCamera").GetComponent<SmoothFollow>();
        //InitRoleDataDic();
    }
    private void InitRoleDataDic()
    {
        Debug.Log("------创建-----");
       // roleDic.Add(RoleType.Owner, new RoleData(RoleType.Owner, "PennyOwner", rolePos));
    }
    //public void CreatRoles()
    //{


    //        GameObject go = GameObject.Instantiate(roleProfab,rolePos, Quaternion.identity);
    //        Debug.Log("======" + go.name + "=====");
    //      Debug.Log("======断点=====");
    //    //if (rd.RoleType == ownerRoleType)
    //    //{

    //    ownerGO = go;
    //        //    //smooth.SetTarget(ownerGO.transform);
    //        //    // ownerGO.GetComponent<PlayerInfo>().isLocal = true;
    //        //}
    //        //else
    //        //{
    //        //    riverGO = go;
    //        //}
    //}
    //public GameObject GetOwnerObject()
    //{
    //    return ownerGO;
    //}


    public void UpdateResult(int totalCount, int winCount)
    {
        userData.TotalCount = totalCount;
        userData.WinCount = winCount;
    }



    public void GameOver()
    {
        //private GameObject currentRoleGameObject;
        //private GameObject playerSyncRequest;
        //private GameObject remoteRoleGameObject;

        //private ShootRequest shootRequest;
        //private AttackRequest attackRequest;
        //GameObject.Destroy(ownerGO);
        //GameObject.Destroy(playerSyncRequest);
       // GameObject.Destroy(remoteRoleGameObject);
        //shootRequest = null;
        //attackRequest = null;
    }
    //private RoleData GetRoleData(RoleType rt)
    //{
    //    RoleData rd = null;
    //    roleDic.TryGetValue(rt, out rd);
    //    return rd;
    //}
    //public void AddControlScript()//给角色添加脚本
    //{
    //    ownerGO.AddComponent<PlayerController>();
    //    //PlayerAttack playerAttack = currentRoleGameObject.AddComponent<PlayerAttack>();
    //    RoleType rt = ownerGO.GetComponent<PlayerInfor>().roleType;
    //    RoleData rd = GetRoleData(rt);
    //    //playerAttack.arrowPrefab = rd.ArrowPrefab;
    //    //playerAttack.SetPlayerMng(this);
    //}
    public void CreateSyncRequest()
    {
      // playerSyncRequest = new GameObject("PlayerSyncRequest");
    //    playerSyncRequest.AddComponent<MoveRequest>().SetLocalPlayer(ownerGO.transform, ownerGO.GetComponent<PlayerController>())
    //        .SetRemotePlayer(riverGO.transform);
    //    //shootRequest = playerSyncRequest.AddComponent<ShootRequest>();
    //    //shootRequest.playerMng = this;
       // getScoreRequest = playerSyncRequest.AddComponent<GetScoreRequest>();
    }



}
