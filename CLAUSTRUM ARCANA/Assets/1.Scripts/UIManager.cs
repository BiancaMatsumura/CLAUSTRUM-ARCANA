using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private InteractableItem currentItem;


    public LevelLoader levelLoader;

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



    public void Option()
    {

    }

    public void Credits()
    {

    }


    public void Quit()
    {
        Application.Quit();
    }

}