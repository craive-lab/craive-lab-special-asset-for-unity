using UnityEngine;

namespace EMPACResearch.Core.Audio
{
    public class RoomAudioMessenger
    {
        public static OSC osc;

        public void Send()
        {
            OscMessage msg = new OscMessage();
            msg.address = "";
            msg.values.Add("");
            osc.Send(msg);
        }

        public void Send(string address, Vector3 v)
        {
            OscMessage msg = new OscMessage();
            msg.address = address;
            msg.values.Add(v);
            osc.Send(msg);
        }
    }
}
