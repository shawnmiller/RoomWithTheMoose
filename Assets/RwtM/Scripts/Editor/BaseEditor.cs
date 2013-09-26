using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

public class BaseEditor<T> : Editor
  where T : MonoBehaviour
{

  override public void OnInspectorGUI()
  {
    T data = (T)target;

    GUIContent label = new GUIContent();
    label.text = "Properties";

    DrawDefaultInspectors(label, data);

    if (GUI.changed)
    {
      EditorUtility.SetDirty(target);
    }
  }

  public static void DrawDefaultInspectors<T>(GUIContent label, T target)
    where T : class
  {
    EditorGUILayout.Separator();
    Type type = typeof(T);
    FieldInfo[] fields = type.GetFields();
    ++EditorGUI.indentLevel;

    foreach (FieldInfo field in fields)
    {
      if (field.IsPublic)
      {
        if (field.FieldType == typeof(int) || field.FieldType == typeof(uint))
        {

        }
        else if (field.FieldType == typeof(float))
        {

        }
        else if (field.FieldType == typeof(double))
        {

        }
        else if (field.FieldType == typeof(bool))
        {
          
        }
        else if (field.FieldType == typeof(char))
        {

        }
        else if (field.FieldType == typeof(string))
        {

        }
        else if (field.FieldType.IsClass)
        {
          Type[] parmTypes = new Type[] { field.FieldType };

          string methodName = "DrawDefaultInspectors";

          MethodInfo drawMethod =
             typeof(EditorGUILayout).GetMethod(methodName);

          if (drawMethod == null)
          {
            Debug.LogError("No method found: " + methodName);
          }

          bool foldOut = true;

          drawMethod.MakeGenericMethod(parmTypes).Invoke(null,
             new object[]
               {
                  MakeLabel(field),
                  field.GetValue(target)
               });
        }
        else
        {
          Debug.LogError(
             "DrawDefaultInspectors does not support fields of type " +
             field.FieldType);
        }
        //etc
      }
    }

    --EditorGUI.indentLevel;
  }

  private static GUIContent MakeLabel(FieldInfo field)
  {
    GUIContent guiContent = new GUIContent();
    /*guiContent.text = field.Name.SplitCamelCase();
    object[] descriptions =
       field.GetCustomAttributes(typeof(DescriptionAttribute), true);

    if (descriptions.Length > 0)
    {
      //just use the first one.
      guiContent.tooltip =
         (descriptions[0] as DescriptionAttribute).Description;
    }*/

    return guiContent;
  }
}
