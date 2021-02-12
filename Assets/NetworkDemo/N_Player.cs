using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using TMPro;
using Random = UnityEngine.Random;

public class N_Player : MonoBehaviour, IPunObservable
{
	public event Action OnUpdatePlayer = null;
	[SerializeField] PhotonView photonID = null;
	[SerializeField] TMP_Text nameLabel = null;
	Vector3 localColor = Vector3.zero, onlineColor = Vector3.zero;
	Renderer playerColor = null;
	public bool IsValid => photonID && playerColor && nameLabel;

	void Start()
	{
		InitPlayer();
	}

	void InitPlayer()
	{
		photonID = PhotonView.Get(this);
		if (!IsValid) return;
		nameLabel.text = photonID.Owner.NickName;
		playerColor = GetComponent<Renderer>();
		SetColor();
	}

	void Update() => ApplyColor();

	void ApplyColor()
	{
		if (!IsValid) return;
		playerColor.material.color = photonID.IsMine ?
						   new Color(localColor.x, localColor.y, localColor.z) :
						   new Color(onlineColor.x, onlineColor.y, onlineColor.z);
	}

	void SetColor()
	{
		Color _color = Random.ColorHSV();
		localColor = new Vector3(_color.r, _color.g, _color.b);
	}
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
			stream.Serialize(ref localColor);
		else onlineColor = (Vector3)stream.ReceiveNext();
	}
}