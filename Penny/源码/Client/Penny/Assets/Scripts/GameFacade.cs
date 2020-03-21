using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.SceneManagement;

public class GameFacade : MonoBehaviour {
    private static GameFacade _instance;
    public static GameFacade Instance { get {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("GameFacade");
                if (go == null) return null;
                _instance = go.GetComponent<GameFacade>();
            }
            return _instance; } }
    private UIManager uiMag;
    private AudioManager audioMag;
    private CameraManager cameraMag;
    private PlayerManager playerMag;
    private RequestManager requestMag;
    private ClientManager clientMag;


    private bool isStartGame = false;
    // Use this for initialization
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);return;
        }
        _instance = this;
    }
    void Start () {
        InitManager();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateManager();
        if (isStartGame)
        {
            StartGame();
            isStartGame = false;          
        }
	}
    private void LateUpdate()
    {
        LateUpdateManager();
    }
    private void OnDestroy()
    {
        DestroyManager();
    }
    private void UpdateManager()
    {
        uiMag.Update();
        audioMag.Update();
        cameraMag.Update();
        playerMag.Update();
        requestMag.Update();
        clientMag.Update();
    }
    private void LateUpdateManager()
    {
        uiMag.LateUpdate();
        audioMag.LateUpdate();
        cameraMag.LateUpdate();
        playerMag.LateUpdate();
        requestMag.LateUpdate();
        clientMag.LateUpdate();
    }
    private void InitManager()
    {
        uiMag = new UIManager(this);
        audioMag = new AudioManager(this);
        cameraMag = new CameraManager(this);
        playerMag = new PlayerManager(this);
        requestMag = new RequestManager(this);
        clientMag = new ClientManager(this);

        uiMag.OnInit();
        audioMag.OnInit();
        cameraMag.OnInit();
        playerMag.OnInit();
        requestMag.OnInit();
        clientMag.OnInit();
    }
    private void DestroyManager()
    {
        uiMag.OnDestroy();
        audioMag.OnDestroy();
        cameraMag.OnDestroy();
        playerMag.OnDestroy();
        requestMag.OnDestroy();
        clientMag.OnDestroy();
    }

    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestMag.AddRequest(actionCode, request);
    }
    public void RemoveRequest(ActionCode actionCode)
    {
        requestMag.RemoveRequest(actionCode);
    }
    public void HandleResponse(ActionCode actionCode,string data)
    {
        requestMag.HandleResponse(actionCode, data);
    }
    public void ShowMessage(string msg)
    {
        uiMag.ShowMessage(msg);
    }
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        clientMag.SendRequest(requestCode, actionCode, data);
    }
    //public void PlayBGM(string soundName)
    //{
    //    audioMag.PlayBGM(soundName);
    //}
    public void PlayNormalAudio(string soundName)
    {
        audioMag.PlayNormalAudio(soundName);
    }
    public void SetUserData(UserData ud)
    {
        playerMag.UserData = ud;
    }
    public UserData GetUserData()
    {
        return playerMag.UserData;
    }
    public void SetOwnerRoleType(RoleType roleType)
    {
        //playerMag.SetOwnerRoleType(roleType);
    }
    //public GameObject GetOwnerObject()
    //{
    //    return playerMag.GetOwnerObject();
    //}
    public void StartGameSync()
    {
        isStartGame = true;
    }
    //public void GetFollowRole()
    //{
    //    cameraMag.FollowRole();
    //}
    private void StartGame()
    {
        Debug.Log("------创建游戏物体------");
        //playerMag.CreatRoles();
        cameraMag.FollowRole();
    }


    public void UpdateResult(int totalCount, int winCount)
    {
        playerMag.UpdateResult(totalCount, winCount);
    }

    public void GameOver()
    {
        //.WalkthroughScene();
        playerMag.GameOver();
    }
    public void Playing()
    {
    //    playerMag.AddControlScript();
       playerMag.CreateSyncRequest();
    }

    public void ReLoad()
    {
        StartCoroutine(ReloadScene());
    }
    public IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);

    }
    //public void SendScore(int score)
    //{
    //   playerMag.SendAttack(score);
    //}
}
