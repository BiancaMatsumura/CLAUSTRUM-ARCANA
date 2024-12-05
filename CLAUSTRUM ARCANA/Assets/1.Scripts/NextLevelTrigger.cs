using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    private Scene cenaAtiva;

    void Start()
    {
        cenaAtiva = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(cenaAtiva.name == "Tutorial")
        {
            if (other.CompareTag("Player") && gameObject.CompareTag("Next"))
            {
                SceneManager.LoadScene("Fase1");
            }
        }
        else if (cenaAtiva.name == "Fase1")
        {
            if (other.CompareTag("Player") && gameObject.CompareTag("Next"))
            {
                SceneManager.LoadScene("Vitoria");
            }
        }
        

    }
}
