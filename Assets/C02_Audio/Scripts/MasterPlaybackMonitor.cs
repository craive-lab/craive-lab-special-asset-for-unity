using UnityEngine;

[RequireComponent(typeof(OSC))]
public class MasterPlaybackMonitor : MonoBehaviour
{
    OSC osc;
    int status = 0;

    void Awake()
    {
        osc = GetComponent<OSC>();
    }

    void Start()
    {
        status = 1;
        Send(status);
    }

    void OnDisable()
    {
        status = 0;
        Send(status);
    }

    public void Send(int status)
    {
        OscMessage msg = new OscMessage();
        msg.address = "/status";
        msg.values.Add(status);
        osc.Send(msg);
    }
}
