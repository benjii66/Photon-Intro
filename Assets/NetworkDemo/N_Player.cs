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
		SetNetworkColor();
	}

	void SetNetworkColor()
	{
		if (!IsValid) return;
		Color _color = Random.ColorHSV();
		if (photonID.IsMine)
			if (photonID.IsColor()) _color = photonID.GetColorOnline();
			else photonID.SetColorOnline(_color);
		else _color = photonID.GetColorOnline();
		playerColor.material.color = _color;
	}
}

