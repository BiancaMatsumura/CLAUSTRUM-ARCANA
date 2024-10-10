using UnityEngine;


public class Precolisores : MonoBehaviour
{
    public static bool MovimentZM = true;
    public static bool MovimentZm = true;
    public static bool MovimentXM= true;
    public static bool MovimentXm = true;
    void OnTriggerStay(Collider other)
    {
        string player = other.name;
        string TesteDeDireção = gameObject.name;
        switch(TesteDeDireção)
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
        if(player == "Brenda")
        {
            GridMoviment.brendaperto = true;
        }
    }
     void OnTriggerExit(Collider other)
     {
        string player = other.name;
         string TesteDeDireção = gameObject.name;
        switch(TesteDeDireção)
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
         if(player == "Brenda")
        {
            GridMoviment.brendaperto = false;
        }
     }
     
}
