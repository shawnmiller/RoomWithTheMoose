using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PathManager : Singleton<PathManager>
{
  public bool IsDebugging = true;

  void Update()
  {
    Debug.Log("Working...");
    if (IsDebugging)
    {
      foreach (string s in PathNames)
      {
        Path cPath = Paths[s];
        Graph<PathNode> cGraph = cPath.GetGraph();
        foreach (GraphEdge edge in cGraph.GetEdges())
        {
          
        }
      }
    }
  }
  public void DrawConnections()
  {
    Debug.Log("Drawing");
    foreach (string s in PathNames)
    {
      Path cPath = Paths[s];
      Graph<PathNode> cGraph = cPath.GetGraph();
      foreach (GraphEdge edge in cGraph.GetEdges())
      {
        Vector3 p1, p2;
        p1 = cGraph.GetNodeByIndex(edge.PointA).transform.position;
        p2 = cGraph.GetNodeByIndex(edge.PointB).transform.position;
        Debug.Log("Drawing: " + p1 + " and " + p2);
        Debug.DrawLine(p1, p2, Color.red, 10f, true);
      }
    }
  }

  private Dictionary<string, Path> Paths = new Dictionary<string, Path>();
  private List<string> PathNames = new List<string>();

  public Path GetPathByName(string name)
  {
    if (Paths.ContainsKey(name))
    {
      return Paths[name];
    }
    /*int index = Paths.FindIndex(x => x.Name == name);
    if (index != -1)
    {
      return Paths[index];
    }*/
    return null;
  }

  public void AddPath(Path newPath)
  {
    if (!Paths.ContainsKey(newPath.Name))
    {
      Paths.Add(newPath.Name, newPath);
      PathNames.Add(newPath.Name);
    }
  }

  public void AddNode(PathNode newNode)
  {
    if (Paths.ContainsKey(newNode.PathName))
    {
      Paths[newNode.PathName].AddNode(newNode);
    }
    else
    {
      Paths.Add(newNode.PathName, new Path(newNode.PathName));
      Paths[newNode.PathName].AddNode(newNode);
      PathNames.Add(newNode.PathName);
    }
  }

  public void BuildPaths()
  {
    Debug.Log("Building Paths");
    foreach (string s in PathNames)
    {
      Paths[s].BuildPath();
    }
  }

  private void Purge()
  {
    Paths.Clear();
  }
}