using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraESCAD : MonoBehaviour
{
    public GameObject CMTRES;
    void OnTriggerEnter()
    {
        CMTRES.SetActive(true);
    }
}
