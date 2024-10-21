
using UnityEngine;
using DialogueEditor;

public class PaperEvent : MonoBehaviour
{ 
    public float maxDistance = 100f;
    private bool umavez = true;
    public NPCConversation myConvarsation;


    public void FixedUpdate()
    {
        if(!umavez)
        return;

        Ray raio = new Ray(transform.position, Vector3.up);
        RaycastHit hit;

        if (Physics.Raycast(raio, out hit, maxDistance))
        {
           //se o armario estiver em cima do desenho fazer algo
           Debug.Log("mewo");
        
        }
        else
        {
            
           StartDialogue();
            umavez = false;

        }
    }

    public void StartDialogue()
    {
            ConversationManager.Instance.StartConversation(myConvarsation);
        
            umavez = true;
    }
}
