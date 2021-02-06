using Ship.Game;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public GameManager gameManager;

    void LateUpdate()
    {
        Player player = gameManager.GetLocalPlayer();
        if (player != null && player.character != null)
        {
            Vector3 playerPosition = player.character.transform.position;
            transform.position = new Vector3(playerPosition.x, playerPosition.y, transform.position.z);
        }
    }
}
