using UnityEngine;

namespace EMPACResearch.Core.Audio
{
    public class RoomAudioMonitor : MonoBehaviour
    {
        [SerializeField]
        OSC osc;

        RoomAudioMessenger messenger;

        private void Start()
        {
            messenger.Send();
        }
    }
}
