
using UnityEngine;
using DialogueEditor;

public class PaperEvent : MonoBehaviour
{ 
    public float maxDistance = 100f;
    private bool umavez = true;
    public DialogueController myConvarsation;


    public void FixedUpdate()
    {
        if(!umavez)
        return;

        Ray raio = new Ray(transform.position, Vector3.up);
        RaycastHit hit;

        if (Physics.Raycast(raio, out hit, maxDistance))
        {
           //se o armario estiver em cima do desenho fazer algo
          
        
        }
        else
        {
            
            StartDialogue();
            umavez = false;

        }
    }

    public void StartDialogue()
    {
            myConvarsation.Dialogue02();
            umavez = true;
    }
}
