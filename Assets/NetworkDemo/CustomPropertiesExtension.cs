using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public static class CustomPropertiesExtension
{
    public static bool IsColor(this PhotonView _view) => _view.Owner.CustomProperties["color"] != null;

    public static void SetColorOnline(this PhotonView _view, Color _color)
    {
        float[] _colorFloat = new float[] { _color.r, _color.g, _color.b };
        Hashtable _colorTable = new Hashtable();
        _colorTable.Add("color", _colorFloat);
        _view.Owner.SetCustomProperties(_colorTable);
    }

    public static Color GetColorOnline(this PhotonView _view)
    {
        bool _canGetColor = _view.Owner.CustomProperties["color"] != null;
        if (!_canGetColor) return Color.white;
        float[] _colorDatas = (float[])_view.Owner.CustomProperties["color"];
        return new Color(_colorDatas[0], _colorDatas[1], _colorDatas[2]);
    }
}
