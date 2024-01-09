using UnityEngine;

public class LineRendererExample : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float lineScale = 5f;

    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, pointA.position);

            Vector3 direction = pointB.position - pointA.position;
            float lineLength = direction.magnitude * lineScale;

            lineRenderer.SetPosition(1, pointA.position + direction.normalized * lineLength);
        }
    }
}
