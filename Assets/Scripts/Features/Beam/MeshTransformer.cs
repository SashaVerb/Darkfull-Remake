using UnityEngine;

public static class MeshTransformer
{
    public static void SetTwoPoints(this MeshFilter meshFilter, Vector3 from, Vector3 to)
    {
        var transform = meshFilter.transform;
        
        Vector3 dir = to - from;
        transform.up = dir.normalized;
        transform.position = (from + to) * 0.5f;

        float meshHeight = meshFilter.sharedMesh.bounds.size.y;
        if (meshHeight == 0f) return;

        Vector3 scale = transform.localScale;
        scale.y = dir.magnitude / meshHeight;
        transform.localScale = scale;
    }
}
