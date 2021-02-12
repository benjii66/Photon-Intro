using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class N_TransformSync : MonoBehaviourPun, IPunObservable
{
    public event Action OnUpdatePlayer = null;
    string name = "coucoucestmoi";

    [SerializeField] PhotonView photonID = null;
    [SerializeField] bool syncPosition = true, syncRotation = false, syncScale = false;
    Vector3 localPosition = Vector3.zero, onlinePosition = Vector3.zero,
            localRotationEuler = Vector3.zero, onlineRotationEuler = Vector3.zero,
            localScale = Vector3.zero, onlineScale = Vector3.zero;

    public bool IsValid => photonID;

    void Start()
    {
        InitNetworkObject();
        if (!IsValid) return;
        if (photonID.IsMine) OnUpdatePlayer += UpdateLocal;
        else OnUpdatePlayer += UpdateOnline;
    }

    void Update() => OnUpdatePlayer?.Invoke();

    void UpdateLocal()
    {
        localPosition = transform.position;
        localRotationEuler = transform.eulerAngles;
        localScale = transform.localScale;
    }

    void UpdateOnline()
    {
        transform.position = Vector3.MoveTowards(transform.position, onlinePosition, .1f);
        transform.eulerAngles = Vector3.RotateTowards(transform.eulerAngles, onlineRotationEuler, .1f, .1f);
        transform.localScale = Vector3.MoveTowards(transform.localScale, onlineScale, .1f);
    }

    void InitNetworkObject()
    {
        photonID = PhotonView.Get(this);

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        WriteOnlineVector(syncPosition, stream, localPosition);
        WriteOnlineVector(syncRotation, stream, localRotationEuler);
        WriteOnlineVector(syncScale, stream, localScale);

        onlinePosition = ReadOnlineVector(stream);
        onlineRotationEuler = ReadOnlineVector(stream);
        onlineScale = ReadOnlineVector(stream);
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
}
