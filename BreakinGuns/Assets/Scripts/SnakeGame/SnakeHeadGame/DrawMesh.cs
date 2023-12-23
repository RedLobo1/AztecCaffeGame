using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DrawLogic
{


public class DrawMesh : MonoBehaviour
{
    private Mesh _mesh;
    private Vector3 _lastMousePosition;
    [SerializeField] float _lineThickness;
    [SerializeField] float _distance;

    public Transform KnifeTipTransform;
    private bool _drawing;
    private bool _startedLine;

        public bool Drawing
        {
            get => _drawing;
            set
            {
                if (_drawing == value)
                {
                    return;
                }
                SetUpDrawStart();
                _drawing = value;
            }
        }

        private void Update()
        {
            if (Drawing)
            {
                DrawLogic();
            }
        }

        private void DrawLogic()
        {
            if (Input.GetMouseButtonDown(0) && !_startedLine)
            {
                SetUpDrawStart();
                _startedLine = true;
                _lastMousePosition = Mouseposition();
            }

            if (Drawing)
            {
                float minDistance = _distance;
                if (Vector3.Distance(Mouseposition(), _lastMousePosition) > minDistance)
                {
                    Vector3[] vertices = new Vector3[_mesh.vertices.Length + 2];
                    Vector2[] uv = new Vector2[_mesh.uv.Length + 2];
                    int[] triangles = new int[_mesh.triangles.Length + 6];

                    _mesh.vertices.CopyTo(vertices, 0);
                    _mesh.uv.CopyTo(uv, 0);
                    _mesh.triangles.CopyTo(triangles, 0);

                    int vIndex = vertices.Length - 4;
                    int vIndex0 = vIndex + 0;
                    int vIndex1 = vIndex + 1;
                    int vIndex2 = vIndex + 2;
                    int vIndex3 = vIndex + 3;

                    Vector3 mouseForwardVector = (Mouseposition() - _lastMousePosition).normalized;
                    Vector3 normaL2D = new Vector3(0, 0, -1f);
                    float lineThickness = _lineThickness;
                    Vector3 newVertexUp = Mouseposition() + Vector3.Cross(mouseForwardVector, normaL2D) * lineThickness;
                    Vector3 newVertexDown = Mouseposition() + Vector3.Cross(mouseForwardVector, normaL2D * -1f) * lineThickness;

                    vertices[vIndex2] = newVertexUp;
                    vertices[vIndex3] = newVertexDown;

                    uv[vIndex2] = Vector2.zero;
                    uv[vIndex3] = Vector2.zero;

                    int tIndex = triangles.Length - 6;

                    triangles[tIndex + 0] = vIndex0;
                    triangles[tIndex + 1] = vIndex2;
                    triangles[tIndex + 2] = vIndex1;

                    triangles[tIndex + 3] = vIndex1;
                    triangles[tIndex + 4] = vIndex2;
                    triangles[tIndex + 5] = vIndex3;

                    _mesh.vertices = vertices;
                    _mesh.uv = uv;
                    _mesh.triangles = triangles;

                    _lastMousePosition = Mouseposition();
                }
            }
        }

        private void SetUpDrawStart()
        {
            _mesh = new Mesh();

            Vector3[] vertices = new Vector3[4];
            Vector2[] uv = new Vector2[4];
            int[] triangles = new int[6];

            vertices[0] = Mouseposition();
            vertices[1] = Mouseposition();
            vertices[2] = Mouseposition();
            vertices[3] = Mouseposition();

            uv[0] = Vector2.zero;
            uv[1] = Vector2.zero;
            uv[2] = Vector2.zero;
            uv[3] = Vector2.zero;

            triangles[0] = 0;
            triangles[1] = 3;
            triangles[2] = 1;

            triangles[3] = 1;
            triangles[4] = 3;
            triangles[5] = 2;

            _mesh.vertices = vertices;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
            _mesh.MarkDynamic();


            GetComponent<MeshFilter>().mesh = _mesh;
        }

        private Vector3 Mouseposition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = -2;
        return mousePosition;
    }
}
}
