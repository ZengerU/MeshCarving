using UnityEngine;

public class CarveController : MonoBehaviour
{
    [SerializeField] private float multiplier = .99f;
    private Bounds area;
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Log")) return;
        area = GetComponent<MeshRenderer>().bounds;
        other.GetComponent<LogCarver>().CarveLog(area, multiplier, Mathf.Abs(area.center.z) - area.extents.z);
    }
}
