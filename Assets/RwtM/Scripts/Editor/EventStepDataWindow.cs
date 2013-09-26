using UnityEngine;
using UnityEditor;
using System.Reflection;

public class EventStepDataWindow : EditorWindow
{
  private EventStepData data;
  private EventEditor parent;

  public void Edit(EventEditor parent, EventStepData data)
  {
    this.parent = parent;
    this.data = data;
  }

  void OnGUI()
  {
    if (parent == null)
    {
      this.Close();
    }

    if (data != null)
    {
      data.prefab = (GameObject)(EditorGUILayout.ObjectField("Prefab", data.prefab, typeof(GameObject), false));
      data.associatedID = EditorGUILayout.IntField("ID", data.associatedID);
      data.position = EditorGUILayout.Vector3Field("Position", data.position);
      data.rotation = Quaternion.Euler(EditorGUILayout.Vector3Field("Rotation", data.rotation.eulerAngles));
      data.animation = EditorGUILayout.TextField("Animation", data.animation);
      data.sound = (AudioClip)(EditorGUILayout.ObjectField("Sound", data.sound, typeof(AudioClip)));
      data.startTime = EditorGUILayout.FloatField("Start Time", data.startTime);
      data.duration = EditorGUILayout.FloatField("Duration", data.duration);
      data.speed = EditorGUILayout.FloatField("Speed", data.speed);
      data.toggle = EditorGUILayout.Toggle("Toggle", data.toggle);
    }
  }

  void OnDestroy()
  {
    data = null;
  }
}