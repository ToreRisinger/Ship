using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    
    void Awake()
    {
        ConnectionManager.GetInstance().ConnectToGameServer();
    }

    void OnApplicationQuit()
    {
        ConnectionManager.GetInstance().DisconnectFromGameServer();
    }

    void OnDestroy()
    {
        ConnectionManager.GetInstance().DisconnectFromGameServer();
    }

}
