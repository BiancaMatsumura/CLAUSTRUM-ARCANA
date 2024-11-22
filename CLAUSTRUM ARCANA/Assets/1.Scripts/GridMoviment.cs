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

    void Start()
    {
        // Procurar o componente Precolisores no mesmo GameObject ou em um filho.
        precolisores = GetComponentInChildren<Precolisores>();
        if (precolisores == null)
        {
            Debug.LogError("Precolisores não encontrado no objeto ou em seus filhos.");
        }
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
                        // Usa as variáveis de Precolisores específicas da instância
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
                                TryMove(Vector3.forward / 2);
                                return;
                            case "Z-":
                                TryMove(Vector3.back / 2);
                                return;
                            case "X+":
                                TryMove(Vector3.right / 2);
                                return;
                            case "X-":
                                TryMove(Vector3.left / 2);
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

    void TryMove(Vector3 direction)
    {
        // Verifica se há colisores na direção antes de mover
        if (!Physics.Raycast(transform.position, direction, checkDistance))
        {
            SphereTarget.position += direction;
            sons.Play();
        }
        else
        {
            Debug.Log("Movimento bloqueado por um objeto na direção " + direction);
        }

        DesativarSetas();
    }

    void DesativarSetas()
    {
        setas_XM.SetActive(false);
        setas_Xm.SetActive(false);
        setas_ZM.SetActive(false);
        setas_Zm.SetActive(false);
    }
}
