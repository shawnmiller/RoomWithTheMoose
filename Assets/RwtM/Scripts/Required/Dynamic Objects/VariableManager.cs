using UnityEngine;
using System.Collections.Generic;

public class VariableManager : Singleton<VariableManager>, IMessageReceiver
{
  List<Variable> Variables = new List<Variable>();

  void Start()
  {
    MessageDispatch.RegisterReceiver(this);
  }

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

  void IMessageReceiver.PushGlobalEvent(string EventName, string Instigator)
  {
    Debug.Log("VariableManager received event: " + EventName + " Instigator: " + Instigator);
    if (EventName == PPS.PP_EVENT_BEGIN_PHASE)
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