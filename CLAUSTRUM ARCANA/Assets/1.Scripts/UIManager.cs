using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private InteractableItem currentItem;
    public LevelLoader levelLoader;

    public GameObject panel; // Arraste o painel que você deseja abrir aqui
    private bool isPanelOpen = false; // Estado inicial do painel

    public void SetCurrentItem(InteractableItem item)
    {
        currentItem = item;
    }

    public void OnRotateButtonPressed()
    {
        if (currentItem != null)
        {
            currentItem.Rotate();
        }
    }

    public void LoadScene(string sceneName)
    {
        levelLoader.Transition(sceneName);
    }

    // Função para abrir/fechar o painel ao clicar no botão
    public void TogglePanel()
    {
        isPanelOpen = !isPanelOpen;
        panel.SetActive(isPanelOpen);
    }

    public void Option() { }
    public void Credits() { }
    public void Quit() { Application.Quit(); }
}
