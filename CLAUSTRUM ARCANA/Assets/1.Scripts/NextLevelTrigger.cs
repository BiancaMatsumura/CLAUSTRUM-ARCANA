using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o player colidiu com o cubo que possui a tag "Next"
        if (other.CompareTag("Player") && gameObject.CompareTag("Next"))
        {
            Debug.Log("Encostou");
            // Carrega a próxima cena chamada "Fase1"
            SceneManager.LoadScene("Fase1");
        }
    }
}
