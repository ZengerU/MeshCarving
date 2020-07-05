using UnityEngine;
using DG.Tweening;

public class LogCarver : MonoBehaviour
{
    [SerializeField] private GameObject vertexPrefab, logPrefab;
    [SerializeField] private Transform logTosser;
    private const float CutOffset = .001f;
    private const float MinDistance = .006f;

    private Vector3 minPointWorld = new Vector3(float.MaxValue, 0, 0),
        maxPointWorld = new Vector3(float.MinValue, 0, 0),
        minPointLocal,
        maxPointLocal;
    
    private Vector3[] vertices;
    private MeshFilter filter;
    private Cursor curs;

    public void CarveLog(Bounds bound, float multiplier, float dist)
    {
        //
        // IMPORTANT TODO: remove multiplier. set vertex using the dist. It screws up performance.
        // 
        var meshFilter = GetComponent<MeshFilter>();
        for (var i = 0; i < vertices.Length; i++)
        {
            var tmp = transform.TransformPoint(vertices[i]);
            var checkX = tmp.x >= bound.center.x - bound.extents.x && tmp.x <= bound.center.x + bound.extents.x;
            var distance = Vector2.Distance(new Vector2(tmp.z, tmp.y), Vector2.zero);
            if (!(distance >= dist) || !checkX) continue;
            if (distance < MinDistance)
            {
                CutLog(bound.center.x - bound.extents.x, bound.center.x + bound.extents.x);
                return;
            }
            
            vertices[i].x *= multiplier;
            vertices[i].z *= multiplier;
            curs.speed = 0.001f;
        }

        meshFilter.mesh.vertices = vertices;
        Destroy(GetComponent<MeshCollider>());
        var coll = gameObject.AddComponent<MeshCollider>();
        coll.sharedMesh = meshFilter.mesh;
    }

    private void CutLog(float left, float right)
    {
        // type 0 : carver inside this log. 2 : carver cutting this log and the right neighbor. 1: same as 2 but left side.
        var type = maxPointWorld.x < right ? 2 : minPointWorld.x > left ? 1 : 0;
        print($"type {type} scenario");
        //TODO : 0 > duplicate this log, morph both. 1|2 > morph this and neighbor.
        switch (type)
        {
            case 0:
                var newLog = Instantiate(logPrefab, transform.position, transform.rotation, transform.parent);
                newLog.transform.SetSiblingIndex(transform.GetSiblingIndex()+1);
                newLog.GetComponent<LogCarver>().MorphLog(right, false, vertices);
                MorphLog(left, true);
                DecideSide(left, right);
                break;
            case 1:
                return;
            case 2:
                var nextLog = transform.parent.GetChild(transform.GetSiblingIndex() + 1).GetComponent<LogCarver>();
                MorphLog(left, true);
                nextLog.MorphLog(right, false);
                DecideSide(left, right);
                break;
        }
    }
    private void MorphLog(float x, bool higher, Vector3[] vertArr = null)
    {
        x += CutOffset * (higher ? -1 : +1);
        var meshFilter = GetComponent<MeshFilter>();
        
        vertices = vertArr ?? meshFilter.mesh.vertices;
        for (var i = 0; i < vertices.Length; i++)
        {
            var tmp = transform.TransformPoint(vertices[i]);
            var checkX = higher ? tmp.x >= x : tmp.x <= x;
            if (!checkX) continue;
            vertices[i].x = 0;
            vertices[i].z = 0;
            vertices[i].y = transform.InverseTransformPoint(new Vector3(x, 0, 0)).y;
        }
        meshFilter.mesh.vertices = vertices;
        Destroy(GetComponent<MeshCollider>());
        var coll = gameObject.AddComponent<MeshCollider>();
        coll.sharedMesh = meshFilter.mesh;
        SetEndPoints();
    }

    private void DecideSide(float left, float right)
    {
        var parent = transform.parent;
        var startPoint = parent.GetChild(0).GetComponent<LogCarver>().minPointWorld.x;
        var endPoint = parent.GetChild(parent.childCount - 1).GetComponent<LogCarver>().maxPointWorld.x;
        var leftSide = left - parent.GetChild(0).GetComponent<LogCarver>().minPointWorld.x;
        var rightSide = parent.GetChild(parent.childCount - 1).GetComponent<LogCarver>().maxPointWorld.x - right;
        var tossRight = leftSide >= rightSide;
        print($"discarding the {(tossRight ? "right" : "left")} side");
        var center = new Vector3(tossRight ? (endPoint + rightSide) /2 : (startPoint + leftSide) /2, 0, 0);
        ThrowLog(center, tossRight);
    }

    private void ThrowLog(Vector3 center, bool rightSide)
    {
        logTosser.rotation = Quaternion.identity;
        logTosser.position = center;
        var parent = transform.parent;
        if (rightSide)
        {
            while (transform.GetSiblingIndex() != parent.childCount -1)
            {
                parent.GetChild(parent.childCount - 1).parent = logTosser;
            }
        }
        else
        {
            while (transform.GetSiblingIndex() != 0)
            {
                parent.GetChild(0).parent = logTosser;
            }

            transform.parent = logTosser;
        }


        curs.stopped = true;
        logTosser.DORotate(new Vector3(0, 0, rightSide ? -90 : 90), 1);
        logTosser.DOMove(new Vector3((rightSide ? 3 : -3), 1 , 0), 2).OnComplete(() =>
        {
            curs.stopped = false; 
            foreach (Transform child in logTosser)
            {
                Destroy(child.gameObject);
            }
        });
    }
    private void Start()
    {
        curs = (Cursor) FindObjectOfType(typeof(Cursor));
        filter = GetComponent<MeshFilter>();
        vertices = filter.mesh.vertices;
        SetEndPoints();
        curs.GetBoundPoints(minPointWorld, maxPointWorld);
    }

    private void SetEndPoints()
    {
        var transform1 = transform;
        foreach (var child in vertices)
        {
            var tmp = transform1.TransformPoint(child);
            minPointWorld.x = Mathf.Min(tmp.x, minPointWorld.x);
            maxPointWorld.x = Mathf.Max(tmp.x, maxPointWorld.x);
        }

        minPointLocal = transform1.InverseTransformPoint(minPointWorld);
        maxPointLocal = transform1.InverseTransformPoint(maxPointWorld);
    }
}