using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class PhotonInit : MonoBehaviour
{
    //현재 제작한 App의 버전 정보
    public string version = "v1.0";

    //플레이어 이름 및 룸 이름 입력 UI 항목 연결
    public InputField userId;
    public InputField roomName;

    //RoomItem이 child로 생성될 parent 객체
    public GameObject scrollContents;
    public GameObject roomItem;

    private void Awake()
    {
        //포톤 클라우드에 접속
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(version);
        }
        userId.text = GetUserId();
        roomName.text = "ROOM_" + Random.Range(0, 999).ToString("000");
    }

    //포톤 클라우드에 정상적으로 접속한 후 로비에 입장하면 호출되는 콜백 함수
    void OnJoinedLobby()
    {
        Debug.Log("Entered Lobby !");
        //PhotonNetwork.JoinRandomRoom();//무작위 룸 접속
    }

    string GetUserId()
    {
        string userId = PlayerPrefs.GetString("USER_ID");

        if(string.IsNullOrEmpty(userId))
        {
            userId = "USER_" + Random.Range(0, 999).ToString("000");
        }
        return userId;
    }

    //생성된 룸 목록이 변경됬을 때 호출되는 콜백 함수
    void OnReceivedRoomListUpdate()
    { 
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("ROOM_ITEM"))
        {
            Destroy(obj);
        }

        //grid layout group 컴포넌트의 constraint count 값을 증가시킬 변수
        int rowCount = 0;
        //스크롤 영역 초기화
        scrollContents.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 15);

        foreach(RoomInfo _room in PhotonNetwork.GetRoomList())
        {
            Debug.Log(_room.Name);
            //RoomItem 프리팹을 동적으로 생성
            GameObject room = (GameObject)Instantiate(roomItem);
            //생성한 RoomItem 프리팹의 parent를 지정
            room.transform.SetParent(scrollContents.transform, false);

            //생성한 RoomItem에 정보 표시하기 위해 텍스트 정보 전달
            RoomData roomdata = room.GetComponent<RoomData>();
            roomdata.roomName = _room.Name;
            roomdata.connectPlayer = _room.PlayerCount;
            roomdata.maxPlayers = _room.MaxPlayers;

            roomdata.DispRoomData();

            //grid layout group 컴포넌트의 constraint count 값을 증가시킴
            scrollContents.GetComponent<GridLayoutGroup>().constraintCount = ++rowCount;
            //스크롤 영역의 높이를 증가시킴
            scrollContents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 62);

            //RoomItem의 Button컴포넌트에 클릭 이벤트를 동적으로 연결
            roomdata.GetComponent<Button>().onClick.AddListener(
               delegate { OnClickRoomItem(roomdata.roomName); }
            );
            // 왜 roomdata?? roomdata는 스크립트 컴포넌트고, 가지고 있는정보가 text, int 이거밖에 없는데 왜 이걸로 됨??
            //getcomponent에서 button을 어떻게 불러옴???
            //getcomponent는 스크립트(여기서는 roomdata)가 포함된 게임 오브젝트에서 <T>형 컴포넌트를 찾아온다
            //즉 roomdata나 room 이나 어찌되었건 둘다 roomitem gameobject안에 포함된 스크립트이기때문에 
            //저렇게 불러도 같은 버튼의 onclick을 불러옴 ㅇㅇ

        }
        scrollContents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 10);

    }

    public void OnClickJoinRandomRoom()
    {
        //로컬 유저 이름 설정
        //PhotonNetwork.player.name = userId.text; 사용하지 않음
        PhotonNetwork.player.NickName = userId.text;

        PlayerPrefs.SetString("USER_ID", userId.text);

        PhotonNetwork.JoinRandomRoom();
    }

    //RoomItem 클릭 시 해당 룸 접속
    public void OnClickRoomItem(string roomName)
    {
        //로컬 플레이어 이름 설정
        PhotonNetwork.player.NickName = userId.text;
        // 플레이어 이름 저장
        PlayerPrefs.SetString("USER_ID", userId.text);

        //인자로 전달된 이름에 해당하는 룸으로 입장
        PhotonNetwork.JoinRoom(roomName);
    }

    //Make Room 버튼 클릭시 호출
    public void OnClickCreateRoom()
    {
        string _roomName = roomName.text;
        //현재 roomName이 공백 혹은 NULL 일경우 룸 이름 자동 지정
        if(string.IsNullOrEmpty(roomName.text))
        {
            _roomName = "ROOM_" + Random.Range(0, 999).ToString("000");
        }
        //플레이어 이름 저장
        PhotonNetwork.player.NickName = userId.text;
        PlayerPrefs.SetString("USER_ID", userId.text);

        Debug.Log(_roomName);

        //생성 룸의 조건(옵션) 설정
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 20;
        //룸 생성
        PhotonNetwork.CreateRoom(_roomName, roomOptions, TypedLobby.Default);
    }

    //룸 생성 실패시 호출되는 콜백 함수
    void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("Create Room Failed = " + codeAndMsg[1]);
    }

    //무작위 룸 접속에 실패한 경우 호출되는 콜백 함수
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("No Rooms !");

        //룸 생성
        PhotonNetwork.CreateRoom("MyRoom");
    }

    //룸에 입장하면 호출되는 콜백 함수
    void OnJoinedRoom()
    {
        Debug.Log("Enter Room");

        // CreateTank();
        StartCoroutine(this.LoadBattleField());
    }

    //룸 씬으로 이동하는 코루틴 함수
    IEnumerator LoadBattleField()
    {
        //씬을 이동하는동안 포톤 클라우드 서버로부터 네트워크 메시지 수신 중단
        PhotonNetwork.isMessageQueueRunning = false;
        //백그라운드로 씬 로딩
        AsyncOperation ao = SceneManager.LoadSceneAsync("scBattleField");
        yield return ao;
    }

    //탱크를 생성하는 함수
    void CreateTank()
    {
        float pos = Random.Range(-100.0f, 100.0f);
        PhotonNetwork.Instantiate("Tank", new Vector3(pos, 20.0f, pos), Quaternion.identity, 0);
    }

    private void OnGUI()
    {
        //화면 좌측 상단에 접속 과정에 대한 로그 출력
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}   
