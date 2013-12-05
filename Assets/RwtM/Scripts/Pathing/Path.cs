using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[ExecuteInEditMode]
public class Path
{
  public string Name;
  private List<PathNode> Nodes = new List<PathNode>();
  private Graph<PathNode> TraversalPath = new Graph<PathNode>();

  public Path(string name)
  {
    this.Name = name;
  }

  public void AddNode(PathNode newNode)
  {
    Nodes.ExclusiveAdd(newNode);
  }

  public void BuildPath()
  {
    List<PathNode> exclusionList = new List<PathNode>();

    foreach(PathNode node in Nodes)
    {
      foreach (PathNode endNode in Nodes.MutuallyExclusivePick(exclusionList))
      {
        exclusionList.Add(node);
        if (!Physics.Raycast(node.transform.position, endNode.transform.position - node.transform.position, Vector3.Distance(node.transform.position, endNode.transform.position)))
        {
          // add edge
        }
      }
    }
  }
}