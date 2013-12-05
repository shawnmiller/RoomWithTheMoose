using System;
using System.Collections.Generic;

[System.Serializable]
public class Graph<T>
{
  List<GraphEdge> Edges;
  List<T> Nodes;

  public Graph()
  {
    Edges = new List<GraphEdge>();
    Nodes = new List<T>();
  }

  public List<GraphEdge> GetEdges()
  {
    return Edges;
  }

  public T GetNodeByIndex(int index)
  {
    return Nodes[index];
  }

  public void AddEdge(T a, T b)
  {
    Nodes.ExclusiveAdd(a);
    Nodes.ExclusiveAdd(b);

    GraphEdge newEdge = new GraphEdge();
    newEdge.PointA = Nodes.IndexOf(a);
    newEdge.PointB = Nodes.IndexOf(b);
    Edges.ExclusiveAdd(newEdge);
  }

  public List<T> FindShortestPath(T from, T to)
  {
    List<AStarNodeData> open = new List<AStarNodeData>();
    List<AStarNodeData> closed = new List<AStarNodeData>();

    AStarNodeData target = new AStarNodeData(to);
    target.Parent = null;
    target.F = 0;
    target.G = 0;
    target.H = 0;

    AStarNodeData current = new AStarNodeData(from);
    current.Parent = null;
    current.F = 0;
    current.G = 0;
    current.H = 0;

    do
    {
      if (current.Equals(target))
      {
        break;
      }

    } while (!(open.Count == 0));

    throw new System.NotImplementedException();
  }
}