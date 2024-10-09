using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingHandler : MonoBehaviour
{
    public LineRenderer lineRenderer; // Componente para desenhar a linha
    public Camera uiCamera; // A câmera da UI, se for uma cena 2D

    private List<Vector2> points = new List<Vector2>(); // Lista de pontos traçados pelo toque

    void Update()
    {
        // Verifica se há toques na tela
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Pega o primeiro toque

            if (touch.phase == TouchPhase.Began)
            {
                // Inicia o desenho ao tocar
                points.Clear();
                lineRenderer.positionCount = 0;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // Continua desenhando conforme o jogador arrasta o dedo
                Vector2 touchPos = uiCamera.ScreenToWorldPoint(touch.position);

                if (points.Count == 0 || Vector2.Distance(points[points.Count - 1], touchPos) > 0.1f)
                {
                    points.Add(touchPos);
                    lineRenderer.positionCount = points.Count;
                    lineRenderer.SetPosition(points.Count - 1, touchPos);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Finaliza o desenho
                ValidateDrawing(points); // Chama função para verificar o desenho
            }
        }
    }

    void ValidateDrawing(List<Vector2> drawnPoints)
    {
        // Lógica para comparar o desenho com um padrão pré-definido
        // Aqui você pode implementar a lógica para verificar se o desenho está correto
        Debug.Log("Desenho finalizado com " + drawnPoints.Count + " pontos.");
    }
}
