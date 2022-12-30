//  Copyright © 2015-2022 Pico Technology Co., Ltd. All Rights Reserved.

using System;
using System.Collections.Generic;
using PXR_Audio.Spatializer;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(PXR_Audio_Spatializer_SceneMaterial))]
public partial class PXR_Audio_Spatializer_SceneGeometry : MonoBehaviour
{
    [SerializeField] private bool includeChildren = false;
    [SerializeField] private bool visualizeMeshInEditor = false;
    [SerializeField] private Mesh bakedStaticMesh;

    #region EDITOR-ONLY SerializedFields

#if UNITY_EDITOR
    [SerializeField] private LayerMask meshBakingLayerMask = ~0;
    [SerializeField, HideInInspector] private string currentBakedStaticMeshAssetPath = null;
#endif

    #endregion

    public bool isStaticMeshBaked => bakedStaticMesh != null;

    private int geometryId = -1;

    public int GeometryId
    {
        get => geometryId;
    }

    private int staticGeometryID = -1;

    public int StaticGeometryId => staticGeometryID;

    private PXR_Audio_Spatializer_Context context;

    public PXR_Audio_Spatializer_Context Context
    {
        get
        {
            if (context == null)
            {
                context = FindObjectOfType<PXR_Audio_Spatializer_Context>();
            }

            return context;
        }
    }

    private PXR_Audio_Spatializer_SceneMaterial material;

    public PXR_Audio_Spatializer_SceneMaterial Material
    {
        get
        {
            if (material == null)
            {
                material = GetComponent<PXR_Audio_Spatializer_SceneMaterial>();
            }

            return material;
        }
    }

    private void GetAllMeshFilter(Transform transform, bool includeChildren, List<MeshFilter> meshFilterList,
        bool isStatic, LayerMask layerMask)
    {
        if (includeChildren)
        {
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                var childTransform = transform.GetChild(i);
                if (childTransform.GetComponent<PXR_Audio_Spatializer_SceneGeometry>() == null)
                {
                    GetAllMeshFilter(childTransform.transform, includeChildren, meshFilterList, isStatic, layerMask);
                }
            }
        }

        //  Gather this mesh only when
        //  1. Its isStatic flag is equal to our requirement
        //  2. Its layer belongs to layerMask set 
        if (transform.gameObject.isStatic == isStatic && ((1 << transform.gameObject.layer) & layerMask) != 0)
        {
            var meshFilterArray = transform.GetComponents<MeshFilter>();
            //  cases we don't add to mesh filter list
            //   1. meshFilter.shardmesh == null
            //   2. meshFilter.sharedmesh.isReadable == false
            if (meshFilterArray != null)
            {
                for (int i = 0; i < meshFilterArray.Length; i++)
                {
                    var meshFilter = meshFilterArray[i];
                    if (meshFilter != null && meshFilter.sharedMesh != null &&
                        (isStatic || meshFilter.sharedMesh.isReadable))
                    {
                        meshFilterList.Add(meshFilter);
                    }
                }
            }
        }
    }

    private static Mesh CombineMeshes(List<MeshFilter> meshFilterList)
    {
        CombineInstance[] combines = new CombineInstance[meshFilterList.Count];
        for (int i = 0; i < meshFilterList.Count; i++)
        {
            combines[i].mesh = meshFilterList[i].sharedMesh;
            combines[i].transform =
                Matrix4x4.Scale(new Vector3(1, 1, -1)) * meshFilterList[i].transform.localToWorldMatrix;
        }

        Mesh combinedMesh = new Mesh
        {
            name = "combined meshes",
            indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
        };
        combinedMesh.CombineMeshes(combines, true, true);

        return combinedMesh;
    }

    private static float[] FlattenVerticesBuffer(Vector3[] verticesBuffer)
    {
        float[] vertices = new float[verticesBuffer.Length * 3];
        int index = 0;
        foreach (Vector3 vertex in verticesBuffer)
        {
            vertices[index++] = vertex.x;
            vertices[index++] = vertex.y;
            vertices[index++] = vertex.z;
        }

        return vertices;
    }

    /// <summary>
    /// Submit non-static mesh of this geometry and its material into spatializer engine context
    /// </summary>
    /// <returns>Result of static mesh submission</returns>
    public PXR_Audio.Spatializer.Result SubmitMeshToContext()
    {
        // find all meshes
        var meshFilterList = new List<MeshFilter>();
        GetAllMeshFilter(transform, includeChildren, meshFilterList, false, ~0);

        //  Combine all meshes
        Mesh combinedMesh = CombineMeshes(meshFilterList);

        //  flatten vertices buffer into a float array
        float[] vertices = FlattenVerticesBuffer(combinedMesh.vertices);

        //  Submit all meshes
        PXR_Audio.Spatializer.Result result = PXR_Audio_Spatializer_Context.Instance.SubmitMeshAndMaterialFactor(
            vertices, vertices.Length / 3,
            combinedMesh.triangles, combinedMesh.triangles.Length / 3,
            Material.absorption, Material.scattering, Material.transmission,
            ref geometryId);

        return result;
    }

    /// <summary>
    /// Submit static mesh of this geometry and its material into spatializer engine context
    /// </summary>
    /// <returns>Result of static mesh submission</returns>
    public PXR_Audio.Spatializer.Result SubmitStaticMeshToContext()
    {
        PXR_Audio.Spatializer.Result result = Result.Success;
        if (bakedStaticMesh != null)
        {
            float[] tempVertices = FlattenVerticesBuffer(bakedStaticMesh.vertices);

            result = PXR_Audio_Spatializer_Context.Instance.SubmitMeshAndMaterialFactor(tempVertices,
                bakedStaticMesh.vertices.Length, bakedStaticMesh.triangles,
                bakedStaticMesh.triangles.Length / 3, Material.absorption, Material.scattering, Material.transmission,
                ref staticGeometryID);

            if (result != Result.Success)
                Debug.LogError("Failed to submit static audio mesh: " + gameObject.name + ", Error code is: " + result);
            else
                Debug.LogFormat("Submitted static geometry #{0}, gameObject name is {1}", staticGeometryID.ToString(),
                    name);
        }

        return result;
    }


#if UNITY_EDITOR
    public int BakeStaticMesh(LayerMask layerMask)
    {
        List<MeshFilter> meshList = new List<MeshFilter>();
        GetAllMeshFilter(transform, includeChildren, meshList, true, meshBakingLayerMask);

        SerializedObject serializedObject = new SerializedObject(this);
        if (meshList.Count == 0)
        {
            bakedStaticMesh = null;
        }
        else
        {
            bakedStaticMesh = CombineMeshes(meshList);
            bakedStaticMesh.name = "baked mesh for ygg";
        }

        serializedObject.FindProperty("bakedStaticMesh").objectReferenceValue = bakedStaticMesh;

        if (bakedStaticMesh != null)
        {
            System.IO.Directory.CreateDirectory("Assets/Resources/PxrAudioSpatializerBakedSceneMeshes/");
            if (!string.IsNullOrEmpty(currentBakedStaticMeshAssetPath))
            {
                AssetDatabase.DeleteAsset(currentBakedStaticMeshAssetPath);
            }

            currentBakedStaticMeshAssetPath = "Assets/Resources/PxrAudioSpatializerBakedSceneMeshes/" + name + "_" +
                                              GetInstanceID() + "_" +
                                              System.DateTime.UtcNow.ToBinary() + ".yggmesh";
            serializedObject.FindProperty("currentBakedStaticMeshAssetPath").stringValue =
                currentBakedStaticMeshAssetPath;
            AssetDatabase.CreateAsset(bakedStaticMesh, currentBakedStaticMeshAssetPath);
            AssetDatabase.SaveAssets();
        }

        serializedObject.ApplyModifiedProperties();
        return meshList.Count;
    }

    public void ClearBakeStaticMesh()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        bakedStaticMesh = null;
        serializedObject.FindProperty("bakedStaticMesh").objectReferenceValue = null;
        if (!string.IsNullOrEmpty(currentBakedStaticMeshAssetPath))
        {
            AssetDatabase.DeleteAsset(currentBakedStaticMeshAssetPath);
            currentBakedStaticMeshAssetPath = null;
            serializedObject.FindProperty("currentBakedStaticMeshAssetPath").stringValue =
                currentBakedStaticMeshAssetPath;
        } 
        serializedObject.ApplyModifiedProperties();
    }
#endif

    public void OnDrawGizmos()
    {
        if (visualizeMeshInEditor)
        {
            //  Visualize non-static meshes
            // find all MeshFilter
            var meshFilterList = new List<MeshFilter>();
            GetAllMeshFilter(transform, includeChildren, meshFilterList, false, ~0);

            for (int i = 0; i < meshFilterList.Count; i++)
            {
                var mesh = meshFilterList[i].sharedMesh;
                var transform = meshFilterList[i].transform;
                Gizmos.DrawWireMesh(mesh,
                    transform.position, transform.rotation, transform.localScale);
            }

            //  Visualize baked static meshes
            if (isStaticMeshBaked)
            {
                Color colorBackUp = Gizmos.color;
                Color c;
                c.r = 0.0f;
                c.g = 0.7f;
                c.b = 0.0f;
                c.a = 1.0f;
                Gizmos.color = c;
                Gizmos.DrawWireMesh(bakedStaticMesh, Vector3.zero, Quaternion.identity, new Vector3(1, 1, -1));
                Gizmos.color = colorBackUp;
            }
        }
    }
}