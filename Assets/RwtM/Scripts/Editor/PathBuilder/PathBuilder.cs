using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PathBuilder : Editor
{
  [MenuItem(@"Moose/Build Paths")]
  static void Init()
  {
    PathManager pathManager = PathManager.Get();

    PathNode[] pathNodes = GameObject.FindObjectsOfType(typeof(PathNode)) as PathNode[];

    for (int i = 0; i < pathNodes.Length; ++i)
    {
      pathManager.AddNode(pathNodes[i]);
    }
    pathManager.BuildPaths();
    pathManager.DrawConnections();
    EditorUtility.SetDirty(pathManager);
  }
}