using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class CubeMapper : MonoBehaviour
{
  public const string CUBEMAP_TAG = "CubeMapPoint";
  public static string FolderToInstallTextures = @"C:\Users";
  public int CubeMapSize = 512;
  public bool GenerateOnRun = false;
  public Cubemap Tex;

  protected class LocalRotations
  {
    public Quaternion Front;
    public Quaternion Back;
    public Quaternion Left;
    public Quaternion Right;
    public Quaternion Up;
    public Quaternion Down;

    public LocalRotations(Transform obj)
    {
      Front = Quaternion.LookRotation(obj.forward);
      Back = Quaternion.LookRotation(-obj.forward);
      Left = Quaternion.LookRotation(Vector3.Cross(obj.up, obj.forward));
      Right = Quaternion.LookRotation(Vector3.Cross(obj.forward, obj.up));
      Up = Quaternion.LookRotation(obj.up);
      Down = Quaternion.LookRotation(-obj.up);
    }
  }

  void Start()
  {
    if (GenerateOnRun)
    {
      Clean();
      Run();
    }
  }

  public void Run()
  {
    Screen.SetResolution(CubeMapSize, CubeMapSize, false);

    // Set up camera
    GameObject cam = new GameObject();
    cam.AddComponent<Camera>();
    cam.camera.fieldOfView = 90;
    cam.camera.nearClipPlane = 0.01f;

    GameObject[] points = GameObject.FindGameObjectsWithTag(CUBEMAP_TAG);


    string LogFilePath = FileHelper.BuildPath(FolderToInstallTextures, "log.txt");
    File.Delete(LogFilePath);
    StreamWriter LogFile = new StreamWriter(LogFilePath);
    foreach (GameObject p in points)
    {
      string Dir = FileHelper.BuildPath(FolderToInstallTextures, p.name, p.GetInstanceID().ToString());

      if (!Directory.Exists(Dir))
      {
        Directory.CreateDirectory(Dir);
      }
      else
      {
        Dir += p.GetInstanceID();
      }

      LogFile.WriteLine(p.name + " @ " + p.transform.position.ToString() + " Written To: " + Dir);

      cam.transform.position = p.transform.position;
      LocalRotations rotations = new LocalRotations(p.transform);
      
      // Front
      cam.transform.rotation = rotations.Front;
      Application.CaptureScreenshot(FileHelper.BuildPath(Dir, "Front.png"));

      // Back
      cam.transform.rotation = rotations.Back;
      Application.CaptureScreenshot(FileHelper.BuildPath(Dir, "Back.png"));

      // Left
      cam.transform.rotation = rotations.Left;
      Application.CaptureScreenshot(FileHelper.BuildPath(Dir, "Left.png"));

      // Right
      cam.transform.rotation = rotations.Right;
      Application.CaptureScreenshot(FileHelper.BuildPath(Dir, "Right.png"));

      // Up
      cam.transform.rotation = rotations.Up;
      Application.CaptureScreenshot(FileHelper.BuildPath(Dir, "Up.png"));

      // Down
      cam.transform.rotation = rotations.Down;
      Application.CaptureScreenshot(FileHelper.BuildPath(Dir, "Down.png"));
    }
    LogFile.Close();
  }

  public void Clean()
  {

  }

  bool ValidateObjects(GameObject[] objects)
  {
    List<GameObject> existing = new List<GameObject>();
    foreach (GameObject o in objects)
    {
      if (existing.Contains(o))
      {
        return false;
      }
    }
    return true;
  }
}