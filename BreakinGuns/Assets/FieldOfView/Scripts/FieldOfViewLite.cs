using CodeMonkey.Utils;
using UnityEngine;

public class FieldOfViewLite : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private NPC_MovementScript _movementScript;
    [SerializeField] private NPC_ShootingLogic _shooting;
    private float fov;

    [SerializeField] private Mesh mesh;
    [SerializeField] private Vector3 origin;
    [SerializeField] private float startingAngle;
    [SerializeField] private float viewDistance;
    [SerializeField] private float shootDistance;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 360;
        origin = Vector3.zero;
    }

    private void Update()
    {
        var rayCount = 50;
        var angle = startingAngle;
        var angleIncrease = fov / rayCount;
        var vertexIndex = 1;

        // DetectDistance

        for (var i = 0; i <= rayCount; i++)
        {
            Vector3 DetectionVertex;
            var rayCastHitDetection =
            Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), viewDistance, layerMask);
            Debug.DrawLine(origin, rayCastHitDetection.point,Color.green);
            if (rayCastHitDetection.collider == null)
            {
                DetectionVertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                // Hit object
                if (rayCastHitDetection.collider.gameObject.tag == "Player")
                {
                    _movementScript.LastPlayerLocation = rayCastHitDetection.collider.gameObject.transform.position;
                    _movementScript.StopMovement = false;
                    //Debug.DrawLine(transform.position,_movementScript.LastPlayerLocation,Color.green);
                   // Debug.Log("Player Spotted");
                }

                DetectionVertex = rayCastHitDetection.point;
            }

            vertexIndex++;
            angle += angleIncrease;
        }

        // ShootDistance

        for (int i = 0; i <= rayCount; i++)
        {
                Vector3 ShootVertex;
                var rayCastHitShooting = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), shootDistance, layerMask);
                Debug.DrawLine(origin, rayCastHitShooting.point,Color.red);
                if (rayCastHitShooting.collider == null)
                {
                    ShootVertex = origin + UtilsClass.GetVectorFromAngle(angle) * shootDistance;
                }
                else
                {
                    // Hit object
                    if (rayCastHitShooting.collider.gameObject.tag == "Player")
                    {
                        _shooting.CanShoot = true;
                        Debug.Log("Shoot");
                    }
                    ShootVertex = rayCastHitShooting.point;
                }
                vertexIndex++;
                angle += angleIncrease;
        }
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