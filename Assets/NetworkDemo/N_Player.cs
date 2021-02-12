using UnityEngine;
using Photon.Pun;
using System;
using TMPro;


public class N_Player : MonoBehaviour
{
	public event Action OnUpdatePlayer = null;
	[SerializeField] PhotonView photonID = null;
	[SerializeField] TMP_Text nameLabel = null;
	Renderer playerColor = null;
	public bool IsValid => photonID && playerColor && nameLabel;

	void Start()
	{
		InitPlayer();
	}

	void InitPlayer()
	{
		photonID = PhotonView.Get(this);
		playerColor = GetComponent<Renderer>();
		if (!IsValid) return;
		nameLabel.text = photonID.Owner.NickName;
	}


}