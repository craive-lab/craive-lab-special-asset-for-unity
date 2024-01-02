using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using UnityEngine;

namespace EMPACResearch.Core.Audio
{
    [RequireComponent(typeof(OSC))]
    public class RoomAudioNetworkManager : MonoBehaviour
    {
        private void Awake()
        {
            if (GetComponent<OSC>().outIP == null)
            {
                string ip = "127.0.0.1";
            }
        }
    }
}
