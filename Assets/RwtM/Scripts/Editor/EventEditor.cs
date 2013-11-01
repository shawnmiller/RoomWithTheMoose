using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class EventEditor : EditorWindow
{
  private Event currentEvent;
  private List<EventStepData> eventSteps;

  private Vector2 scrollPosition;

  private const int barHeight = 40;

  private EventStepDataWindow stepDataWindow;
  private EditorTimeline timeline;

  [MenuItem("Moose/Event Editor")]
  static void Init()
  {
    EventEditor eventEd = EditorWindow.GetWindow<EventEditor>();
    if (Selection.activeGameObject != null)
    {
      eventEd.currentEvent = Selection.activeGameObject.GetComponent<Event>();
    }
  }

  void Update()
  {
    Vector2 topleft = new Vector2(270, 0);

    if (timeline != null)
    {
      timeline.ResizeGrid(new Rect(topleft.x, topleft.y,
                                   position.width - topleft.x - 20, position.height - topleft.y - 20));
    }
  }

  // Update is called once per frame
  void OnGUI()
  {
    // Boring loading stuff.
    EditorGUILayout.BeginHorizontal();
    currentEvent = EditorGUILayout.ObjectField("Event", currentEvent, typeof(Event), true) as Event;
    if (GUILayout.Button("Load"))
    {
      LoadEventData();
    }
    if (GUILayout.Button("Save"))
    {
      SaveEventData();
    }
    EditorGUILayout.EndHorizontal();

    if (eventSteps != null)
    {
      scrollPosition = GUILayout.BeginScrollView(scrollPosition);
      for(int i=0; i<eventSteps.Count; ++i)// (EventStepData data in eventSteps)
      {
        EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField(data.type.ToString(), GUILayout.Width(300), GUILayout.Height(barHeight));
        string[] names = Enum.GetNames(typeof(EventType));
        eventSteps[i].type = (EventType)EditorGUILayout.Popup((int)eventSteps[i].type, names, GUILayout.Width(150), GUILayout.Height(barHeight));
        if (GUILayout.Button("X", GUILayout.Width(20)))
        {
          eventSteps.Remove(eventSteps[i]);
          break;
        }
        if (GUILayout.Button("Edit", GUILayout.Width(80)))
        {
          stepDataWindow = EditorWindow.GetWindow<EventStepDataWindow>();
          stepDataWindow.Edit(this, eventSteps[i]);
        }
        
        EditorGUILayout.EndHorizontal();
      }

      // Draw the timeline
      if(timeline != null)
      {
        timeline.Draw();
      }
      GUILayout.EndScrollView();
      
      if (GUILayout.Button("Add", GUILayout.Width(40)))
      {
        eventSteps.Add(new EventStepData());
      }
    }
  }

  void LoadEventData()
  {
    if (currentEvent != null)
    {
      eventSteps = currentEvent.tempData;
      timeline = new EditorTimeline(new Rect(200, 50, position.width - 220, position.height - 70),
                                    10f, 0f, 10f, barHeight);
      //eventSteps = new List<EventStepData>();
      //eventSteps.AddRange(currentEvent.tempData.ToArray());
    }
    else
    {
      Debug.LogError("Tried to load event data, but no object was found.");
    }
  }

  void SaveEventData()
  {
    if (currentEvent != null)
    {
      Debug.Log("Saving Event");
      eventSteps.Sort();
      currentEvent.tempData = eventSteps;
      EditorUtility.SetDirty(currentEvent);
    }
  }
}
