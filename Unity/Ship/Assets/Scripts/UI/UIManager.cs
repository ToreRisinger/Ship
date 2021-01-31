using Ship.Game.Event;
using Ship.Shared.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public InputField usernameField;

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

        Log.setupLogger(ELogLevel.DEBUG, false, print);
    }

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        usernameField.interactable = false;
        SceneManager.LoadScene("Game");
    }

    private void print(string msg)
    {
        Debug.Log(msg);
    }

}
