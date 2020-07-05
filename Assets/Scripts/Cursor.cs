using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField]
    private float speedUpMultiplier = 1;
    public float speed = .05f;
    private float startSpeed, minX= float.MaxValue, maxX= float.MinValue;
    [SerializeField] private Vector2 zBounds = new Vector2(-.11f, -.27f);
    public bool stopped = false;
    private void Start()
    {
        startSpeed = speed;
        
    }

    private void Update()
    {
        if(stopped) return;
        speed += Time.deltaTime * speedUpMultiplier;
        speed = Mathf.Min(startSpeed, speed);
        var tmp = transform.position;
        tmp.x += Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        tmp.z += Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
        tmp.x = Mathf.Clamp(tmp.x, minX, maxX);
        tmp.z = Mathf.Clamp(tmp.z, zBounds.y, zBounds.x);
        transform.position = tmp;
    }

    public void GetBoundPoints(Vector3 low, Vector3 high)
    {
        minX = Mathf.Min(low.x, minX);
        maxX = Mathf.Max(high.x, maxX);
    }
}
