using Ship.Shared.Utilities;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public static ConnectionManager instance;
    private Client client;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Log.error("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Start()
    {
        client = new Client();
    }

    private void OnApplicationQuit()
    {
        client.Disconnect();
    }

    public void ConnectToGameServer()
    {
        client.ConnectToGameServer();
    }


}
