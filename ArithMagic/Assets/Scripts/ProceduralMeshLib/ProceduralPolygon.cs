using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ProceduralPolygon : MonoBehaviour {

    [SerializeField, Range(3, 20)]
    private int polyn = 3;

    [SerializeField, Range(0.1f, 10.0f)]
    private float height = 1.0f;
    [SerializeField, Range(0.1f, 10.0f)]
    private float length = 1.0f;

    [SerializeField]
    private Vector3 polynorm = Vector3.up;

    private MeshFilter mf;

    // Use this for initialization
    void Start() {
        mf = gameObject.AddComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            BuildMesh();
            RotateMyself();
        }
    }

    private void BuildMesh() {
        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        Vector3[] vert = new Vector3[polyn + 1];

        vert[0] = Vector3.zero;
        vert[1] = new Vector3(-length, 0, 0);

        float theta = 2.0f / polyn * Mathf.PI;
        float costh = Mathf.Cos(theta);
        float sinth = Mathf.Sin(theta);
        Vector3 curr = new Vector3(-length, 0, 0);
        for (int i = 2; i < polyn; ++i) {
            curr = new Vector3(curr.x * costh - curr.z * sinth, 0, curr.x * sinth + curr.z * costh);
            vert[i] = vert[i - 1] + curr;
        }

        Vector3 center = Vector3.zero;
        for (int i = 0; i < polyn; ++i)
            center += vert[i];
        center = center / (float)polyn;
        vert[polyn] = Vector3.up * height;
        for (int i = 0; i < polyn; ++i)
            vert[i] -= center;

        int[] tri = new int[(2 * polyn - 2) * 3];

        for (int i = 0; i < polyn - 2; ++i) {
            tri[3 * i + 0] = 0;
            tri[3 * i + 1] = i + 1;
            tri[3 * i + 2] = (i + 2) % polyn;
        }
        for (int i = polyn - 2; i < 2 * polyn - 2; ++i) {
            tri[3 * i + 0] = polyn;
            tri[3 * i + 1] = (i - polyn + 3) % polyn;
            tri[3 * i + 2] = i - polyn + 2;
        }

        Vector3[] norm = new Vector3[polyn + 1];
        for (int i = 0; i < polyn + 1; ++i)
            norm[i] = vert[i].normalized;

        mesh.vertices = vert;
        mesh.triangles = tri;
        mesh.normals = norm;
    }

    private void RotateMyself() {
        transform.rotation = Quaternion.identity;
        Quaternion q = Quaternion.FromToRotation(Vector3.up, polynorm);
        transform.rotation = q;
    }
}
