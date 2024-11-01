using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DesenhoScript : MonoBehaviour
{
    private Texture2D texture;
    public RawImage img;
    private Vector2? lastTouchPosition = null;
    private Color drawColor = new Color(54f / 255f, 112f / 255f, 96f / 255f); // Cor atualizada para 367060

    void OnEnable()
    {
        texture = new Texture2D(50, 50, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Bilinear;
        texture.alphaIsTransparency = true;

        img.texture = texture;

        // Define a textura como transparente
        ClearTexture();
        texture.Apply();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;

            // Converte a posição do toque para o espaço de coordenadas da textura
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                img.rectTransform, touchPosition, null, out Vector2 localPoint);

            // Ajusta a posição para estar dentro da textura
            float x = Mathf.Clamp((localPoint.x + img.rectTransform.rect.width / 2) / img.rectTransform.rect.width * 50, 0, 49);
            float y = Mathf.Clamp((localPoint.y + img.rectTransform.rect.height / 2) / img.rectTransform.rect.height * 50, 0, 49);

            Vector2 pixelPos = new Vector2(x, y);

            if (touch.phase == TouchPhase.Moved)
            {
                if (lastTouchPosition.HasValue)
                {
                    DrawLine((int)lastTouchPosition.Value.x, (int)lastTouchPosition.Value.y, (int)pixelPos.x, (int)pixelPos.y, drawColor);
                }

                lastTouchPosition = pixelPos;
                texture.Apply();
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Limpa a textura ao finalizar o toque
                ClearTexture();
                texture.Apply();
                lastTouchPosition = null;
            }
        }
    }

    // Função para desenhar uma linha entre dois pontos
    void DrawLine(int x0, int y0, int x1, int y1, Color color)
    {
        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            texture.SetPixel(x0, y0, color);

            if (x0 == x1 && y0 == y1) break;
            int e2 = err * 2;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 < dx) { err += dx; y0 += sy; }
        }
    }

    // Função para limpar a textura
    void ClearTexture()
    {
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                texture.SetPixel(j, i, new Color(0, 0, 0, 0));
            }
        }
    }
}
