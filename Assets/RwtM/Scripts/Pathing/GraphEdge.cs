public struct GraphEdge
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