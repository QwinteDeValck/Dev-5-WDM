using System.Collections.Generic;
using UnityEngine;

public class TargetDetection : MonoBehaviour
{
    [SerializeField] private float detectionRange = 20f;
    [SerializeField] private float timeBeforeLog = 0.1f;
    [System.Serializable]
    public class LookedTarget
    {
        public string targetTag;
        public float lookDuration;
    }

    public List<LookedTarget> lookedTargets = new List<LookedTarget>();
    private Transform currentTarget = null;
    private float currentLookTime = 0f;

    void Update()
    {
        DetectTarget();
    }

    void DetectTarget()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionRange, ~0, QueryTriggerInteraction.Collide))
        {
            Transform hitTarget = hit.transform;

            if (hitTarget == currentTarget)
            {
                currentLookTime += Time.deltaTime;
            }
            else
            {
                if (currentTarget != null && currentLookTime >= timeBeforeLog)
                {
                    SaveTargetData(currentTarget.tag, currentLookTime);
                }

                // Reset voor nieuw target
                currentTarget = hitTarget;
                currentLookTime = 0f;
            }
        }
        else
        {
            if (currentTarget != null && currentLookTime >= timeBeforeLog)
            {
                SaveTargetData(currentTarget.tag, currentLookTime);
            }

            currentTarget = null;
            currentLookTime = 0f;
        }

        Debug.DrawRay(transform.position, transform.forward * detectionRange, Color.red);
    }

    void SaveTargetData(string tag, float duration)
    {
        // Sla data op in lijst
        lookedTargets.Add(new LookedTarget { targetTag = tag, lookDuration = duration });
        Debug.Log($"Naar {tag} gekeken voor {duration:F2} seconden");
    }
}
