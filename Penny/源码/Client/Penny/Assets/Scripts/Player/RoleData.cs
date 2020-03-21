using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class RoleData  {
    public RoleType RoleType { get; private set; }
    public GameObject RoleProfab { get; private set; }
    public Vector2 RolePos { get; private set; }
    private const string PATH_PREFAB ="Role/";
    public RoleData(RoleType roleType,string rolePath,Vector2 rolePos)
    {
        this.RoleType = roleType;
        this.RoleProfab = Resources.Load(PATH_PREFAB+rolePath) as GameObject;
        this.RolePos = rolePos;
    }
}
