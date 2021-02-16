using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ICM_ChatManager : MonoBehaviour
{
    [SerializeField] PhotonView chatNetID = null;
    [SerializeField] ICM_Chat chat = null;
    public bool IsValid => chatNetID;

    private void Start()
    {
        InitChat();
    }

    void InitChat()
    {
        chatNetID = PhotonView.Get(this);
        if (!IsValid) return;
        chat = new ICM_Chat();
    }

    void OnGUI()
    {
        if (!PhotonNetwork.IsConnected) return;
        Rect _windowRect = new Rect(0, Screen.height - Screen.height * .5f, Screen.width * .5f, Screen.height * .5f);
        GUILayout.Window(0, _windowRect, ChatWindow, "Chat");
    }

    void ChatWindow(int _id)
    {
        GUILayout.BeginHorizontal();
        chat.CurrentLine = GUILayout.TextArea(chat.CurrentLine);
        if(GUILayout.Button("Send")) chat?.SendChatMessage();
        GUILayout.EndHorizontal();

        for (int i = 0; i < chat.ChatConversation.Count; i++)
            GUILayout.Box(chat.ChatConversation[i].CompleteMessage, GetChatStyle());
    }

    GUIStyle GetChatStyle()
    {
        GUIStyle _chat = GUI.skin.box;
        _chat.richText = true;
        return _chat;
    }
}
