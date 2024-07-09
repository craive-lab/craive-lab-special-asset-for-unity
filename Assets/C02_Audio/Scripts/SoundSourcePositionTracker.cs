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
    float roomHeading;

    public Vector3[] relativePositions { get; private set; }


    void Awake()
    {
        osc = GetComponent<OSC>();
        room = GetComponent<RoomMonitor>().room;
        soundSources = GetComponent<SoundSourceMonitor>().soundSources;
        sourceCount = GetComponent<SoundSourceMonitor>().sourceCount;

        sourcePositions = new Vector3[sourceCount];
    }


    /*
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
    */


    void Update()
    {
        roomPosition = room.transform.position;
        roomHeading = room.transform.rotation.eulerAngles.y;

        /* Unless specified, the default source position updates are in 2D. */
        sourcePositions = (messageType == MessageType.Position3D) ? 
            UpdatePosition3D() : UpdatePosition2D();

        Debug.Log(sourcePositions[0]);

        if (messageType != MessageType.None)
            SendPositionMessage();
    }


    Vector3[] UpdatePosition2D()
    {
        Vector3[] positions = new Vector3[sourceCount];

        Vector3 referencePosition;
        float referenceAngle, distance, relativeAngle;

        for (int i = 0; i < sourceCount; i++)
        {
            /* Calculate a reference position between the controller and the sound source
             * without taking into account the controller's heading. */
            referencePosition = new Vector3(
                soundSources[i].transform.position.x - roomPosition.x, 0f,
                soundSources[i].transform.position.z - roomPosition.z);

            /* Determine the absolute angle of the virtual sound source based upon the 
             * positive x-axis of the world coordinates (right-hand rule). */
            referenceAngle = Vector3.SignedAngle(
                referencePosition, Vector3.right, Vector3.up
            );

            /* Calculate the distance between the sound source and the controller. */
            distance = Vector3.Distance(
                soundSources[i].transform.position, roomPosition
            );

            /* Calculate the relative angle between the controller's heading 
             * and the direction of the virtual sound source. */
            relativeAngle =
                Mathf.Deg2Rad * (roomHeading + referenceAngle);

            /* Finally, calculate the relative positions based upon the 
             * relative angle. */
            relativePositions[i] = new Vector3(
                distance * Mathf.Cos(relativeAngle), 0f,
                distance * Mathf.Sin(relativeAngle)
                );
        }

        return positions;
    }


    Vector3[] UpdatePosition3D()
    {
        Vector3[] positions = new Vector3[sourceCount];
        
        for (int i = 0; i < sourceCount; i++)
        {

        }

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
            Debug.Log(msg);
        }
    }
}
