using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(LineRenderer))]
public class LaserPointer : MonoBehaviour
{
    private GameObject _cursor;
    private LineRenderer _lineRenderer;

    public float distance = 10.0f;
    public float cursorSize = 0.025f;

    private void Awake()
    {
        _cursor = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Destroy(_cursor.GetComponent<Collider>());
        _cursor.transform.localScale = Vector3.one * cursorSize;
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        var hit = CastRay(distance);
        Vector3 endPos;

        if (hit.collider)   endPos = hit.point;
        else                endPos = transform.forward * distance;
        _cursor.transform.position = endPos;

        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, endPos);
    }

    private void OnValidate()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.forward * distance);
    }

    RaycastHit CastRay(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, length);

        return hit;
    }
}