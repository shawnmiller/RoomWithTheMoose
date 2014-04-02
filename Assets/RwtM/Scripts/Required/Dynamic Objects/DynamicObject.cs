public class DynamicObject
{
  private bool global = false;
  public bool Global { get { return global; } set { global = value; } }
  public string Name { get; set; }
}