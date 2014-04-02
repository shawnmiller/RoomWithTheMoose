using UnityEngine;
using System.Collections.Generic;

public class VariableManager : Singleton<VariableManager>
{
  List<Variable> Variables = new List<Variable>();

  public void AddVariable(Variable var)
  {
    if (Variables.IndexOf(var) == -1)
    {
      Variables.Add(var);
    }
  }

  public Variable GetVariable(string name)
  {
    return Variables.Find(x => x.Name == name);
  }

  public void PushGlobalEvent(string eventName, string objectName)
  {
    if (eventName == PPS.PP_EVENT_BEGIN_PHASE)
    {
      PurgeNonGlobals();
    }
  }

  private void PurgeNonGlobals()
  {
    foreach (Variable var in Variables)
    {
      if (var.Global == false)
      {
        Variables.Remove(var);
      }
    }
  }
}