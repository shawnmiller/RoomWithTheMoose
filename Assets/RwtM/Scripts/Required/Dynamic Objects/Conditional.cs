using UnityEngine;

public class Conditional : DynamicObject
{
  //public string Name { get; set; }
  private Variable Var { get; set; } // cached version of the Variable
  public string Value { get; set; }
  private Variable Val { get; set; }

  public string Condition { get; set; }
  public float Comparer { get; set; }
  public string Action { get; set; }

  private bool runOnce = false;

  public bool ConditionMet()
  {
    if (!runOnce)
    {
      Debug.Log("Running Variable Fetch: " + Name);
      CheckForValueOrName();
      runOnce = true;
      Debug.Log("Variable Found: " + (Var != null || Val != null));
    }

    if (!ValidateConditional())
    {
      return false;
    }

    if (Var == null)
    {
      Var = VariableManager.Get().GetVariable(Name);
      if (Var == null)
      {
        Debug.Log("Can't find Variable \"" + Name + "\"");
        return false;
      }
    }

    object TestActual = Comparer;
    if (Val != null)
    {
      TestActual = Val.Value;
    }

    // Normally I wouldn't do something like this, but since we're forcing the validation
    // of the Conditional before executing this, we can convert everything down to a
    // float and comparisons should work.
    switch (Condition)
    {
      case ConditionalType.LessThan:
        return (float)Var.Value < (float)TestActual;
      case ConditionalType.EqualTo:
        return (float)Var.Value == (float)TestActual;
      case ConditionalType.NotEqualTo:
        return !((float)Var.Value == (float)TestActual);
      case ConditionalType.GreaterThan:
        return (float)Var.Value > (float)TestActual;
      default:
        return false;
    }
  }

  private bool ValidateConditional()
  {
    if(Var == null)
    {
      Debug.LogError("Attempted to compare a Variable which was not created.");
      Debug.Break();
    }
    //Debug.Log("Var Type " + Var.Type.ToString());
    // This is going to look ugly...
    switch (Var.Type)
    {
      case "Bool":
        if (Condition != ConditionalType.EqualTo && Condition != ConditionalType.NotEqualTo)
        {
          Debug.LogError("Conditional attempted to compare a Bool (" + Var.Name + ") with LessThan or GreaterThan.");
          Debug.LogError(Condition);
          Debug.Break();
          return false;
        }
        return true;
      case "String":
        Debug.LogError("Conditionals currently do not support Strings. (" + Var.Name + ")");
        Debug.Break();
        return false;
      case "Integer":
      case "Float":
      default:
        return true;
    }
  }

  private void CheckForValueOrName()
  {
    Var = ObjectController.Get().GetObject<Variable>(ObjectCategories.Variable, Name);
    if (Val == null)
    {
      Comparer = (float)TypeConversion.Convert(typeof(float), Value);
    }
  }
}