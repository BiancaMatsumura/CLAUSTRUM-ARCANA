using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class PasswordLockPatternSetup : MonoBehaviour
{
    public Canvas canvas;  // Agora o Canvas será referenciado diretamente no Inspector
    public GameObject passwordPanel;  // Referenciado diretamente no Inspector
    public List<Image> points = new List<Image>();  // Adicione os pontos manualmente no Inspector
    public List<int> selectedPoints = new List<int>();
    private List<GameObject> instantiatedCubes = new List<GameObject>();

    public GameObject cubePrefab; // Prefab do cubo a ser instanciado

    // Referência ao objeto que será rotacionado
    public GameObject objectToRotate;

    // Variáveis para controle da rotação gradual
    private bool shouldRotate = false;
    private Quaternion targetRotation;
    private float rotationSpeed = 100f;  // Velocidade de rotação
    
    public AudioSource portaabrir;

    public DialogueController dialogueController;
    public UIManager uIManager;

    [SerializeField] private TextMeshProUGUI tentativasText;  
    [SerializeField] private Button pergaminhoButton;  

    private int tentativasRestantes = 3;

    private void Start()
    {   
        passwordPanel.AddComponent<PasswordDraw>().Initialize(points, this);
        PasswordDraw passwordDraw = passwordPanel.AddComponent<PasswordDraw>();
        UpdateTentativasText();
    }

    public void UpdateTentativasText()
    {
        tentativasText.text = $"{tentativasRestantes}";  
        if (tentativasRestantes <= 0)
        {
            uIManager.TogglePanel();
            pergaminhoButton.interactable = false;  
        }
    }
    public void DecrementarTentativas()
    {
        tentativasRestantes--;
        UpdateTentativasText();
    }

    private void Update()
    {
        // Verifica se deve rotacionar o objeto gradualmente
        if (shouldRotate && objectToRotate != null)
        {
            // Rotaciona gradualmente até o ângulo desejado
            objectToRotate.transform.rotation = Quaternion.RotateTowards(
                objectToRotate.transform.rotation, 
                targetRotation, 
                rotationSpeed * Time.deltaTime
            );

            // Para de rotacionar quando atingir a rotação desejada
            if (Quaternion.Angle(objectToRotate.transform.rotation, targetRotation) < 0.1f)
            {
                shouldRotate = false;
                Debug.Log("Rotação concluída.");
                uIManager.TogglePanel();
                dialogueController.Dialogue04();
                
            }
        }
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
        if (drawnPoints.Count == correctPassword.Count && !drawnPoints.Except(correctPassword).Any())
        {
            StartRotation(); // Inicia a rotação gradual
            return true;
        }
        return false;
    }

    // Função para iniciar a rotação gradual
    private void StartRotation()
    {
        if (objectToRotate != null)
        {
            targetRotation = objectToRotate.transform.rotation * Quaternion.Euler(90f, 0f, 0f);
            shouldRotate = true;
            Debug.Log("Iniciando rotação gradual.");
            portaabrir.Play();
            
        }
        else
        {
            Debug.LogWarning("Nenhum objeto foi atribuído para rotacionar.");
        }
    }
}

public class PasswordDraw : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private List<Image> points;
    private PasswordLockPatternSetup setup;
    private Vector2 lastPosition;

    [SerializeField] public TextMeshPro tentativasText;

    private int tentativasRestantes = 3;

    [SerializeField] public Button pergaminhoButton;

    public void Initialize(List<Image> pointImages, PasswordLockPatternSetup passwordSetup)
    {
        points = pointImages;
        setup = passwordSetup;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPos);

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
                    setup.InstantiateCube(localPos);
                    lastPosition = localPos;
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
            gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Senha incorreta!");
            setup.DecrementarTentativas();
            
        }

        setup.selectedPoints.Clear();
        setup.ClearCubes();
        lastPosition = Vector2.zero;
    }


}
