using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private InteractableItem currentItem;
    public LevelLoader levelLoader;
    public AudioSource folhason;

    public GameObject panel; // Arraste o painel que você deseja abrir aqui
    private bool isPanelOpen = false; // Estado inicial do painel

    public GameObject pausePanel;
    private bool isPaused = false;
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
        folhason.Play();
    }

    public void Pause()
    {
        if (!isPaused) 
        {
            isPaused = true; 
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        if (isPaused) 
        {
            isPaused = false; 
            pausePanel.SetActive(false); 
            Time.timeScale = 1f;
        }
    }

    public void Option() { }
    public void Credits() { }
    public void Quit() { Application.Quit(); }
}
