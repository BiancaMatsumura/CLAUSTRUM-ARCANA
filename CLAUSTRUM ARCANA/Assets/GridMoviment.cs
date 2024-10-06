
using UnityEngine;
public class GridMoviment : MonoBehaviour
{
    public Transform SphereTarget;
    public float speed = 0.01f;
    public GameObject setas_ZM;
    public GameObject setas_Zm;
    public GameObject setas_XM;
    public GameObject setas_Xm;
    public string SetaClicada;
    public bool movendo;
    
    void FixedUpdate()
    {
        Vector3 dir = SphereTarget.position - transform.position;
        transform.position += dir * speed * Time.deltaTime;
        movendo = true;
    }
    void Update()
    {       
        if(movendo == false)
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
                            Debug.Log($"{SetaClicada}");

                            setas_XM.SetActive(true);
                            setas_Xm.SetActive(true);
                            setas_ZM.SetActive(true);
                            setas_Zm.SetActive(true);

            
                            string ZM = setas_ZM.name;
                            string Zm = setas_Zm.name;
                            string XM = setas_XM.name;
                            string Xm = setas_Xm.name; 

                            
                            
                            switch(SetaClicada)
                            {
                                case "Z+":
                                    SphereTarget.position += Vector3.forward/2;
                                    movendo = false;
                                    break;
                                case "Z-":
                                    SphereTarget.position += Vector3.back/2;
                                    movendo = false;
                                    break;
                                case "X+":
                                    SphereTarget.position += Vector3.right/2;
                                    movendo = false;
                                    break;
                                case "X-":
                                    SphereTarget.position += Vector3.left/2;
                                    movendo = false;
                                    break;
                            
                            }

                        }
                        else
                        {
                            setas_XM.SetActive(false);
                            setas_Xm.SetActive(false);
                            setas_ZM.SetActive(false);
                            setas_Zm.SetActive(false);
                        }
                    

                }
            }
        }
    }


}
