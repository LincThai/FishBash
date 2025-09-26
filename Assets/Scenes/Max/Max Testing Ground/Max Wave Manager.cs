using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MaxWaveManager : MonoBehaviour
{

    private MeshFilter meshFilter;
    public static MaxWaveManager instance;

    public float amplitude = 1f;
    public float length = 2f;
    public float speed = 1f;
    public float offset = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        meshFilter = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        offset += Time.deltaTime * speed;

        Vector3[] vertices = meshFilter.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = MaxWaveManager.instance.GetWaveHeight(transform.position.x + vertices[i].x, transform.position.y + vertices[i].z);
        }

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.RecalculateNormals();
    }

    public float GetWaveHeight(float x, float z)
    {
        return amplitude * Mathf.Sin(x + z / length + offset);
    }
}
