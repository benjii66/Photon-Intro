using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using TMPro;
using Random = UnityEngine.Random;
using Hashtable = ExitGames.Client.Photon.Hashtable;

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
		playerColor = GetComponent<Renderer>();
		if (!IsValid) return;
		nameLabel.text = photonID.Owner.NickName;
	}

	void SetNetworkColor()
    {
		if (!IsValid) return;
		float[] _colorFloat = null;
		Color _color = Color.white;
		if (photonID.IsMine)
        {
			Hashtable _properties = new Hashtable();
			_color = Random.ColorHSV();
			_colorFloat = new float[] { _color.r, _color.g, _color.b };
			_properties.Add("color", _colorFloat);
			photonID.Owner.SetCustomProperties(_properties);
		}
		else
        {
			_colorFloat = (float[])photonID.Owner.CustomProperties["color"];
			_color = new Color(_colorFloat[0], _colorFloat[1], _colorFloat[2]);
        }
		playerColor.material.color = _color;
    }

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		SetNetworkColor();
	}
}