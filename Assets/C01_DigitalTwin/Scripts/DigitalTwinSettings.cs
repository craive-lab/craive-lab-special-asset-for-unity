using UnityEngine;


[CreateAssetMenu]
public class DigitalTwinSettings : ScriptableObject
{
    public enum Topology { Rectangular, Cylindrical }
    public Topology topology = Topology.Rectangular;

    public float width = 10f;
    public float length = 12f;
    public float height = 4.2f;
    public float radius = 1.5f;
    
}
