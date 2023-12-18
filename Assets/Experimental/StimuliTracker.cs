using UnityEngine;

namespace EMPACResearch.Cognition
{
    public class StimuliTracker : MonoBehaviour
    {
        [SerializeField]
        OSC osc;

        [SerializeField]
        GameObject controller;

        [SerializeField]
        GameObject[] stimuli;

        int stimuliCount;

        Vector3 controllerPosition, referencePosition, relativePosition;
        float controllerHeading, referenceAngle, relativeAngle;

        void OnEnable()
        {
            if (stimuli == null)
            {
                stimuli = GameObject.FindGameObjectsWithTag("Stimulus");
            }
        }

        void Start()
        {
            stimuliCount = stimuli.Length;
        }


        void Update()
        {
            controllerPosition = controller.transform.position;
            controllerHeading = controller.transform.rotation.eulerAngles.y;

            for (int i = 0; i < stimuliCount; i++)
            {
                referencePosition = new Vector3(
                    stimuli[i].transform.position.x - controllerPosition.x, 0f,
                    stimuli[i].transform.position.z - controllerPosition.z);
                referenceAngle = Vector3.SignedAngle(referencePosition, Vector3.right, Vector3.up);

                float distance = Vector3.Distance(stimuli[i].transform.position, controllerPosition);
                relativeAngle = Mathf.Deg2Rad * (controllerHeading + referenceAngle);
                relativePosition = new Vector3(
                    distance * Mathf.Cos(relativeAngle), 0f,
                    distance * Mathf.Sin(relativeAngle)
                    );
            }
        }

        void OnDisable()
        {
            if (stimuli != null)
            {
                stimuli = null;
            }
        }
    }
}
