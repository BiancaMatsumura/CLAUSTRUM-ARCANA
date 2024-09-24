using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    public GameObject pointPrefab; // Prefab do ponto que será exibido
    public float distanceBetweenPoints = 1f; // Distância entre os pontos (1 metro)
    public float maxDistance = 2f; // Distância máxima (2 metros)
    public float moveSpeed = 2f; // Velocidade de movimento do bloco

    private GameObject[] movementPoints; // Guardar os pontos gerados
    private Vector3 targetPosition; // Posição alvo para o movimento
    private bool isMoving = false; // Indica se o bloco está se movendo

    void Update()
    {
        // Detecta clique do mouse
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == this.gameObject) // Verifica se o objeto foi clicado
                {
                    ShowMovementPoints(hit.point); // Chama a função para mostrar os pontos
                }
                else if (hit.collider.CompareTag("MovePoint")) // Verifica se o clique foi em um ponto de movimento
                {
                    MoveToPoint(hit.collider.gameObject.transform.position); // Move o bloco para o ponto clicado
                }
            }
        }

        // Movimento suave do bloco
        if (isMoving)
        {
            MoveBlockSmoothly();
        }
    }

    // Exibir apenas os pontos em frente, atrás, esquerda e direita
    void ShowMovementPoints(Vector3 hitPosition)
    {
        ClearOldPoints(); // Limpar pontos anteriores

        Vector3[] directions = {
            new Vector3(1, 0, 0), // Direita
            new Vector3(-1, 0, 0), // Esquerda
            new Vector3(0, 0, 1), // Frente
            new Vector3(0, 0, -1) // Atrás
        };

        foreach (Vector3 direction in directions)
        {
            for (float i = 1f; i <= maxDistance; i += distanceBetweenPoints)
            {
                Vector3 pointPosition = hitPosition + direction * i;
                GameObject point = Instantiate(pointPrefab, pointPosition, Quaternion.identity);
                point.tag = "MovePoint"; // Tag para identificar os pontos clicáveis
            }
        }
    }

    // Função para mover o bloco para o ponto selecion
    void MoveToPoint(Vector3 target)
    {
        targetPosition = new Vector3(target.x, transform.position.y, target.z); // Define a nova posição alvo
        isMoving = true; // Indica que o bloco deve se mover
    }

    // Função para mover o bloco suavemente
    void MoveBlockSmoothly()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Verifica se o bloco chegou perto o suficiente da posição alvo
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition; // Ajusta a posição final
            isMoving = false; // Para o movimento
            ClearOldPoints(); // Remover os pontos após a movimentação
        }
    }

    // Limpar pontos de movimentação antigos
    void ClearOldPoints()
    {
        GameObject[] oldPoints = GameObject.FindGameObjectsWithTag("MovePoint");

        foreach (GameObject point in oldPoints)
        {
            Destroy(point); // Remover os pontos da cena
        }
    }
}
