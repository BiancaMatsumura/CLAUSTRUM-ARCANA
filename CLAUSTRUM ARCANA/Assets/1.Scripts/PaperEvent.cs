
using UnityEngine;
using DialogueEditor;
using Cinemachine;
public class PaperEvent : MonoBehaviour
{ 
    public float maxDistance = 100f;
    private bool umavez = true;
    public DialogueController myConvarsation;
    public Animator gato; 
    public CinemachineVirtualCamera cutscene;
    public Animator pergaminho;
    public Animator visão;



   
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
            StartANimation();
            cutscene.Priority = 40;


        }
    }

    public void StartDialogue()
    {
            myConvarsation.Dialogue02();
            umavez = true;
    }
    
    public void StartANimation()
    {
        gato.SetTrigger("GatoSim");
    }

    public void EndCutscene()
    {
        cutscene.Priority = -1;
        visão.SetTrigger("GatilhoVision");
        pergaminho.SetTrigger("GatilhoUIanim");

       
    }
}
