public class AStarNodeData
{
  public object Self; // a reference to the object this node represents
  public AStarNodeData Parent;
  public int F; // cached G + G
  public int G; // weight to parent
  public int H; // heuristic weight

  public AStarNodeData(object nodeObj)
  {
    this.Self = nodeObj;
    this.Parent = null;
    this.F = 0;
    this.G = 0;
    this.H = 0;
  }

  public void CalculateFGH(int heuristicValue)
  {
    H = heuristicValue;
    if (this.Parent != null)
    {

    }
  }

  public override bool Equals(object obj)
  {
    return this.Self == ((AStarNodeData)obj).Self;
  }
};