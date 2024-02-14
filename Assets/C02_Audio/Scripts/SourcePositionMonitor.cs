using UnityEngine;

[RequireComponent(
    typeof(OSC),
    typeof(SoundSourceMonitor),
    typeof(RoomMonitor)
    )]
public class SoundSourcePositionTracker : MonoBehaviour
{
    enum MessageType { None, Position2D, Position3D }

    [SerializeField]
    MessageType messageType = MessageType.None;

    OSC osc;
    GameObject room;
    GameObject[] soundSources;
    int sourceCount;

    Vector3[] sourcePositions;
    Vector3 roomPosition;


    void Awake()
    {
        osc = GetComponent<OSC>();
        room = GetComponent<RoomMonitor>().room;
        soundSources = GetComponent<SoundSourceMonitor>().soundSources;
        sourceCount = GetComponent<SoundSourceMonitor>().sourceCount;
    }


    void OnEnable()
    {
        sourcePositions = new Vector3[sourceCount];
    }


    void OnDisable()
    {
        sourcePositions = null;
    }


    void OnValidate()
    {
        if (sourcePositions != null)
        {
            OnDisable();
            OnEnable();
        }
    }


    void Update()
    {
        roomPosition = room.transform.position;

        /* Unless specified, the default source position updates are in 2D. */
        sourcePositions = (messageType == MessageType.Position3D) ? 
            UpdatePosition3D() : UpdatePosition2D();

        if (messageType != MessageType.None)
            SendPositionMessage();
    }


    Vector3[] UpdatePosition2D()
    {
        Vector3[] positions = new Vector3[sourceCount];
        /* TODO */
        return positions;
    }


    Vector3[] UpdatePosition3D()
    {
        Vector3[] positions = new Vector3[sourceCount];
        /* TODO */
        return positions;
    }


    void SendPositionMessage()
    {
        OscMessage msg;
        for (int i = 0; i < sourceCount; i++)
        {
            msg = new OscMessage();
            msg.address = "/source/" + (i + 1).ToString() + 
                ((messageType == MessageType.Position3D) ? "/xyz" : "/xy");
            msg.values.Add(sourcePositions[i].x);
            msg.values.Add(sourcePositions[i].z);
            if (messageType == MessageType.Position3D)
                msg.values.Add(sourcePositions[i].y);
            osc.Send(msg);
        }
    }
}
