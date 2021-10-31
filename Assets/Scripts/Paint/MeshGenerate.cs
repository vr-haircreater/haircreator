using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class MeshGenerate : MonoBehaviour
{
    public SkinnedMeshRenderer rend;
    private Mesh mesh;
    public static Material GethairColor;
    Shader HairShader;

    Vector3[] vertice;
    Vector2[] uv;
    Vector4[] tangents;
    int[] triangle;
    BoneWeight[] weights;

    MeshCollider HairCollider;
    Rigidbody HairRigidbody;

    public void GenerateMesh(List<Vector3> PointPos,List<Vector3> GetPointPos, int Getwidth,int count)
    {
        GethairColor = GetComponent<Renderer>().material;
        HairShader = Shader.Find("Diffuse Fast");
        GethairColor.shader = HairShader;
        GethairColor.color = ButtonTransitioner.HairColor;

        rend = GetComponent<SkinnedMeshRenderer>();
        mesh = new Mesh();
        mesh.name = "HairModel";

        if (gameObject.GetComponent<MeshCollider>() == null)
        {
            HairCollider = gameObject.AddComponent<MeshCollider>();
        }
        else HairCollider = gameObject.GetComponent<MeshCollider>();
        HairCollider.sharedMesh = mesh;
        HairCollider.convex = true;
        HairCollider.isTrigger = true;

        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            HairRigidbody = gameObject.AddComponent<Rigidbody>();
        }
        else HairRigidbody = gameObject.GetComponent<Rigidbody>();
        HairRigidbody.isKinematic = true;
        
        vertice = new Vector3[GetPointPos.Count];
        uv = new Vector2[GetPointPos.Count];
        tangents = new Vector4[GetPointPos.Count];

        for (int i = 0, j = 0; i < GetPointPos.Count; i++, j++)
        {
            vertice[i] = GetPointPos[j];
            tangents[i] = new Vector4(1f, 0f, 0f, -1f);
        }

        int len = GetPointPos.Count / 4;
        float TexWidth = 0.8f;
        for (int i = 0, x = 0; i < len; i++)
        {
            for (int j = 1; j <= 4; j++)
            {
                uv[x] = new Vector2(TexWidth / 4 * j, 1.0f / len * i);
                x++;
            }
        }

        mesh.vertices = vertice;//mesh網格點生成
        mesh.uv = uv;
        mesh.tangents = tangents;

        int point = GetPointPos.Count - 2;
        triangle = new int[point * 6];

        int t = 0;
        for (int i = 1, vi = 0; i <= point - 2; i++, vi++)
        {
            if (i % 4 != 0) t = SetQuad(triangle, t, vi, vi + 1, vi + 4, vi + 5);
            else t = SetQuad(triangle, t, vi, vi - 3, vi + 4, vi + 1);
        }
        int vii = 0;
        t = SetQuad(triangle, t, vii + 2, vii + 1, vii + 3, vii);
        vii = GetPointPos.Count - 1;
        t = SetQuad(triangle, t, vii - 1, vii, vii - 2, vii - 3);

        mesh.triangles = triangle;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        //架設骨架
        weights = new BoneWeight[PointPos.Count * 4];
        for (int i = 0, n = 0; i < weights.Length; i++)
        {
            weights[i].boneIndex0 = n;
            weights[i].weight0 = 1;
            if ((i + 1) % 4 == 0) n++;
        }
        mesh.boneWeights = weights;

        Transform[] bones = new Transform[PointPos.Count];
        Matrix4x4[] bindPoses = new Matrix4x4[PointPos.Count];

        int z = 0;
        foreach (Transform obj in GameObject.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_C_Neck/J_Bip_C_Head/HairRig" + count).transform)
        {
            if (obj.name == "Hair" + z)
            {
                Destroy(obj.gameObject);
                Debug.Log("Yes");
            }
            if (z < bones.Length - 1) z++;
        }

        for (int i = 0; i < bones.Length; i++)
        {
            bones[i] = new GameObject("Hair" + i).transform;
            bones[i].parent = GameObject.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_C_Neck/J_Bip_C_Head/HairRig" + count).transform;

            bones[i].localRotation = Quaternion.identity;
            bones[i].localPosition = PointPos[i];

            bindPoses[i] = bones[i].worldToLocalMatrix * transform.localToWorldMatrix;
        }
        mesh.bindposes = bindPoses;
        rend.bones = bones;
        rend.sharedMesh = mesh;
        rend.sharedMaterial = GethairColor;

        Transform modelRoot;
        modelRoot = GameObject.Find("Girl/Root").transform;
        rend.rootBone = modelRoot;

    }

    private static int SetQuad(int[] triangles, int i, int v0, int v1, int v2, int v3)
    {
        triangles[i] = v0;
        triangles[i + 1] = v1;
        triangles[i + 2] = v2;
        triangles[i + 3] = v2;
        triangles[i + 4] = v1;
        triangles[i + 5] = v3;
        return i + 6;
    }
}
