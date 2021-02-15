using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class N_BenManager : MonoBehaviourPunCallbacks
{
    NC_Const allString = new NC_Const();

    List<string> Messages = new List<string>();

    void Connect() => PhotonNetwork.ConnectUsingSettings();
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        RoomOptions _room = new RoomOptions()
        {
            MaxPlayers = 4
        };
        PhotonNetwork.JoinOrCreateRoom(allString.Room, _room, TypedLobby.Default);
        Debug.Log("OK to master");
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log($"OK to room = {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.Instantiate("BenPlayer", Random.insideUnitSphere * 5, Quaternion.identity);
    }

    private void OnGUI()
    {
        GUILayout.TextField($"oui");
        if (GUILayout.Button("Send"))Debug.Log("Message Sent");
        if (GUILayout.Button("Connect")) Connect();

        for (int i = 0; i < Messages.Count; i++)
        {
            GUILayout.Box($"{PhotonNetwork.NickName} {System.DateTime.Now}");
            Debug.Log($"Message {Messages[i]}");
        }
        GUILayout.Box(PhotonNetwork.NetworkClientState.ToString());
        PhotonNetwork.NickName = GUILayout.TextField(allString.PlayerName);
        if (PhotonNetwork.InRoom)
        {
            int _currentPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
            int _maxPlayers = PhotonNetwork.CurrentRoom.MaxPlayers;
            GUILayout.Box($"Player name {PhotonNetwork.NickName}");
            GUILayout.Box($"Room datas = {PhotonNetwork.CurrentRoom.Name} : {_currentPlayers}/{_maxPlayers} players");

        }

    }
}
