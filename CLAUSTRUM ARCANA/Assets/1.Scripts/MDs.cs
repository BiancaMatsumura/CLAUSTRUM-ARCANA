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
    [HideInInspector]
    public List<int> selectedPoints = new List<int>();
    private List<GameObject> instantiatedCubes = new List<GameObject>();

    //public List<int> correctPassword = new List<int> { 0, 1, 2 };

    public GameObject cubePrefab; // Prefab do cubo a ser instanciado

    // Referência ao objeto que será rotacionado
    public List<GameObject> objectsToRotate; // Lista de objetos que serão rotacionados
    private int currentObjectIndex = 0; // Índice para saber qual objeto rotacionar


    // Variáveis para controle da rotação gradual
    private bool shouldRotate = false;
    private Quaternion targetRotation;
    private float rotationSpeed = 100f;  // Velocidade de rotação
    
    public AudioSource portaabrir;

    public DialogueController dialogueController;
    public UIManager uIManager;

    [SerializeField] private TextMeshProUGUI tentativasText;  
    [SerializeField] private Button pergaminhoButton;  

    public Passwords passwords;

    private int tentativasRestantes = 5;

    private void Start()
    {   

        passwordPanel.AddComponent<PasswordDraw>().Initialize(points, this);
        PasswordDraw passwordDraw = passwordPanel.AddComponent<PasswordDraw>();
        UpdateTentativasText();
        selectedPoints.Clear();
    }

    public void UpdateTentativasText()
    {
        tentativasText.text = $"{tentativasRestantes}";  
        if (tentativasRestantes <= 0)
        {
            uIManager.TogglePanel();
            pergaminhoButton.interactable = false;
            dialogueController.StartDialogue(4);
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
    if (shouldRotate)
    {
        int currentPasswordIndex = passwords.GetCurrentPasswordIndex();  // Índice da senha atual
        if (objectsToRotate.Count > currentPasswordIndex)
        {
            GameObject objectToRotate = objectsToRotate[currentPasswordIndex];

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
                Debug.Log("Rotação concluída do objeto " + currentPasswordIndex);

                // Avança para a próxima senha
                passwords.AdvanceToNextPassword();

                // Verifica se há mais senhas a serem resolvidas
                if (passwords.HasMorePasswords())
                {
                    Debug.Log("Proxima senha.");
                }
                else
                {
                    Debug.Log("Todas as senhas foram resolvidas!");
                    uIManager.TogglePanel();  // Exemplo de ação após terminar todas as rotações
                    dialogueController.StartDialogue(3);  // Exemplo de diálogo
                }
            }
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
    List<int> currentPassword = passwords.GetCurrentPassword();

    if (currentPassword != null &&
        drawnPoints.Count == currentPassword.Count &&
        !drawnPoints.Except(currentPassword).Any())
    {
        Debug.Log("Senha correta!");
        selectedPoints.Clear();
        StartRotation(); // Inicia a rotação do objeto correspondente à senha atual

        return true;
    }

    Debug.Log("Senha incorreta!");
    return false;
    }



    // Função para iniciar a rotação gradual
    private void StartRotation()
    {
    // A senha corrente determina qual objeto rotacionar
    int currentPasswordIndex = passwords.GetCurrentPasswordIndex();  // Método que retorna o índice da senha atual
    if (objectsToRotate.Count > currentPasswordIndex)
    {
        GameObject objectToRotate = objectsToRotate[currentPasswordIndex];

        if (objectToRotate != null)
        {
            targetRotation = objectToRotate.transform.rotation * Quaternion.Euler(0f, 0f, -90f);
            shouldRotate = true;
            Debug.Log("Iniciando rotação do objeto " + currentPasswordIndex);
            portaabrir.Play();  // Som de porta abrindo
        }
        else
        {
            Debug.LogWarning("Nenhum objeto foi atribuído para rotacionar.");
        }
    }
    else
    {
        Debug.LogWarning("Não há objetos suficientes na lista para rotacionar.");
    }
    }


}

public class PasswordDraw : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private List<Image> points;
    private PasswordLockPatternSetup setup;
    private Vector2 lastPosition;

    [SerializeField] public TextMeshPro tentativasText;

    //private int tentativasRestantes = 3;

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
            Debug.Log("Senha correta! OnEndDrag");
            gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Senha incorreta! OnEndDrag");
            setup.DecrementarTentativas();
            
        }

        setup.selectedPoints.Clear();
        setup.ClearCubes();
        lastPosition = Vector2.zero;
    }


}
