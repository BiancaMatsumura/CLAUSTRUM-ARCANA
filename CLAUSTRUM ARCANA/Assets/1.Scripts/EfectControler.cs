
using UnityEngine;
using UnityEngine.Rendering;

public class EfectControler : MonoBehaviour
{
    Volume v;
    public float efectVelocity;
    void Start()
    {
        v = GetComponent<Volume>();
        

    }
      public void ActiveEFECT()
    {
        v.weight= 1;
       InvokeRepeating("ActiveEFECT2", efectVelocity, efectVelocity);
    }
  
    public void ActiveEFECT2()
    {
        if (v.weight > 0.0)
        {
            v.weight -= 0.1f;
            
        }
        else 
        {
            v.weight = 0;
            CancelInvoke();
        }
    }
}
