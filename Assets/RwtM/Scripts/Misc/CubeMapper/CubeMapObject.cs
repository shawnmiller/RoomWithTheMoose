using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class CubeMapObject : MonoBehaviour
{
  public Transform MaterialObject;
  public Cubemap CubeMapTexture;
  public string TextureName = "_ReflectionTex";
  public string InstanceID;

  void Update()
  {
    if (!Application.isEditor)
    {
      Destroy(this);
    }

    if (MaterialObject != null)
    {
      InstanceID = MaterialObject.root.gameObject.GetInstanceID().ToString();
    }
  }

  public void UpdateCubeMap()
  {
    string path = FileHelper.BuildPath(CubeMapper.FolderToInstallTextures, MaterialObject.root.gameObject.name + InstanceID);
  
    Texture2D tex = Resources.LoadAssetAtPath(FileHelper.BuildPath(path, "Front.png"), typeof(Texture2D)) as Texture2D;
    CubeMapTexture.SetPixels(tex.GetPixels(), CubemapFace.PositiveZ);

    tex = Resources.LoadAssetAtPath(FileHelper.BuildPath(path, "Back.png"), typeof(Texture2D)) as Texture2D;
    CubeMapTexture.SetPixels(tex.GetPixels(), CubemapFace.NegativeZ);

    tex = Resources.LoadAssetAtPath(FileHelper.BuildPath(path, "Left.png"), typeof(Texture2D)) as Texture2D;
    CubeMapTexture.SetPixels(tex.GetPixels(), CubemapFace.NegativeX);

    tex = Resources.LoadAssetAtPath(FileHelper.BuildPath(path, "Right.png"), typeof(Texture2D)) as Texture2D;
    CubeMapTexture.SetPixels(tex.GetPixels(), CubemapFace.PositiveX);

    tex = Resources.LoadAssetAtPath(FileHelper.BuildPath(path, "Up.png"), typeof(Texture2D)) as Texture2D;
    CubeMapTexture.SetPixels(tex.GetPixels(), CubemapFace.PositiveY);

    tex = Resources.LoadAssetAtPath(FileHelper.BuildPath(path, "Down.png"), typeof(Texture2D)) as Texture2D;
    CubeMapTexture.SetPixels(tex.GetPixels(), CubemapFace.NegativeY);

    CubeMapTexture.Apply();

    MaterialObject.renderer.material.SetTexture(TextureName, CubeMapTexture);

    EditorUtility.SetDirty(MaterialObject);
  }
}