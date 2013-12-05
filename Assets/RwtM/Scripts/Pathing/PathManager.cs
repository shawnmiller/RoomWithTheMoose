using UnityEngine;
using System.Collections.Generic;

public class PathManager : Singleton<PathManager>
{
  private Dictionary<string, Path> Paths = new Dictionary<string, Path>();
  //private List<Path> Paths = new List<Path>();

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
    }
  }

  private void Purge()
  {
    Paths.Clear();
  }
}