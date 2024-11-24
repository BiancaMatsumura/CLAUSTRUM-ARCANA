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
    public bool brendaperto; // Variável agora é independente
    public AudioSource sons;

    private Precolisores precolisores; // Referência para o Precolisores associado
    public float checkDistance = 0.55f; // Distância para verificar colisão (ajuste conforme necessário)

    private Color originalColor; // Para restaurar a cor original da seta

    void Start()
    {
        // Procurar o componente Precolisores no mesmo GameObject ou em um filho.
        precolisores = GetComponentInChildren<Precolisores>();
        if (precolisores == null)
        {
            Debug.LogError("Precolisores não encontrado no objeto ou em seus filhos.");
        }

        // Armazenar a cor original de qualquer seta (assume que todas compartilham o mesmo material inicial)
        if (setas_XM != null)
            originalColor = setas_XM.GetComponent<Renderer>().material.color;
    }

    void FixedUpdate()
    {
        Vector3 dir = SphereTarget.position - transform.position;
        transform.position += dir * speed * Time.deltaTime;
    }

    void Update()
    {
        if (!brendaperto)
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

                    if (hit.collider != null && hit.collider.CompareTag("ObjetoMovivel"))
                    {
                        if (precolisores != null)
                        {
                            if (precolisores.MovimentXM)
                                setas_XM.SetActive(true);
                            if (precolisores.MovimentXm)
                                setas_Xm.SetActive(true);
                            if (precolisores.MovimentZM)
                                setas_ZM.SetActive(true);
                            if (precolisores.MovimentZm)
                                setas_Zm.SetActive(true);
                        }

                        // Movimenta o alvo com base na seta clicada
                        switch (SetaClicada)
                        {
                            case "Z+":
                                if (!TryMove(Vector3.forward / 2, setas_ZM)) return;
                                break;
                            case "Z-":
                                if (!TryMove(Vector3.back / 2, setas_Zm)) return;
                                break;
                            case "X+":
                                if (!TryMove(Vector3.right / 2, setas_XM)) return;
                                break;
                            case "X-":
                                if (!TryMove(Vector3.left / 2, setas_Xm)) return;
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
        Vector3 startPosition = transform.position;

        // Raycast para verificar a colisão com objetos sólidos
        if (Physics.Raycast(startPosition, direction, out RaycastHit hitInfo, checkDistance))
        {
            if (!hitInfo.collider.isTrigger)
            {
                Debug.Log("Movimento bloqueado por um objeto: " + hitInfo.collider.name);

                // Acionar o efeito de piscar a seta
                StartCoroutine(PiscarSeta(seta));
                return false; // Bloqueia o movimento
            }
        }

        // Movimento permitido
        SphereTarget.position += direction;
        sons.Play();

        DesativarSetas();
        return true;
    }

    void DesativarSetas()
    {
        setas_XM.SetActive(false);
        setas_Xm.SetActive(false);
        setas_ZM.SetActive(false);
        setas_Zm.SetActive(false);
    }

    System.Collections.IEnumerator PiscarSeta(GameObject seta)
    {
        if (seta == null) yield break;

        Renderer renderer = seta.GetComponent<Renderer>();
        if (renderer == null) yield break;

        // Mudar para vermelho
        renderer.material.color = Color.red;

        // Aguarde um curto período
        yield return new WaitForSeconds(0.3f);

        // Restaurar a cor original
        renderer.material.color = originalColor;
    }
}
