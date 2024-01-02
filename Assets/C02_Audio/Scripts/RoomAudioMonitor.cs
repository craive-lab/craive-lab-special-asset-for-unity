using UnityEngine;

namespace EMPACResearch.Core.Audio
{
    [RequireComponent(typeof(OSC))] 
    public class RoomAudioMonitor : MonoBehaviour
    {

        private void Awake()
        {
            if (GetComponent<OSC>().outIP == "127.0.0.1")
            {
                Debug.LogWarning("Currently connected to self. Please configure to target IP, if available.");
            }
        }

        private void Start()
        {
        }
    }
}
