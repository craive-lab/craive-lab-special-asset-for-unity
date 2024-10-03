using UnityEngine;

[RequireComponent(
    typeof(OSC),
    typeof(SoundSourceMonitor),
    typeof(RoomMonitor)
    )]
public class SoundSourceDistanceTracker : MonoBehaviour
{
    enum MessageType { None, Distance, Gain }

    [SerializeField]
    MessageType messageType = MessageType.None;

    OSC osc;
    GameObject room;
    GameObject[] soundSources;
    int sourceCount;

    public float[] distances {  get; private set; }

    void Start()
    {
        osc = GetComponent<OSC>();
        room = GetComponent<RoomMonitor>().room;
        soundSources = GetComponent<SoundSourceMonitor>().soundSources;
        sourceCount = GetComponent<SoundSourceMonitor>().sourceCount;

        distances = new float[sourceCount];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < sourceCount; i++)
        {
            distances[i] = Vector3.Distance(
                soundSources[i].transform.position, room.transform.position
            );
        }
        if (messageType != MessageType.None) SendDistanceMessage();
    }

    void SendDistanceMessage()
    {
        OscMessage msg = new OscMessage();
        msg.address = 
            (messageType == MessageType.Distance) ? "/distance" : "/gain";
        foreach (var distance in distances) msg.values.Add(
            (messageType == MessageType.Distance) ? distance : -distance);
        osc.Send(msg);
        Debug.Log(msg);
    }
}
