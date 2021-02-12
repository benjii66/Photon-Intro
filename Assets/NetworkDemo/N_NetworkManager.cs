using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class N_NetworkManager : MonoBehaviourPunCallbacks
{
    string playerName = "Toto";

    void Connect() => PhotonNetwork.ConnectUsingSettings();
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        RoomOptions _room = new RoomOptions()
        {
            MaxPlayers = 4
        };
        PhotonNetwork.JoinOrCreateRoom("O3D", _room, TypedLobby.Default);
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
        PhotonNetwork.Instantiate("Player", Random.insideUnitSphere * 5, Quaternion.identity);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Connect")) Connect();
        GUILayout.Box(PhotonNetwork.NetworkClientState.ToString());
        PhotonNetwork.NickName = GUILayout.TextField(playerName);
        if(PhotonNetwork.InRoom)
        {
            int _currentPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
            int _maxPlayers = PhotonNetwork.CurrentRoom.MaxPlayers;
            GUILayout.Box($"Player name {PhotonNetwork.NickName}");
            GUILayout.Box($"Room datas = {PhotonNetwork.CurrentRoom.Name} : {_currentPlayers}/{_maxPlayers} players");

        }
        
    }
}
