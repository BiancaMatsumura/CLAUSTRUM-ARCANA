using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public List<NPCConversation> Endialogues; 
    public List<NPCConversation> PtDialogues;
    public static bool english = true;
    public static bool portugues = false;

    void Start()
    {
        if(!portugues && english)
        {
            if (Endialogues != null && Endialogues.Count > 0)
            {
                ConversationManager.Instance.StartConversation(Endialogues[0]); 
            }
        }
        else
        {
            if(PtDialogues != null && PtDialogues.Count > 0)
            {
                ConversationManager.Instance.StartConversation(PtDialogues[0]);
            }
        }


    }

    public void StartDialogue(int index)
    {
        if(!portugues && english)
        {
            if (Endialogues != null && index >= 0 && index < Endialogues.Count)
            {
                ConversationManager.Instance.StartConversation(Endialogues[index]);
            }
        }
        else
        {
            if (PtDialogues != null && index >= 0 && index < PtDialogues.Count)
            {
                ConversationManager.Instance.StartConversation(PtDialogues[index]);
            }
        }

    }
}
