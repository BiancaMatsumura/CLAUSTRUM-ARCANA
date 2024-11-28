
using UnityEngine;

public class cameraALternator : MonoBehaviour
{   
    public GameObject camera2;
    public GameObject camera3;
    public GameObject camera4;
    int medidor;
    
    void OnTriggerEnter()
    {
        medidor++;
    }
    void FixedUpdate()
    {
        switch(medidor)
        {
            case 1:
            camera2.SetActive(true);
            break;
            case 2:
            camera3.SetActive(true);
            break;
            case 3:
            camera4.SetActive(true);
            break;
        }
    }
}
