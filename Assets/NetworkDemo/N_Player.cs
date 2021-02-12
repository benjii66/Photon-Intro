using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using TMPro;

public class N_Player : MonoBehaviour
{
    public event Action OnUpdatePlayer = null;
    [SerializeField] PhotonView photonID = null;
    [SerializeField] TMP_Text nameLabel = null;
    public bool IsValid => photonID;

    void Start()
    {
        InitPlayer();
    }

  void InitPlayer()
	{
        photonID = PhotonView.Get(this);
        if (!IsValid) return;
        nameLabel.text = photonID.Owner.NickName;
	}
}