using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public NPCConversation dialogue01;
    public NPCConversation dialogue02;
    public NPCConversation dialogue03;
    public NPCConversation dialogue04;
    public NPCConversation dialogue05;

    void Start()
    {
        ConversationManager.Instance.StartConversation(dialogue01);
    }

    public void Dialogue02()
    {
        ConversationManager.Instance.StartConversation(dialogue02);

    }

    public void Dialogue03()
    {
        ConversationManager.Instance.StartConversation(dialogue03);

    }

    public void Dialogue04()
    {
        ConversationManager.Instance.StartConversation(dialogue04);

    }
    public void Dialogue05()
    {
        ConversationManager.Instance.StartConversation(dialogue05);

    }
}
