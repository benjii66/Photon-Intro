using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;



public class N_BenChat : MonoBehaviour
{
    [SerializeField] PhotonView photonID = null;
    [SerializeField] N_Player player = null;
    string messages = "";
    public void OnGetMessages(string _channelName, string[] _senders, string[] _messages, Color _color)
    {
        //_color = player.set
        for (int i = 0; i < _senders.Length; i++)
            messages = string.Format($" {messages} {_senders} {_color} : {_messages}, ");
        Console.WriteLine($"All Messages: {_channelName} ({_senders.Length}) > {messages}");


    }
    public void OnPrivateMessage(string _sender, string _message, string _channelName, Color _color)
    {
        Console.WriteLine($"Message: {_channelName} ({_sender}) {_color} > {_message}");
        //ChatChannel _ch = this.chatClient.PrivateChannels[_channelName];
        foreach (char _msg in messages)
            Console.WriteLine(_msg);

    }
}
