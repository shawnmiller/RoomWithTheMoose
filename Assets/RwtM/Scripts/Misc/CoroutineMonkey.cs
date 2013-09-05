using UnityEngine;
using System;
using System.Reflection;
using System.Collections;

public class CoroutineMonkey : MonoBehaviour
{
  public void RunCoroutine(string name, object runFor)
  {
    Type scriptType = runFor.GetType();
    MethodInfo method = scriptType.GetMethod(name, BindingFlags.Public | BindingFlags.InvokeMethod);
    //StartCoroutine(((IEnumerator)method).Invoke(runFor));
  }
}