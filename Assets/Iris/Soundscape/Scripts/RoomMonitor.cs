using UnityEngine;

public class RoomMonitor : MonoBehaviour
{
    public GameObject room;

    public Vector3 roomPosition     { get; private set; }
    public Vector3 roomOrientation  { get; private set; }

    private void Awake()
    {
        if (room == null)
            room = Camera.main.gameObject.transform.parent.transform.gameObject;
    }

    private void Update()
    {
        roomPosition = room.transform.position;
        roomOrientation = room.transform.rotation.eulerAngles;
    }
}
