
using UnityEngine;
using DialogueEditor;

public class PaperEvent : MonoBehaviour
{ 
    public float maxDistance = 100f;

    public NPCConversation myConvarsation;


    public void FixedUpdate()
    {
        Ray raio = new Ray(transform.position, Vector3.up);
        RaycastHit hit;

        if (Physics.Raycast(raio, out hit, maxDistance))
        {
           //se o armario estiver em cima do desenho fazer algo
        
        }
        else
        {
            Debug.Log("entenda");
        }
    }

    public void StartDialogue()
    {
            ConversationManager.Instance.StartConversation(myConvarsation);
        

    }
}
