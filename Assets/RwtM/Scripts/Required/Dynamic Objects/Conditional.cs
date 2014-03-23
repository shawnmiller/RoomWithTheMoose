using UnityEngine;

public class Conditional
{
  public Variable Var { get; set; }
  public ConditionalType Condition { get; set; }
  public float Comparer { get; set; }
  public string Action { get; set; }

  public bool ConditionMet()
  {
    if (!ValidateConditional())
    {
      return false;
    }

    // Normally I wouldn't do something like this, but since we're forcing the validation
    // of the Conditional before executing this, we can convert everything down to a
    // float and comparisons should work.
    switch (Condition)
    {
      case ConditionalType.LessThan:
        return (float)Var.Value < Comparer;
      case ConditionalType.EqualTo:
        return (float)Var.Value == Comparer;
      case ConditionalType.NotEqualTo:
        return !((float)Var.Value == Comparer);
      case ConditionalType.GreaterThan:
        return (float)Var.Value > Comparer;
      default:
        return false;
    }
  }

  private bool ValidateConditional()
  {
    if(Var != null)
    {
      Debug.LogError("Attempted to compare a Variable which was not created.");
      Debug.Break();
    }
    
    // This is going to look ugly...
    switch (Var.Type)
    {
      case "Bool":
        if (Condition != ConditionalType.EqualTo || Condition != ConditionalType.NotEqualTo)
        {
          Debug.LogError("Conditional attempted to compare a Bool (" + Var.Name + ") with LessThan or GreaterThan.");
          Debug.Break();
          return false;
        }
        return true;
      case "String":
        Debug.LogError("Conditionals currently do not support Strings. (" + Var.Name + ")");
        Debug.Break();
        /*if (Condition != ConditionalType.EqualTo || Condition != ConditionalType.NotEqualTo)
        {
          Debug.LogError("Conditional attempted to compare a String (" + Var.Name + ") with LessThan or GreaterThan.");
        }*/
        return false;
      case "Integer":
      case "Float":
      default:
        return true;
    }
  }
}