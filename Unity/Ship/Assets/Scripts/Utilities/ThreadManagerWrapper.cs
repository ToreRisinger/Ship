using Ship.Shared.Utilities;
using UnityEngine;

public class ThreadManagerWrapper : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Log.debug("ThreadManagerWrapper.Awake");
        ThreadManager.init();
    }

    // Update is called once per frame
    void Update()
    {
        ThreadManager.UpdateMain();
    }
}
