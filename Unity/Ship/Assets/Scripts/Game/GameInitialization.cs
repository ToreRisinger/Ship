using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    
    void Awake()
    {
        
    }

    void Start()
    {
        ConnectionManager.GetInstance().ConnectToGameServer();
    }

}
