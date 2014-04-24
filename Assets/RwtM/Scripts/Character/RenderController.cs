using UnityEngine;
using System.Collections.Generic;

public class RenderController : Singleton<RenderController>
{
  private  List<RenderSphere> RenderSpheres = new List<RenderSphere>();
  private List<Transform> RenderList = new List<Transform>();

  void Start()
  {
    // Turn errythang off...well not the player, but whatever.
    Transform[] SceneObjects = GameObject.FindObjectsOfType(typeof(Transform)) as Transform[];

    for (int i = 0; i < SceneObjects.Length; ++i)
    {
      if (SceneObjects[i].root.tag != "Player")
      {
        SetRenderer(SceneObjects[i], false);
      }
    }
  }

  private void SetRenderer(Transform Render, bool ShouldRender)
  {
    try
    {
      Render.renderer.enabled = ShouldRender;
    }
    catch
    { } // Object doesn't have a renderer, all good.
  }

  public void NewRenderStatus(Transform Object, bool ShouldRender)
  {
    if (ShouldRender)
      SetRenderer(Object, true);

    else
    {
      int ReferenceCount = 0;
      foreach (RenderSphere Sphere in RenderSpheres)
      {
        if (Sphere.Contains(Object))
          ++ReferenceCount;
      }

      // If there is no RenderSpheres which have this Transform in their collision list, it's not in range.
      if (ReferenceCount < 1)
        SetRenderer(Object, false);
    }
  }
}