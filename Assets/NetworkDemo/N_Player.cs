using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System;

public class N_Player : MonoBehaviour
{
    [SerializeField] PhotonView photonID = null;
    [SerializeField] TextMeshPro nameText = new TextMeshPro();

    public bool IsValid => photonID && nameText;

    private void Start() => InitPlayer();

    void InitPlayer()
    {
        photonID = PhotonView.Get(this);
        if (!IsValid) return;
        nameText.text = photonID.Owner.NickName;
    }
}