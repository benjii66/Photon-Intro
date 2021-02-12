using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class N_TransformSync : MonoBehaviourPun, IPunObservable
{
    [SerializeField] PhotonView photoID = null;
    [SerializeField] bool syncPosition = true, syncRotation = false, syncScale = false;
    Vector3 localPosition = Vector3.zero, onlinePosition = Vector3.zero, localRotationEuler = Vector3.zero, onlineRotationEuler = Vector3.zero, localScale = Vector3.zero, onlineScale = Vector3.zero;
    public bool IsValid => photoID;
    private void Start()
    {
        InitNetworkObject();
    }

    void InitNetworkObject()
    {
        photoID = PhotonView.Get(this);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if(stream.IsWriting)
        //  {
        //      stream.Serialize(ref localPosition);

        //  }
        //  else
        //  {
        //      onlinePosition = (Vector3)stream.ReceiveNext();
        //  }
        WriteOnline(syncPosition, stream, localPosition);
        WriteOnline(syncRotation, stream, localPosition);
        WriteOnline(syncScale, stream, localPosition);
        onlinePosition = ReadOnlineVector(stream);
        onlineRotationEuler = ReadOnlineVector(stream);
        onlineScale = ReadOnlineVector(stream);

    }



    void WriteOnline(bool _canWrite, PhotonStream _stream, Vector3 _vector)
    {
        if (!_canWrite) return;
        if (_stream.IsWriting)
            _stream.Serialize(ref _vector);
    }

    Vector3 ReadOnlineVector(PhotonStream _stream)
    {
        if (_stream.IsReading)
            return (Vector3)_stream.ReceiveNext();
        else return Vector3.zero;

    }

}
