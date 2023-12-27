using CodeMonkey.Utils;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private NPC_MovementScript _movementScript;
    private float fov;

    private Mesh mesh;
    private Vector3 origin;
    private float startingAngle;
    private float viewDistance;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 360;
        viewDistance = 5f;
        origin = Vector3.zero;
    }

    private void LateUpdate()
    {
        var rayCount = 50;
        var angle = startingAngle;
        var angleIncrease = fov / rayCount;

        var vertices = new Vector3[rayCount + 1 + 1];
        var uv = new Vector2[vertices.Length];
        var triangles = new int[rayCount * 3];

        vertices[0] = origin;

        var vertexIndex = 1;
        var triangleIndex = 0;
        for (var i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            var raycastHit2D = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), viewDistance, layerMask);
            Debug.DrawLine(origin, raycastHit2D.point);
            if (raycastHit2D.collider == null)
            {
                
                vertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                // Hit object
                if (raycastHit2D.collider.gameObject.tag == "Player")
                {
                    _movementScript.LastPlayerLocation = raycastHit2D.collider.gameObject.transform.position;
                    _movementScript.StopMovement = false;
                    //Debug.DrawLine(transform.position,_movementScript.LastPlayerLocation,Color.green);
                    Debug.Log("Player Spotted");
                }
                vertex = raycastHit2D.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle += angleIncrease;
        }


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        //mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) - fov / 2f;
    }

    public void SetFoV(float fov)
    {
        this.fov = fov;
    }

    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }
}