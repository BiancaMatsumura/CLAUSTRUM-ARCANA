
using UnityEngine;

public class GridMoviment : MonoBehaviour
{
   
    [Header ("Setas")]
    public GameObject setas_ZM;
    public GameObject setas_Zm;
    public GameObject setas_XM;
    public GameObject setas_Xm;
    [Header ("AnimacaoSetas")]
    public Animation animXM;
    public Animation animXm;
    public Animation animZM;
    public Animation animZm;
    [Header ("Outros")]
    public float speed = 0.10f;
    private string SetaClicada;
    public bool brendaperto; // Variável agora é independente
    public AudioSource sons;
    public Transform Raycastgameobject;
    public GameObject SphereTarget; //gameobject do alvo do movel
    private GameObject instaciaprefab;
    public float checkDistance = 0.55f; // Distância para verificar colisão (ajuste conforme necessário)


    void FixedUpdate()
    {   
        if(instaciaprefab != null){
        Vector3 dir = instaciaprefab.transform.position - transform.position;
        transform.position += dir * speed * Time.deltaTime;
         DestroyPrefab(5f);
    }
    }

    void OnTriggerStay(Collider other)
    {   
        Debug.Log("colidiu");
        if(other.name == "Brenda")
        {
            brendaperto = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.name == "Brenda")
        {
            brendaperto = false;
            DesativarSetas();
        }
    }
    void Update()
    {
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
                    Debug.Log($"{SetaClicada}");
                    if (hit.collider != null && hit.collider.CompareTag("ObjetoMovivel"))
                    {
                        if (!brendaperto)
                        return;

                                Instanciador();
                        if(instaciaprefab.name == gameObject.name) 
                        {  
                                setas_XM.SetActive(true);
                            
                                setas_Xm.SetActive(true);
                          
                                setas_ZM.SetActive(true);

                                setas_Zm.SetActive(true);
                        }

                        // Movimenta o alvo com base na seta clicada
                        switch (SetaClicada)
                        {
                            case "Z+":
                                if (!TryMove(Vector3.forward / 2, setas_XM))
                                return;
                            break;
                            case "Z-":
                                if (!TryMove(Vector3.back / 2, setas_Xm))
                                return;
                            break;
                            case "X+":
                                if (!TryMove(Vector3.right / 2, setas_ZM))
                                return;
                            break;
                            case "X-":
                                if (!TryMove(Vector3.left / 2, setas_Zm))
                                return;
                            break;
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

    bool TryMove(Vector3 direction, GameObject seta)
    {
        Vector3 startPosition = Raycastgameobject.position;
        animXM.clip.legacy = true;
        animXm.clip.legacy = true;
        animZm.clip.legacy = true;
        animZM.clip.legacy = true;

        // Raycast para verificar a colisão com objetos sólidos
        if (Physics.Raycast(startPosition, direction, out RaycastHit hitInfo, checkDistance))
        {
            if (!hitInfo.collider.isTrigger)
            {
                Debug.Log("Movimento bloqueado por um objeto: " + hitInfo.collider.name);
                 switch (seta.name)
                        {
                            case "Z+":
                                
                                animXM.Play();
                            break;
                            case "Z-":
                                animXm.Play();
                            break;
                            case "X+":
                                animZM.Play();
                            break;
                            case "X-":
                                animZm.Play();
                            break;
                        }
                DestroyPrefab(0f);
                return false; // Bloqueia o movimento
                
            
            }
        }

        // Movimento permitido
        instaciaprefab.transform.position += direction;
        sons.Play();
        
        DesativarSetas();
        DestroyPrefab(3f);
        return true;
    }

    void DesativarSetas()
    {
        setas_XM.SetActive(false);
        setas_Xm.SetActive(false);
        setas_ZM.SetActive(false);
        setas_Zm.SetActive(false);
    }

  void DestroyPrefab(float segundos)
    {
        if (instaciaprefab != null)
        {
            // Destroi a instância do prefab
            Destroy(instaciaprefab, segundos);
            Debug.Log("Prefab destruído!");
        }
    }
    void Instanciador()
    {
        instaciaprefab = Instantiate(SphereTarget, transform.position, transform.rotation);
        instaciaprefab.name = this.gameObject.name ;
    }
}


