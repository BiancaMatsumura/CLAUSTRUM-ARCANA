
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DesenhoScript : MonoBehaviour
{
    private Texture2D texture;
    public RawImage img;
    void OnEnable()
    {
        texture = new Texture2D(50, 50 , TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Bilinear;
        texture.alphaIsTransparency = true;
        
        img.texture = texture;
        for (int i = 0; i < 50; i++)
                {
                  for(int I = 0; I < 50; I++)
                  {
                    texture.SetPixel(I, i, new Color(0, 0 , 0 , 0));
                  }
                  
                }
        
    } 

   
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;

            Vector2 pos = touchPosition;
            pos.x = pos.x / Screen.width;
            pos.y = pos.y / Screen.height;

            if (touch.phase == TouchPhase.Moved)
            {
            
                texture.SetPixel((int)(pos.x*50),(int)(pos.y *50), Color.magenta);

                texture.Apply();
                Debug.Log ($"teste:{pos}");
            }
            if (touch.phase == TouchPhase.Ended)
            {
                for (int i = 0; i < 50; i++)
                {
                  for(int I = 0; I < 50; I++)
                  {
                    texture.SetPixel(I, i, new Color(0, 0 , 0 , 0));
                  }
                  
                }
            }
    
    }
}}
