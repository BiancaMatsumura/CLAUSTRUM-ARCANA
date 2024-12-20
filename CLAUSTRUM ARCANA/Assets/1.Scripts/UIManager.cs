using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    private InteractableItem currentItem;
    public LevelLoader levelLoader;
    public AudioSource folhason;

    public GameObject panel;
    public GameObject panelRaw; // Arraste o painel que você deseja abrir aqui
    private bool isPanelOpen = false; // Estado inicial do painel

    public GameObject pausePanel;
    private bool isPaused = false;

        public void SetLanguage(int index)
    { 

        if (index == 0) 
        {
            DialogueController.english = true;
            DialogueController.portugues = false;
            
            
        }
        else if (index == 1) 
        {
            DialogueController.portugues = true;
            DialogueController.english = false;
          
        }
    }

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
        panelRaw.SetActive(isPanelOpen);
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

    public void Restart(string sceneName)
    {
        if (isPaused)
        {
            isPaused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            levelLoader.Transition(sceneName);

        }
    }

    public void Option() { }
    public void Credits() { }
    public void Quit() { Application.Quit(); }
}
