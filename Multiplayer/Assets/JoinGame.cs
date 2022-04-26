using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;


public class JoinGame : MonoBehaviour
{
    private NetworkManager networkManager;
    private List<GameObject> roomList = new List<GameObject>();
    [SerializeField] private Text status;
    [SerializeField] private GameObject roomListItemPrefab;
    [SerializeField] private Transform roomListParent;


    // Start is called before the first frame update
    void Start()
    {
        networkManager = NetworkManager.singleton;
        if(networkManager = NetworkManager.singleton)
        {
            networkManager.StartMatchMaker();

        }
        RefreshRoomList();
    }

    public void RefreshRoomList()
    {
        networkManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
        status.text = "Loading...";
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        status.text = "";
        if(matchList == null)
        {
            status.text = "Couldn't get room list.";
            return;
        }
        ClearRoomList();
        foreach(MatchInfoSnapshot item in matchList)
        {
            GameObject roomListItemGO = Instantiate(roomListItemPrefab);
            roomListItemGO.transform.SetParent(roomListParent);

            RoomListItem roomListItem = roomListItemGO.GetComponent<RoomListItem>();
            if(roomListItem!= null)
            {
                roomListItem.Setup(item, JoinRoom);
            }
            roomList.Add(roomListItemGO);

        }
        if (roomList.Count == 0)
        {
            status.text = "No rooms at the moment.";

        }
    }

    private void ClearRoomList()
    {
        for(int i =0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }
        roomList.Clear();
    }

    public void JoinRoom(MatchInfoSnapshot _match)
    {
        networkManager.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
        StartCoroutine(WaitForJoin());

    }

    IEnumerator WaitForJoin()
    {
        ClearRoomList();
        int counntdown = 10;
        while (counntdown > 0)
        {
            status.text = "JOINING... (" + counntdown + ")";

            yield return new WaitForSeconds(1);
            counntdown--;
        }

        status.text = "Failed to connect. ";
        yield return new WaitForSeconds(1);

        MatchInfo matchInfo = networkManager.matchInfo;
        if (matchInfo != null)
        {
            networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
            networkManager.StopHost();
        }

        RefreshRoomList();


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
