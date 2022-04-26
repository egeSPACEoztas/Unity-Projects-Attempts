using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    public delegate void JoinRoomDelegate(MatchInfoSnapshot match);
    private JoinRoomDelegate joinRoomCallBack;

    private MatchInfoSnapshot match;

    [SerializeField] private Text roomNameText;

    public void Setup(MatchInfoSnapshot _match,JoinRoomDelegate _joinRoomCallback)
    {
        match = _match;
        joinRoomCallBack = _joinRoomCallback;
        roomNameText = GetComponentInChildren<Text>();
        roomNameText.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")";
    }

    public void JoinRoom()
    {
        joinRoomCallBack.Invoke(match);
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
