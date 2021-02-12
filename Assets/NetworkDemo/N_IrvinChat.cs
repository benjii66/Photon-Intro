using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_IrvinChat : MonoBehaviour
{
    [SerializeField] N_Player player = null;
    public void OnGetMessages(string _channelName, string[] _senders, string[] _messages, Color _color)
    {
        string _msgs = "";
        for (int i = 0; i < _senders.Length; i++)
            _msgs = string.Format($" {_msgs} {_senders} : {_messages}, ");
        //Console.WriteLine($"Messages: {_channelName} ({_senders.Length}) > {_msgs}");
    }
}
