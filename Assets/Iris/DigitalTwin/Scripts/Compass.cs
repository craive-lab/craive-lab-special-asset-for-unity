using UnityEngine;

public class Compass : MonoBehaviour
{
    enum VisualizationMode { Compass2D, Compass3D }

    [SerializeField]
    VisualizationMode mode;

    private void OnDrawGizmos()
    {
        if (mode == VisualizationMode.Compass2D)
            DrawCompass2D();
        else if (mode == VisualizationMode.Compass3D)
            DrawCompass3D();
    }

    void DrawCompass2D()
    {

    }

    void DrawCompass3D()
    {

    }
}
