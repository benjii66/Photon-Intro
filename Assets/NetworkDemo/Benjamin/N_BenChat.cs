using UnityEngine;
using Photon.Pun;
using TMPro;
using System;
using Random = UnityEngine.Random;


public class N_BenChat : MonoBehaviour
{
    public event Action OnUpdatePlayer = null;

    [SerializeField] PhotonView photonID = null;
    [SerializeField] TextMeshPro nameLabel = null;
    [SerializeField] Renderer playerColor = null;
    Vector3 localColor = Vector3.zero, onlineColor = Vector3.zero;
    string messages = "";

    public bool IsValid => photonID && nameLabel && playerColor;

    private void Start()
    {
        InitPlayer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && photonID.IsMine)
            SetPlayerNewColor(GetRandomColor());

        if (Input.GetKeyDown(KeyCode.KeypadEnter) && photonID.IsMine)
            SendLocalMessage(messages);
    }
    public void InitPlayer()
    {
        photonID = PhotonView.Get(this);
        if (!playerColor) playerColor = GetComponent<Renderer>();
        if (!IsValid) return;
        nameLabel.text = photonID.Owner.NickName;
        SetNetworkColor();
    }

    #region Color
    void SetNetworkColor()
    {
        if (!IsValid) return;
        Color _color = Random.ColorHSV();
        if (photonID.IsMine)
        {
            if (photonID.IsColor())
                _color = photonID.GetColorOnline();
            else
                photonID.SetColorOnline(_color);
        }
        else
            _color = photonID.GetColorOnline();
        playerColor.material.color = _color;

    }

    Color GetRandomColor() => Random.ColorHSV();
    void SetPlayerNewColor(Color _color)
    {
        if (!IsValid) return;
        playerColor.material.color = _color;
        if (!photonID.IsMine) return;
        photonID.RPC("SetPlayerNewColorOnline", RpcTarget.All, new float[] { _color[0], _color[1], _color[2] });
    }
    [PunRPC]
    void SetPlayerNewColorOnline(float[] _color)
    {
        if (!playerColor) return;
        playerColor.material.color = new Color(_color[0], _color[1], _color[2]);
    } 
    #endregion


    void SendLocalMessage(string _message)
    {
        if (!IsValid) return;
        if (!photonID.IsMine) return;
        photonID.RPC("SendOnlineMessage", RpcTarget.All,_message);
    }
    [PunRPC]
    void SendOnlineMessage(string _message)
    {
        messages = _message;
    }

}


//public void OnGetMessages(string _channelName, string[] _senders, string[] _messages, Color _color)
//{
//    //_color = player.set
//    for (int i = 0; i < _senders.Length; i++)
//        messages = string.Format($" {messages} {_senders} {_color} : {_messages}, ");
//    Console.WriteLine($"All Messages: {_channelName} ({_senders.Length}) > {messages}");
//}
//public void OnPrivateMessage(string _sender, string _message, string _channelName, Color _color)
//{
//    Console.WriteLine($"Message: {_channelName} ({_sender}) {_color} > {_message}");
//    //ChatChannel _ch = this.chatClient.PrivateChannels[_channelName];
//    foreach (char _msg in messages)
//        Console.WriteLine(_msg);
//}