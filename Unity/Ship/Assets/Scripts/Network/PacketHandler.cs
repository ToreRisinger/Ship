using Ship.Network;
using Ship.Shared.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketHandler : MonoBehaviour
{
    public static PacketHandler instance;
    public delegate void PacketHandlerFunction(Packet _packet);
    public static Dictionary<int, PacketHandlerFunction> packetHandlers = new Dictionary<int, PacketHandlerFunction>()
    {
         //{ (int)ServerPackets.welcome, ClientHandle.Welcome},
         //{ (int)ServerPackets.initialize, ClientHandle.Initialize},
         // { (int)ServerPackets.spawnPlayer, ClientHandle.SpawnPlayer},
         //  { (int)ServerPackets.gameState, ClientHandle.GameState}
    };

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onPacketReceived(int packetId, Packet packet)
    {
        //TODO add check
        packetHandlers[packetId](packet);
    }

    //Handle functions

}
