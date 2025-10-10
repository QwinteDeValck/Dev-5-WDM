using UnityEngine;

public class TargetDetection : MonoBehaviour
{
    [SerializeField] private float detectionRange = 20f;
    
    void Update()
    {
        DetectObjectInView();
    }

    void DetectObjectInView()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            Debug.Log("Camera ziet object met tag: " + hit.collider.tag);
        }

        Debug.DrawRay(transform.position, transform.forward * detectionRange, Color.red);
    }
}
