using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public List<NPCConversation> dialogues; 
    public List<NPCConversation> PtDialogues;
    public static bool tradução = false;

    void Start()
    {
        if(!tradução)
        {
            if (dialogues != null && dialogues.Count > 0)
            {
                ConversationManager.Instance.StartConversation(dialogues[0]); 
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
        if(!tradução)
        {
            if (dialogues != null && index >= 0 && index < dialogues.Count)
            {
                ConversationManager.Instance.StartConversation(dialogues[index]);
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
