using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomData : MonoBehaviour
{
    //외부 접근을 위해 public 으로 선언하지만 Inspector에 노출하지 않음
    [HideInInspector]
    public string roomName = "";

    [HideInInspector]
    public int connectPlayer = 0;

    [HideInInspector]
    public int maxPlayers = 0;

    //룸 박스 표시 UI
    public Text textRoomName;
    public Text textConnectInfo;

    //룸 정보를 전달 후 TextUI항목에 표시하는 함수
    public void DispRoomData()
    {
        textRoomName.text = roomName;
        textConnectInfo.text = "("+connectPlayer.ToString()+"/"+maxPlayers.ToString()+")";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
