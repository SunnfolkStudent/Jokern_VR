using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class OverrideBounds : MonoBehaviour
{
    public Vector3 Center;
    public Vector3 Size;

    public void CreateUpdatedMesh()
    {
        var meshFilter = GetComponent<MeshFilter>();
        var meshCopy = Instantiate(meshFilter.sharedMesh);
        meshCopy.bounds = new Bounds(Center, Size);
        meshFilter.sharedMesh = meshCopy;
    }
}


[CustomEditor(typeof(OverrideBounds))]
public class OverrideBoundsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var component = (OverrideBounds)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Create Updated Mesh"))
        {
            component.CreateUpdatedMesh();
        }
    }
}
