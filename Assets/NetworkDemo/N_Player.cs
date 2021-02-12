using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class N_Player : MonoBehaviour, IPunObservable
{
    public event Action OnUpdatePlayer = null;
    [SerializeField] Vector3 localPosition = Vector3.zero, onlinePosition = Vector3.zero;
    [SerializeField] Quaternion localRotation = Quaternion.identity, onlineRotation = Quaternion.identity;
    [SerializeField] PhotonView photonID = null;
    public bool IsValid => photonID;

    void Start()
    {
        photonID = PhotonView.Get(this);
        if (!IsValid) return;
        if(photonID.IsMine) OnUpdatePlayer += UpdateLocal;
        else OnUpdatePlayer += UpdateOnline;
    }

    void Update() => OnUpdatePlayer?.Invoke();

    void UpdateLocal()
    {
        localPosition = transform.position;
        localRotation = transform.rotation;
    }

    void UpdateOnline()
    {
        transform.position = Vector3.MoveTowards(transform.position, onlinePosition, .1f);
        transform.rotation = Quaternion.Lerp(transform.rotation, onlineRotation, .1f);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.Serialize(ref localPosition);
            stream.Serialize(ref localRotation);
        }
        else
        {
            onlinePosition = (Vector3)stream.ReceiveNext();
            onlineRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}