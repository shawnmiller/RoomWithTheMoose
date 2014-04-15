using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;

public class EventStepDataWindow : EditorWindow
{
  private EventStepData data;
  private object parent;

  private GameObject helper;
  private GameObject defaultHelper;

  private List<string> context;

  public void Edit(object parent, EventStepData data)
  {
    this.parent = parent;
    this.data = data;
    this.defaultHelper = Resources.LoadAssetAtPath("Assets/RwtM/Prefabs/Editor/EventHelper.prefab", typeof(GameObject)) as GameObject;
    this.context = EventStepDataContext.GetContext(data.type);
  }

  void OnGUI()
  {
    if (parent == null)
    {
      this.Close();
    }

    if (data != null)
    {
      if (context.Contains("Prefab"))
      {
        data.prefab = (GameObject)(EditorGUILayout.ObjectField("Prefab", data.prefab, typeof(GameObject), false));
      }

      if (context.Contains("ID"))
      {
        data.associatedID = EditorGUILayout.IntField("ID", data.associatedID);
      }

      if (context.Contains("Position") || context.Contains("Rotation"))
      {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Spawn Helper"))
        {
          this.helper = Instantiate((data.prefab != null ? data.prefab : this.defaultHelper), data.position, data.rotation) as GameObject;
        }
        if (GUILayout.Button("Get Values"))
        {
          if (this.helper != null)
          {
            data.position = this.helper.transform.position;
            data.rotation = this.helper.transform.rotation;
            DestroyImmediate(helper);
          }
          else
          {
            Debug.LogError("No helper was found in the level.");
          }
        }
        GUILayout.EndHorizontal();
      }

      if (context.Contains("Animation"))
      {
        data.animation = EditorGUILayout.TextField("Animation", data.animation);
      }

      if (context.Contains("SoundClip"))
      {
        data.sound = (AudioClip)(EditorGUILayout.ObjectField("Sound", data.sound, typeof(AudioClip)));
      }

      if (context.Contains("StartTime"))
      {
        data.startTime = EditorGUILayout.FloatField("Start Time", data.startTime);
      }

      if (context.Contains("Duration"))
      {
        data.duration = EditorGUILayout.FloatField("Duration", data.duration);
      }

      if (context.Contains("Speed"))
      {
        data.speed = EditorGUILayout.FloatField("Speed", data.speed);
      }

      if (context.Contains("Toggle"))
      {
        data.toggle = EditorGUILayout.Toggle("Toggle", data.toggle);
      }
    }
  }

  void OnDestroy()
  {
    data = null;
    DestroyImmediate(helper);
  }
}