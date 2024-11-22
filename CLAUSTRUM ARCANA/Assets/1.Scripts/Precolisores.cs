using UnityEngine;

public class Precolisores : MonoBehaviour
{
    public bool MovimentZM = true;
    public bool MovimentZm = true;
    public bool MovimentXM = true;
    public bool MovimentXm = true;

    private GridMoviment associatedGridMoviment;

    void Start()
    {
        // Vincular ao GridMoviment correspondente
        associatedGridMoviment = GetComponentInParent<GridMoviment>();
        if (associatedGridMoviment == null)
        {
            Debug.LogError("GridMoviment não encontrado no objeto ou em seus pais.");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (associatedGridMoviment == null) return;

        string player = other.name;
        string testeDeDirecao = gameObject.name;

        switch (testeDeDirecao)
        {
            case "Z+p":
                MovimentZM = false;
                break;
            case "Z-p":
                MovimentZm = false;
                break;
            case "X+p":
                MovimentXM = false;
                break;
            case "X-p":
                MovimentXm = false;
                break;
        }

        if (player == "Brenda")
        {
            associatedGridMoviment.brendaperto = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (associatedGridMoviment == null) return;

        string player = other.name;
        string testeDeDirecao = gameObject.name;

        switch (testeDeDirecao)
        {
            case "Z+p":
                MovimentZM = true;
                break;
            case "Z-p":
                MovimentZm = true;
                break;
            case "X+p":
                MovimentXM = true;
                break;
            case "X-p":
                MovimentXm = true;
                break;
        }

        if (player == "Brenda")
        {
            associatedGridMoviment.brendaperto = false;
        }
    }
}
