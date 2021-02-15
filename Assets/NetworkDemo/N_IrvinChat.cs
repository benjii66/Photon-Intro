using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class N_IrvinChat : MonoBehaviour, IPunObservable
{
    public event Action OnUpdateChat = null;
    [SerializeField] TMP_InputField inputField = null;
    [SerializeField] Button sendButton = null;
    [SerializeField] PhotonView photonID = null;
    public bool IsValid => photonID && inputField && sendButton;

    void Start()
    {
        InitNetworkObject();
        if (!IsValid) return;
        if (photonID.IsMine) OnUpdateChat += UpdateLocal;
        else OnUpdateChat += UpdateOnline;

    }

    void Update() => OnUpdateChat?.Invoke();

    void SendChatMessage()
    {

    }

    void UpdateLocal()
    {
        
    }

    void UpdateOnline()
    {

    }

    void InitNetworkObject()
    {
        photonID = PhotonView.Get(this);
    }

    void WriteOnlineVector(bool _canWrite, PhotonStream _stream, Vector3 _vector)
    {
        if (!_canWrite) return;
        if (_stream.IsWriting) _stream.Serialize(ref _vector);
    }

    Vector3 ReadOnlineVector(PhotonStream _stream)
    {
        if (_stream.IsReading)
            return (Vector3)_stream.ReceiveNext();
        else return Vector3.zero;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}