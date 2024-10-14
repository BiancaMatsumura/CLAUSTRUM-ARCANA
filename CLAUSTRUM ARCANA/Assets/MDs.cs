using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class PasswordLockPatternSetup : MonoBehaviour
{
    private Canvas canvas;
    private GameObject passwordPanel;
    public GameObject cubePrefab; // Prefab do cubo a ser instanciado
    public List<Image> points = new List<Image>();
    public List<int> selectedPoints = new List<int>();
    private List<GameObject> instantiatedCubes = new List<GameObject>();

    private void Start()
    {
        CreateCanvas();
        CreatePasswordPanel();
        CreatePoints();

    }

    private void CreateCanvas()
    {
        canvas = new GameObject("Canvas").AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.gameObject.AddComponent<CanvasScaler>();
        canvas.gameObject.AddComponent<GraphicRaycaster>();
    }

    private void CreatePasswordPanel()
    {
        passwordPanel = new GameObject("PasswordPanel");
        passwordPanel.transform.SetParent(canvas.transform);
        RectTransform panelRect = passwordPanel.AddComponent<RectTransform>();
        panelRect.sizeDelta = new Vector2(300, 300);
        panelRect.anchoredPosition = Vector2.zero;

        Image panelImage = passwordPanel.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.5f); // Semi-transparente

        passwordPanel.AddComponent<PasswordDraw>().Initialize(points, this);
    }

    private void CreatePoints()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject point = new GameObject($"Point{i}");
            point.transform.SetParent(passwordPanel.transform);
            Image pointImage = point.AddComponent<Image>();
            pointImage.color = Color.white; // Cor do ponto

            RectTransform pointRect = point.GetComponent<RectTransform>();
            pointRect.sizeDelta = new Vector2(40, 40); // Tamanho do ponto
            pointRect.anchoredPosition = new Vector2((i % 3) * 100 - 100, (i / 3) * -100 + 100); // Posições em grade

            points.Add(pointImage);
        }
    }

    private void SetupCinemachine()
    {
        // Cria uma câmera do Cinemachine
        var vcamGo = new GameObject("CinemachineVirtualCamera");
        var virtualCamera = vcamGo.AddComponent<Cinemachine.CinemachineVirtualCamera>();
        var camera = Camera.main;

        // Define a câmera principal como a câmera do Cinemachine
        if (camera != null)
        {
            virtualCamera.Follow = camera.transform;
            virtualCamera.LookAt = passwordPanel.transform; // Foca no painel da senha
        }

        // Posiciona a câmera
        camera.transform.position = new Vector3(0, 0, -10); // Ajuste a posição conforme necessário
    }

    public void InstantiateCube(Vector2 position)
    {
        GameObject newCube = Instantiate(cubePrefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
        instantiatedCubes.Add(newCube);
    }

    public void ClearCubes()
    {
        foreach (var cube in instantiatedCubes)
        {
            Destroy(cube);
        }
        instantiatedCubes.Clear();
    }

    public bool IsPasswordCorrect(List<int> drawnPoints)
    {
        // Defina a senha correta (exemplo: [0, 1, 2])
        List<int> correctPassword = new List<int> { 0, 1, 2 }; // Altere conforme necessário
        return drawnPoints.Count == correctPassword.Count && !drawnPoints.Except(correctPassword).Any();
    }
}

public class PasswordDraw : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private List<Image> points;
    private PasswordLockPatternSetup setup;
    private Vector2 lastPosition;

    public void Initialize(List<Image> pointImages, PasswordLockPatternSetup passwordSetup)
    {
        points = pointImages;
        setup = passwordSetup;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPos);

        // Cria um novo cubo se o movimento do mouse for significativo
        if ((localPos - lastPosition).magnitude > 30) // Ajuste o valor para controlar a sensibilidade
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(points[i].GetComponent<RectTransform>(), eventData.position))
                {
                    if (!setup.selectedPoints.Contains(i))
                    {
                        setup.selectedPoints.Add(i);
                    }
                    setup.InstantiateCube(localPos); // Instancia um cubo na posição do toque
                    lastPosition = localPos; // Atualiza a posição anterior
                    break;
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (setup.IsPasswordCorrect(setup.selectedPoints))
        {
            Debug.Log("Senha correta!");
        }
        else
        {
            Debug.Log("Senha incorreta!");
        }

        // Limpa a seleção e os cubos
        setup.selectedPoints.Clear();
        setup.ClearCubes();
        lastPosition = Vector2.zero; // Reseta a última posição
    }
}
