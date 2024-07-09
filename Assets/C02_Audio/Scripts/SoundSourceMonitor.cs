using System.Collections.Generic;
using UnityEngine;

public class SoundSourceMonitor : MonoBehaviour
{
    /// <summary>
    /// Monitor for virtual sound sources.
    /// </summary>
    /// <param 
    
    enum AutoDetectMode 
    { 
        Off, 
        On,
        // Static,
        // Dynamic
    }

    [SerializeField]
    AutoDetectMode autoDetectMode = AutoDetectMode.Off;

    public GameObject[] soundSources { get; private set; }

    public int sourceCount { get; private set; }

    void Awake()
    {
        if (soundSources == null)
        {
            if (autoDetectMode == AutoDetectMode.On)
                soundSources = GameObject.FindGameObjectsWithTag("SoundSource");
            else Debug.LogWarning("There is no sound source being monitored.");
        }
        sourceCount = soundSources.Length;
    }
}