using Ship.Shared.Utilities;
using UnityEngine;

public class ThreadManagerWrapper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ThreadManager.UpdateMain();
    }
}
