using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using TMPro;
using Random = UnityEngine.Random;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class N_Player : MonoBehaviour
{
    public event Action OnUpdatePlayer = null;

    [SerializeField] PhotonView photonID = null;
    [SerializeField] TextMeshPro nameLabel = null;
    [SerializeField] Renderer playerColor = null;
    Vector3 localColor = Vector3.zero, onlineColor = Vector3.zero;

    public bool IsValid => photonID && nameLabel && playerColor;

    private void Start()
    {
        InitPlayer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && photonID.IsMine)
            SetPlayerNewColor(GetRandomColor());
    }
    public void InitPlayer()
    {
        photonID = PhotonView.Get(this);
        if (!playerColor) playerColor = GetComponent<Renderer>();
        if (!IsValid) return;
        nameLabel.text = photonID.Owner.NickName;
        SetNetworkColor();
    }

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
        photonID.RPC("SetPlayerNewColorOnline", RpcTarget.All, new float[] {_color[0], _color[1], _color[2]});
    }

    [PunRPC]
    void SetPlayerNewColorOnline(float[] _color)
    {
        if (!playerColor) return;
        playerColor.material.color = new Color(_color[0], _color[1], _color[2]);
    }
}