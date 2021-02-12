using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Random = UnityEngine.Random;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class N_Color : MonoBehaviour
{
	[SerializeField] PhotonView photonID = null;
	Renderer playerColor = null;
	NC_Const allString = new NC_Const(); 
	public bool IsValid => photonID && playerColor;
	float[] colorFloat = null;
	Color renderColor = Color.white;
	void SetNetworkColor()
	{
		if (!IsValid) return;
		if (photonID.IsMine)
		{
			Hashing();
		}
		else
		{
			colorFloat = (float[])photonID.Owner.CustomProperties[allString.ColorString];
			renderColor = new Color(colorFloat[0], colorFloat[1], colorFloat[2]);
		}
		playerColor.material.color = renderColor;
	}
	void Hashing()
	{
		Hashtable _properties = new Hashtable();
		bool _gotProperties = _properties != null;

		renderColor = Random.ColorHSV();
		colorFloat = new float[] { renderColor.r, renderColor.g, renderColor.b };
		if (_gotProperties)
		{
			_properties.Add(allString.ColorString, colorFloat);
			photonID.Owner.SetCustomProperties(_properties);
		}
	}


	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		SetNetworkColor();
	}

}
