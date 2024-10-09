using System.Collections.Generic;
using UnityEngine;

public class UnlockPatternHandler : MonoBehaviour
{
    public LineRenderer lineRenderer; // Arraste o LineRenderer aqui no Inspector
    public List<Transform> points; // Arraste aqui os Transform dos botões/pontos
    public float pointRadius = 50f; // Ajuste a sensibilidade do toque
    private List<Transform> selectedPoints = new List<Transform>();
    private bool isDrawing = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position); // Converte para coordenadas de mundo

            Debug.Log("Touch detected at position: " + touchPosition); // Log para verificar a posição do toque

            if (touch.phase == TouchPhase.Began)
            {
                isDrawing = true;
                lineRenderer.positionCount = 0; // Reinicia a contagem de posições
                selectedPoints.Clear(); // Limpa a lista de pontos selecionados
                Debug.Log("Touch started - Begin drawing.");
            }
            else if (touch.phase == TouchPhase.Moved && isDrawing)
            {
                foreach (Transform point in points)
                {
                    // Verifica se o toque está dentro do raio do ponto
                    if (Vector2.Distance(point.position, touchPosition) <= pointRadius && !selectedPoints.Contains(point))
                    {
                        selectedPoints.Add(point); // Adiciona o ponto à lista
                        lineRenderer.positionCount = selectedPoints.Count; // Atualiza a contagem de posições
                        lineRenderer.SetPosition(selectedPoints.Count - 1, point.position); // Define a posição do LineRenderer

                        Debug.Log("Point detected: " + point.name + " at position: " + point.position); // Log para identificar o ponto detectado
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDrawing = false; // Para de desenhar ao soltar o toque
                Debug.Log("Touch ended - Stop drawing.");
                ValidatePattern(); // Lógica para validar o padrão
            }
        }
    }

    private void ValidatePattern()
    {
        // Implementar lógica para validar o padrão (exemplo: sequência correta)
        Debug.Log("Pattern validation triggered.");
    }
}
