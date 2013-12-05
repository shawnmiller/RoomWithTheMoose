using System;
using System.Collections.Generic;

[System.Serializable]
public class Graph<T>
{
  protected struct GraphEdge
  {
    private int _a;
    private int _b;
    private float _weight;
    public int PointA
    {
      get { return _a; }
      set { _a = value; }
    }
    public int PointB
    {
      get { return _b; }
      set { _b = value; }
    }
    public float Weight
    {
      get { return _weight; }
      set { _weight = value; }
    }

    public bool Contains(int index)
    {
      return _a == index || _b == index;
    }

    public override bool Equals(object obj)
    {
      GraphEdge t = (GraphEdge)obj;
 	    return this._a == t._a && this._b == t._b && this._weight == t._weight;
    }

    public static bool operator ==(GraphEdge x, GraphEdge y)
    {
      return x._a == y._a && x._b == y._b && x._weight == y._weight;
    }
    public static bool operator !=(GraphEdge x, GraphEdge y)
    {
      return !(x._a == y._a) || !(x._b == y._b) || !(x._weight == y._weight);
    }
  }

  List<GraphEdge> Edges;
  List<T> Nodes;

  public Graph()
  {
    Edges = new List<GraphEdge>();
    Nodes = new List<T>();
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