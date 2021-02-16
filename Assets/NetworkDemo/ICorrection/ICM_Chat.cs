using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

[Serializable]
public class ICM_Chat : MonoBehaviour
{
    [SerializeField] PhotonView chatNetOwner = null;
    [SerializeField] List<ICM_ChatMessage> chatConversation = new List<ICM_ChatMessage>();
    public List<ICM_ChatMessage> ChatConversation => chatConversation;
    public string CurrentLine { get; set; } = "";
    public void Init(PhotonView _owner) => chatNetOwner = _owner;

    public void SendChatMessage()
    {
        Send();
    }

    void Send()
    {
        ICM_ChatMessage _msg = new ICM_ChatMessage(PhotonNetwork.LocalPlayer.NickName, CurrentLine, GetChatColor());
        chatConversation.Add(_msg);
        CurrentLine = "";
        if (!chatNetOwner) return;
        chatNetOwner.RPC("SendLocalMessage", RpcTarget.Others, JsonUtility.ToJson(_msg));
    }

    [PunRPC]
    void SendOnlineMessage(string _objectData)
    {
        ICM_ChatMessage _msg = JsonUtility.FromJson<ICM_ChatMessage>(_objectData);
        chatConversation.Add(_msg);
    }

    string GetChatColor()
    {
        if (!PhotonNetwork.IsConnected) return $"#{ColorUtility.ToHtmlStringRGB(Color.white)}";
        float[] _colorFloat = (float[])PhotonNetwork.LocalPlayer.CustomProperties["color"];
        Color _color = new Color(_colorFloat[0], _colorFloat[1], _colorFloat[2]);
        return $"#{ColorUtility.ToHtmlStringRGB(_color)}";
    }
}

[Serializable]
public struct ICM_ChatMessage
{
    [SerializeField] string ownerNickname;
    [SerializeField] string message;
    [SerializeField] string date;
    [SerializeField] string colorData;

    public string OwnerNickname => ownerNickname;
    public string Message => message;
    public string CompleteMessage => $"<color={colorData}><b> {ownerNickname} - {date} : {message}</b></color>";

    public ICM_ChatMessage(string _owner, string _message, string _colorData)
    {
        ownerNickname = _owner;
        message = _message;
        date = System.DateTime.Now.ToString();
        colorData = _colorData;
    }
}