using System.Collections.Generic;

public static class PP
{
  public enum ParseModeDuration
  {
    None,
    Single,
    Line,
    Cancelled
  }

  public const string COMMENT_LINE = "//";                           // Indicates that the line should be ignored
  public const string NO_MODE = " ";
  public const string STRING_SPACER = "~";

  // Building Phases
  public const string PHASE_OPEN = "Phase";
  public const string PHASE_CLOSE = "EndPhase";
  public const string GLOBAL_OPEN = "Global";
  public const string GLOBAL_CLOSE = "EndGlobal";

  // Custom Event Setup
  public const string CUSTOM_EVENT_OPEN = "BeginCustomEvent";
  public const string CUSTOM_EVENT_CLOSE = "EndCustomEvent";

  // Creatable Objects
  public const string OBJECT_VARIABLE = "Variable";
  public const string OBJECT_TIMER = "Timer";
  //public const string PP_OBJECT_TRIGGER = "Trigger";
  public const string OBJECT_SOUND = "Sound";
  //public const string PP_OBJECT_CUSTOM_EVENT = "CustomEvent";

  // Variable Types
  public const string VAR_TYPE_INT = "Integer";
  public const string VAR_TYPE_FLOAT = "Float";
  public const string VAR_TYPE_BOOL = "Bool";
  public const string VAR_TYPE_STRING = "String";

  // Predefined Events
  public const string EVENT_BEGIN_PHASE = "OnBeginPhase";
  public const string EVENT_TIMER_COMPLETED = "OnTimerComplete";
  public const string EVENT_ITEM_PICKUP = "OnPickUpItem";
  public const string EVENT_ENTER_TRIGGER = "OnTriggerEnter";
  public const string EVENT_MATH_CONDITION = "OnCondition";

  // Actions
  public const string ACTION_END_GAME = "EndGame";
  public const string ACTION_PLAY_SOUND = "PlaySound";
  public const string ACTION_STOP_SOUND = "StopSound";
  public const string ACTION_ENABLE_TRIGGER = "EnableTrigger";
  public const string ACTION_DISABLE_TRIGGER = "DisableTrigger";
  public const string ACTION_BEGIN_TIMER = "BeginTimer";
  public const string ACTION_END_PHASE = "CompletePhase";
  public const string ACTION_SET_VALUE = "SetValue";
  public const string ACTION_INCREMENT_VALUE = "IncrementValue";
  public const string ACTION_TOGGLE_BOOL = "ToggleBool";
  public const string ACTION_MAKE_INTERACTIBLE = "MakeInteractible";
  public const string ACTION_REMOVE_INTERACTIBLE = "RemoveInteractible";
  public const string ACTION_WAIT = "Wait";
  public const string ACTION_LEGACY_EVENT = "LegacyEvent";
 
  // Parameters
  public const string PARAM_GLOBAL = "Global";
  public const string PARAM_NAME = "Name";
  public const string PARAM_VALUE = "Value";                         // Variable
  public const string PARAM_TYPE = "Type";                           // Variable
  public const string PARAM_DURATION = "Duration";                   // Timer
  public const string PARAM_REPEAT = "RepeatCount";                  // Timer
  public const string PARAM_AUTOSTART = "AutoStart";                 // Timer, Sound
  public const string PARAM_DESTROY_ON_PHASE = "DestroyOnPhase";     // Timer, Variable, Trigger
  public const string PARAM_ACTION = "Action";                       // Predefined Events
  //public const string PP_PARAM_SOUND_LOCATION = "PlayAt";               // Sound
  public const string PARAM_SOUND_ACTOR = "PlayFrom";                // Sound
  public const string PARAM_CONDITION_REQUIREMENT = "ConditionReq";  // Condition
  public const string PARAM_PATH = "Path";
  public const string PARAM_WAIT_TIME = "Time";
  public const string PARAM_EVENT_REQ = "PreReqs";
  public const string PARAM_CONDITION_ITEM = "ItemInScene";          // Condition
  public const string PARAM_USE_COUNT = "Uses";                      // Item Interaction
  public const string PARAM_PERSISTENT = "Persistent";               // Item Interaction

  public const string PARAM_GLOBAL = "Global";
  public const string PARAM_NAME = "Name";
  public const string PARAM_VALUE = "Value";                         // Variable
  public const string PARAM_TYPE = "Type";                           // Variable
  public const string PARAM_DURATION = "Duration";                   // Timer
  public const string PARAM_REPEAT = "RepeatCount";                  // Timer
  public const string PARAM_AUTOSTART = "AutoStart";                 // Timer, Sound
  public const string PARAM_DESTROY_ON_PHASE = "DestroyOnPhase";     // Timer, Variable, Trigger
  public const string PARAM_ACTION = "Action";                       // Predefined Events
  //public const string PP_PARAM_SOUND_LOCATION = "PlayAt";             // Sound
  public const string PARAM_SOUND_ACTOR = "PlayFrom";                // Sound
  public const string PARAM_SOUND_ACTOR_STOP = "StopOn";             // Sound
  public const string PARAM_CONDITION_REQUIREMENT = "ConditionReq";  // Condition
  public const string PARAM_PATH = "Path";
  public const string PARAM_WAIT_TIME = "Time";
  public const string PARAM_EVENT_REQ = "PreReqs";
  public const string PARAM_USE_COUNT = "Uses";                      // Item Interaction
  public const string PARAM_PERSISTENT = "Persistent";               // Item Interaction
  public const string PARAM_LOOP = "Loop";                           // Sound
  
>>>>>>> added stop sound option, some renaming
  // Conditionals
  public const string COND_GT = "GreaterThan";
  public const string COND_LT = "LessThan";
  public const string COND_ET = "EqualTo";
  public const string COND_NET = "NotEqualTo";



  /*private static MultiDictionary<string, string> ValidInputs = new MultiDictionary<string, string>() {
      {NO_MODE, PHASE_OPEN},

      // Phase Mode
      {PHASE_OPEN, PHASE_CLOSE},

      {PHASE_OPEN, OBJECT_SOUND},
      {PHASE_OPEN, OBJECT_TIMER},
      //{PHASE_OPEN, PP_OBJECT_TRIGGER},
      {PHASE_OPEN, OBJECT_VARIABLE},
      {PHASE_OPEN, CUSTOM_EVENT_OPEN},

      {CUSTOM_EVENT_OPEN, CUSTOM_EVENT_CLOSE},
      {CUSTOM_EVENT_OPEN, ACTION_END_PHASE},
      {CUSTOM_EVENT_OPEN, ACTION_MAKE_INTERACTIBLE},
      {CUSTOM_EVENT_OPEN, ACTION_REMOVE_INTERACTIBLE},
      {CUSTOM_EVENT_OPEN, ACTION_PLAY_SOUND},
      {CUSTOM_EVENT_OPEN, ACTION_ENABLE_TRIGGER},
      {CUSTOM_EVENT_OPEN, ACTION_DISABLE_TRIGGER},
      {CUSTOM_EVENT_OPEN, ACTION_INCREMENT_VALUE},
      {CUSTOM_EVENT_OPEN, ACTION_SET_VALUE},
      {CUSTOM_EVENT_OPEN, ACTION_WAIT},
      {CUSTOM_EVENT_OPEN, ACTION_LEGACY_EVENT},

      // Sound Creation Mode
      {OBJECT_SOUND, PARAM_NAME},
      {OBJECT_SOUND, PARAM_AUTOSTART},

      // Timer Creation Mode
      {OBJECT_TIMER, PARAM_GLOBAL},
      {OBJECT_TIMER, PARAM_NAME},
      {OBJECT_TIMER, PARAM_DURATION},
      {OBJECT_TIMER, PARAM_AUTOSTART},
      {OBJECT_TIMER, PARAM_DESTROY_ON_PHASE},
      {OBJECT_TIMER, PARAM_REPEAT},

      // Trigger Creation Mode
      //{PP_OBJECT_TRIGGER, PARAM_NAME},

      // Variable Creation Mode
      {OBJECT_VARIABLE, PARAM_GLOBAL},
      {OBJECT_VARIABLE, PARAM_NAME},
      {OBJECT_VARIABLE, PARAM_VALUE},
      {OBJECT_VARIABLE, PARAM_TYPE},
      {OBJECT_VARIABLE, PARAM_DESTROY_ON_PHASE},

      // Custom Event Creation Mode
      {OBJECT_CUSTOM_EVENT, PARAM_NAME},
      {OBJECT_CUSTOM_EVENT, ACTION_ENABLE_TRIGGER},
      {OBJECT_CUSTOM_EVENT, ACTION_DISABLE_TRIGGER},
      {OBJECT_CUSTOM_EVENT, ACTION_END_PHASE},
      {OBJECT_CUSTOM_EVENT, ACTION_INCREMENT_VALUE},
      {OBJECT_CUSTOM_EVENT, ACTION_SET_VALUE},
      {OBJECT_CUSTOM_EVENT, ACTION_MAKE_INTERACTIBLE},
      {OBJECT_CUSTOM_EVENT, ACTION_REMOVE_INTERACTIBLE},
      {OBJECT_CUSTOM_EVENT, ACTION_PLAY_SOUND},
      {OBJECT_CUSTOM_EVENT, ACTION_WAIT},

      // Trigger Status Mode
      {ACTION_DISABLE_TRIGGER, PARAM_NAME},
      {ACTION_ENABLE_TRIGGER, PARAM_NAME},
      
      // Variable Incrementing Mode
      {ACTION_INCREMENT_VALUE, PARAM_NAME},
      {ACTION_INCREMENT_VALUE, PARAM_VALUE},

      // Variable Setting Mode
      {ACTION_SET_VALUE, PARAM_NAME},
      {ACTION_SET_VALUE, PARAM_VALUE},

      // Sound Play Mode
      {ACTION_PLAY_SOUND, PARAM_NAME},
      {ACTION_PLAY_SOUND, PARAM_SOUND_ACTOR},
      //{ACTION_PLAY_SOUND, PP_PARAM_SOUND_LOCATION},

      // Wait Mode
      {ACTION_WAIT, PARAM_VALUE},

      {ACTION_LEGACY_EVENT, PARAM_NAME},

      // Event Handling Mode
      {EVENT_TIMER_COMPLETED, PARAM_NAME},
      {EVENT_TIMER_COMPLETED, PARAM_ACTION},

      {EVENT_ENTER_TRIGGER, PARAM_NAME},
      {EVENT_ENTER_TRIGGER, PARAM_ACTION},

      {EVENT_MATH_CONDITION, PARAM_NAME},
      {EVENT_MATH_CONDITION, PARAM_CONDITION_REQUIREMENT},
      {EVENT_MATH_CONDITION, PARAM_VALUE},
      {EVENT_MATH_CONDITION, PARAM_ACTION},

      {EVENT_ITEM_PICKUP, PARAM_NAME},
      {EVENT_ITEM_PICKUP, PARAM_ACTION},
  };

  private static Dictionary<string, ParseModeDuration> ParseDurations = new Dictionary<string, ParseModeDuration>() {
    {COMMENT_LINE ,ParseModeDuration.Line},
    {NO_MODE, ParseModeDuration.Single},
    {PHASE_OPEN, ParseModeDuration.Cancelled},
    {PHASE_CLOSE, ParseModeDuration.None},
    {CUSTOM_EVENT_OPEN, ParseModeDuration.Cancelled},
    {CUSTOM_EVENT_CLOSE, ParseModeDuration.None},
    {OBJECT_VARIABLE, ParseModeDuration.Line},
    {OBJECT_TIMER, ParseModeDuration.Line},
    //{PP_OBJECT_TRIGGER, ParseModeDuration.Line},
    {OBJECT_SOUND, ParseModeDuration.Line},
    //{PP_OBJECT_CUSTOM_EVENT, ParseModeDuration.Line},
    {VAR_TYPE_INT, ParseModeDuration.Line},
    {VAR_TYPE_FLOAT, ParseModeDuration.Line},
    {VAR_TYPE_BOOL, ParseModeDuration.Line},
    {VAR_TYPE_STRING, ParseModeDuration.Line},
    {EVENT_TIMER_COMPLETED, ParseModeDuration.Line},
    {EVENT_ITEM_PICKUP, ParseModeDuration.Line},
    {EVENT_ENTER_TRIGGER, ParseModeDuration.Line},
    {EVENT_MATH_CONDITION, ParseModeDuration.Line},
    {ACTION_PLAY_SOUND, ParseModeDuration.Line},
    {ACTION_ENABLE_TRIGGER, ParseModeDuration.Line},
    {ACTION_DISABLE_TRIGGER, ParseModeDuration.Line},
    {ACTION_END_PHASE, ParseModeDuration.Single},
    {ACTION_SET_VALUE, ParseModeDuration.Line},
    {ACTION_INCREMENT_VALUE, ParseModeDuration.Line},
    {ACTION_MAKE_INTERACTIBLE, ParseModeDuration.Line},
    {ACTION_REMOVE_INTERACTIBLE, ParseModeDuration.Line},
    {ACTION_WAIT, ParseModeDuration.Line},
    {PARAM_GLOBAL, ParseModeDuration.Single},
    {PARAM_NAME, ParseModeDuration.Single},
    {PARAM_VALUE, ParseModeDuration.Single},
    {PARAM_TYPE, ParseModeDuration.Single},
    {PARAM_DURATION, ParseModeDuration.Single},
    {PARAM_REPEAT, ParseModeDuration.Single},
    {PARAM_AUTOSTART, ParseModeDuration.Single},
    {PARAM_DESTROY_ON_PHASE, ParseModeDuration.Single},
    {PARAM_ACTION, ParseModeDuration.Single},
    //{PP_PARAM_SOUND_LOCATION, ParseModeDuration.Single},
    {PARAM_SOUND_ACTOR, ParseModeDuration.Single},
    {PARAM_CONDITION_REQUIREMENT, ParseModeDuration.Single},
    {COND_LT, ParseModeDuration.None},
    {COND_ET, ParseModeDuration.None},
    {COND_GT, ParseModeDuration.None}
  };*/

  private static Dictionary<string, System.Type> ParameterTypes = new Dictionary<string, System.Type>() {
    {VAR_TYPE_INT, typeof(int)},
    {VAR_TYPE_FLOAT, typeof(float)},
    {VAR_TYPE_BOOL, typeof(bool)},
    {VAR_TYPE_STRING, typeof(string)},
    {PARAM_GLOBAL, typeof(bool)},
    {PARAM_NAME, typeof(string)},
    {PARAM_VALUE, typeof(float)},
    {PARAM_TYPE, typeof(string)},
    {PARAM_DURATION, typeof(float)},
    {PARAM_REPEAT, typeof(int)},
    {PARAM_AUTOSTART, typeof(bool)},
    {PARAM_DESTROY_ON_PHASE, typeof(bool)},
    {PARAM_ACTION, typeof(string)},
    //{PP_PARAM_SOUND_LOCATION, typeof(string)},
    {PARAM_SOUND_ACTOR, typeof(string)},
    {PARAM_CONDITION_ITEM, typeof(string)},
    {PARAM_SOUND_ACTOR_STOP, typeof(string)},
    {PARAM_CONDITION_REQUIREMENT, typeof(string)},
    {PARAM_PATH, typeof(string)},
    {PARAM_USE_COUNT, typeof(int)},
    {PARAM_PERSISTENT, typeof(bool)},
    {PARAM_LOOP, typeof(bool)},
  };

  private static Dictionary<string, string> ParameterLiterals = new Dictionary<string, string>() {
    {PARAM_GLOBAL, "Global"},
    {PARAM_NAME, "Name"},
    {PARAM_VALUE, "Value"},
    {PARAM_DURATION, "Duration"},
    {PARAM_REPEAT, "RepeatCount"},
    {PARAM_AUTOSTART, "IsRunning"},
    {PARAM_DESTROY_ON_PHASE, "DestroyOnPhase"},
    {PARAM_ACTION, "Action"},
    //{PP_PARAM_SOUND_LOCATION, "SoundLocation"},
    {PARAM_SOUND_ACTOR, "Actor"},
    {PARAM_SOUND_ACTOR_STOP, "Actor"},
    {PARAM_CONDITION_REQUIREMENT, "Condition"},
    {PARAM_WAIT_TIME, "Value"}
  };

  /*public static bool IsValidForMode(string mode, string param)
  {
    if (ValidInputs.HasKey(mode))
    {
      string[] validParams = ValidInputs.GetValues(mode);
      for (int i = 0; i < validParams.Length; ++i)
      {
        if (validParams[i] == param)
          return true;
      }
    }
    return false;
  }*/

  /*public static ParseModeDuration GetModeDuration(string mode)
  {
    return ParseDurations[mode];
  }*/

  public static System.Type GetParameterType(string param)
  {
    if (ParameterTypes.ContainsKey(param))
      return ParameterTypes[param];
    else
      UnityEngine.Debug.LogError("Couldn't find Parameter type for " + param);
    return typeof(string);
  }

  public static string GetParameterLiteralName(string param)
  {
    if (ParameterLiterals.ContainsKey(param))
      return ParameterLiterals[param];
    else
      return param;
  }

  public static string GetSpacedString(string parsed)
  {
    return parsed.Replace(STRING_SPACER, " ");
  }
}
