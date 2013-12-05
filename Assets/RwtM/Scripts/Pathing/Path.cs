using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[ExecuteInEditMode]
public class Path
{
  const bool VerboseMode = true;
  public string Name;
  private List<PathNode> Nodes = new List<PathNode>();
  private Graph<PathNode> TraversalPath = new Graph<PathNode>();

  public Graph<PathNode> GetGraph()
  {
    return TraversalPath;
  }

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
    if (VerboseMode)
    {
      Debug.Log("Building Path: " + this.Name);
      Debug.Log("Total # of Nodes: " + Nodes.Count);
      Debug.Log("Purging Old Graph...");
    }

    PurgeGraph();

    List<PathNode> exclusionList = new List<PathNode>();

    foreach(PathNode node in Nodes)
    {
      exclusionList.Add(node);
      foreach (PathNode endNode in Nodes.MutuallyExclusivePick(exclusionList))
      {
        if (VerboseMode)
        {
          Debug.Log("Occlusion Test: " + node.GetInstanceID() + " + " + endNode.GetInstanceID());
        }

        if (!Physics.Raycast(node.transform.position, endNode.transform.position - node.transform.position, Vector3.Distance(node.transform.position, endNode.transform.position)))
        {
          TraversalPath.AddEdge(node, endNode);
          if (VerboseMode)
          {
            Debug.Log("Not Occluded, Edge Made");
          }
        }
        else
        {
          if (VerboseMode)
          {
            Debug.Log("Occluded Nodes, No Edge Made");
          }
        }
      }
    }
  }

  void PurgeGraph()
  {
    TraversalPath = new Graph<PathNode>();
  }
}