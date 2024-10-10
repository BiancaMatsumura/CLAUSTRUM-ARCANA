
using UnityEngine;
public class GridMoviment : MonoBehaviour
{
    public Transform SphereTarget;
    public float speed = 0.01f;
    public GameObject setas_ZM;
    public GameObject setas_Zm;
    public GameObject setas_XM;
    public GameObject setas_Xm;
    private string SetaClicada;
    public static bool brendaperto;
    
    void FixedUpdate()
    {
        Vector3 dir = SphereTarget.position - transform.position;
        transform.position += dir * speed * Time.deltaTime;
    }
    void Update()
    {       
        if(!brendaperto)
        return;

        if (Input.touchCount > 0)
             {
            Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                     if (Physics.Raycast(ray, out hit))
                {   
                    
                            SetaClicada = hit.collider.name;
                    // se o objeto colidir com o um objeto movivel, ele vai pegar o gameobject setas que esta nele mesmo destivado e liga-lo
                    
                        if (hit.collider != null && hit.collider.CompareTag("ObjetoMovivel"))
                        {
            
                            if(Precolisores.MovimentXM == true){
                                setas_XM.SetActive(true);
                            }
                             if(Precolisores.MovimentXm == true){
                                setas_Xm.SetActive(true);
                            }
                             if(Precolisores.MovimentZM == true){
                                setas_ZM.SetActive(true);
                            }
                             if(Precolisores.MovimentZm == true){
                                setas_Zm.SetActive(true);
                            }
                         
                            
                           
                            switch(SetaClicada)
                            {
                                case "Z+":
                                    SphereTarget.position += Vector3.forward/2;
                                    DesativarSetas();
                                    return;
                                case "Z-":
                                    SphereTarget.position += Vector3.back/2;
                                    DesativarSetas();
                                    return;
                                case "X+":
                                    SphereTarget.position += Vector3.right/2;
                                    DesativarSetas();

                                    return;
                                case "X-":
                                    SphereTarget.position += Vector3.left/2;
                                    DesativarSetas();
                                    return; 
                            }
                        }
                        else
                        {
                           DesativarSetas();
                        }
                    

                }
            }
        }
    }



    void DesativarSetas()
    {
        setas_XM.SetActive(false);
        setas_Xm.SetActive(false);
        setas_ZM.SetActive(false);
        setas_Zm.SetActive(false);
    }
}
