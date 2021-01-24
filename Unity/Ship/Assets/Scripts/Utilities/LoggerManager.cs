using UnityEngine;
using Ship.Shared.Utilities;

public class LoggerManager : MonoBehaviour
{
    void Start()
    {
        Log.setupLogger(ELogLevel.INFO, false, print);
    }

    private void print(string msg)
    {
        Debug.Log(msg);
    }
}
