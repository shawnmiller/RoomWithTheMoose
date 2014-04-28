using System.Collections.Generic;

public static class PPS
{
  public enum ParseModeDuration
  {
    None,
    Single,
    Line,
    Cancelled
  }

  public const string PP_COMMENT_LINE = "//";                           // Indicates that the line should be ignored
  public const string PP_NO_MODE = " ";
  public const string PP_STRING_SPACER = "~";

  // Building Phases
  public const string PP_PHASE_OPEN = "Phase";
  public const string PP_PHASE_CLOSE = "EndPhase";
  public const string PP_GLOBAL_OPEN = "Global";
  public const string PP_GLOBAL_CLOSE = "EndGlobal";

  // Custom Event Setup
  public const string PP_CUSTOM_EVENT_OPEN = "BeginCustomEvent";
  public const string PP_CUSTOM_EVENT_CLOSE = "EndCustomEvent";

  // Creatable Objects
  public const string PP_OBJECT_VARIABLE = "Variable";
  public const string PP_OBJECT_TIMER = "Timer";
  //public const string PP_OBJECT_TRIGGER = "Trigger";
  public const string PP_OBJECT_SOUND = "Sound";
  //public const string PP_OBJECT_CUSTOM_EVENT = "CustomEvent";

  // Variable Types
  public const string PP_VAR_TYPE_INT = "Integer";
  public const string PP_VAR_TYPE_FLOAT = "Float";
  public const string PP_VAR_TYPE_BOOL = "Bool";
  public const string PP_VAR_TYPE_STRING = "String";

  // Predefined Events
  public const string PP_EVENT_BEGIN_PHASE = "OnBeginPhase";
  public const string PP_EVENT_TIMER_COMPLETED = "OnTimerComplete";
  public const string PP_EVENT_ITEM_PICKUP = "OnPickUpItem";
  public const string PP_EVENT_ENTER_TRIGGER = "OnTriggerEnter";
  public const string PP_EVENT_MATH_CONDITION = "OnCondition";

  // Actions
  public const string PP_ACTION_END_GAME = "EndGame";
  public const string PP_ACTION_PLAY_SOUND = "PlaySound";
  public const string PP_ACTION_ENABLE_TRIGGER = "EnableTrigger";
  public const string PP_ACTION_DISABLE_TRIGGER = "DisableTrigger";
  public const string PP_ACTION_BEGIN_TIMER = "BeginTimer";
  public const string PP_ACTION_END_PHASE = "CompletePhase";
  public const string PP_ACTION_SET_VALUE = "SetValue";
  public const string PP_ACTION_INCREMENT_VALUE = "IncrementValue";
  public const string PP_ACTION_TOGGLE_BOOL = "ToggleBool";
  public const string PP_ACTION_MAKE_INTERACTIBLE = "MakeInteractible";
  public const string PP_ACTION_REMOVE_INTERACTIBLE = "RemoveInteractible";
  public const string PP_ACTION_WAIT = "Wait";
  public const string PP_ACTION_LEGACY_EVENT = "LegacyEvent";
 
  // Parameters
  public const string PP_PARAM_GLOBAL = "Global";
  public const string PP_PARAM_NAME = "Name";
  public const string PP_PARAM_VALUE = "Value";                         // Variable
  public const string PP_PARAM_TYPE = "Type";                           // Variable
  public const string PP_PARAM_DURATION = "Duration";                   // Timer
  public const string PP_PARAM_REPEAT = "RepeatCount";                  // Timer
  public const string PP_PARAM_AUTOSTART = "AutoStart";                 // Timer, Sound
  public const string PP_PARAM_DESTROY_ON_PHASE = "DestroyOnPhase";     // Timer, Variable, Trigger
  public const string PP_PARAM_ACTION = "Action";                       // Predefined Events
  //public const string PP_PARAM_SOUND_LOCATION = "PlayAt";               // Sound
  public const string PP_PARAM_SOUND_ACTOR = "PlayFrom";                // Sound
  public const string PP_PARAM_CONDITION_REQUIREMENT = "ConditionReq";  // Condition
  public const string PP_PARAM_PATH = "Path";
  public const string PP_PARAM_WAIT_TIME = "Time";
  public const string PP_PARAM_EVENT_REQ = "PreReqs";
  public const string PP_PARAM_CONDITION_ITEM = "ItemInScene";          // Condition

  // Conditionals
  public const string PP_COND_GT = "GreaterThan";
  public const string PP_COND_LT = "LessThan";
  public const string PP_COND_ET = "EqualTo";
  public const string PP_COND_NET = "NotEqualTo";



  private static MultiDictionary<string, string> ValidInputs = new MultiDictionary<string, string>() {
      {PP_NO_MODE, PP_PHASE_OPEN},

      // Phase Mode
      {PP_PHASE_OPEN, PP_PHASE_CLOSE},

      {PP_PHASE_OPEN, PP_OBJECT_SOUND},
      {PP_PHASE_OPEN, PP_OBJECT_TIMER},
      //{PP_PHASE_OPEN, PP_OBJECT_TRIGGER},
      {PP_PHASE_OPEN, PP_OBJECT_VARIABLE},
      {PP_PHASE_OPEN, PP_CUSTOM_EVENT_OPEN},

      {PP_CUSTOM_EVENT_OPEN, PP_CUSTOM_EVENT_CLOSE},
      {PP_CUSTOM_EVENT_OPEN, PP_ACTION_END_PHASE},
      {PP_CUSTOM_EVENT_OPEN, PP_ACTION_MAKE_INTERACTIBLE},
      {PP_CUSTOM_EVENT_OPEN, PP_ACTION_REMOVE_INTERACTIBLE},
      {PP_CUSTOM_EVENT_OPEN, PP_ACTION_PLAY_SOUND},
      {PP_CUSTOM_EVENT_OPEN, PP_ACTION_ENABLE_TRIGGER},
      {PP_CUSTOM_EVENT_OPEN, PP_ACTION_DISABLE_TRIGGER},
      {PP_CUSTOM_EVENT_OPEN, PP_ACTION_INCREMENT_VALUE},
      {PP_CUSTOM_EVENT_OPEN, PP_ACTION_SET_VALUE},
      {PP_CUSTOM_EVENT_OPEN, PP_ACTION_WAIT},
      {PP_CUSTOM_EVENT_OPEN, PP_ACTION_LEGACY_EVENT},

      // Sound Creation Mode
      {PP_OBJECT_SOUND, PP_PARAM_NAME},
      {PP_OBJECT_SOUND, PP_PARAM_AUTOSTART},

      // Timer Creation Mode
      {PP_OBJECT_TIMER, PP_PARAM_GLOBAL},
      {PP_OBJECT_TIMER, PP_PARAM_NAME},
      {PP_OBJECT_TIMER, PP_PARAM_DURATION},
      {PP_OBJECT_TIMER, PP_PARAM_AUTOSTART},
      {PP_OBJECT_TIMER, PP_PARAM_DESTROY_ON_PHASE},
      {PP_OBJECT_TIMER, PP_PARAM_REPEAT},

      // Trigger Creation Mode
      //{PP_OBJECT_TRIGGER, PP_PARAM_NAME},

      // Variable Creation Mode
      {PP_OBJECT_VARIABLE, PP_PARAM_GLOBAL},
      {PP_OBJECT_VARIABLE, PP_PARAM_NAME},
      {PP_OBJECT_VARIABLE, PP_PARAM_VALUE},
      {PP_OBJECT_VARIABLE, PP_PARAM_TYPE},
      {PP_OBJECT_VARIABLE, PP_PARAM_DESTROY_ON_PHASE},

      /*// Custom Event Creation Mode
      {PP_OBJECT_CUSTOM_EVENT, PP_PARAM_NAME},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_ENABLE_TRIGGER},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_DISABLE_TRIGGER},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_END_PHASE},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_INCREMENT_VALUE},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_SET_VALUE},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_MAKE_INTERACTIBLE},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_REMOVE_INTERACTIBLE},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_PLAY_SOUND},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_WAIT},*/

      // Trigger Status Mode
      {PP_ACTION_DISABLE_TRIGGER, PP_PARAM_NAME},
      {PP_ACTION_ENABLE_TRIGGER, PP_PARAM_NAME},
      
      // Variable Incrementing Mode
      {PP_ACTION_INCREMENT_VALUE, PP_PARAM_NAME},
      {PP_ACTION_INCREMENT_VALUE, PP_PARAM_VALUE},

      // Variable Setting Mode
      {PP_ACTION_SET_VALUE, PP_PARAM_NAME},
      {PP_ACTION_SET_VALUE, PP_PARAM_VALUE},

      // Sound Play Mode
      {PP_ACTION_PLAY_SOUND, PP_PARAM_NAME},
      {PP_ACTION_PLAY_SOUND, PP_PARAM_SOUND_ACTOR},
      //{PP_ACTION_PLAY_SOUND, PP_PARAM_SOUND_LOCATION},

      // Wait Mode
      {PP_ACTION_WAIT, PP_PARAM_VALUE},

      {PP_ACTION_LEGACY_EVENT, PP_PARAM_NAME},

      // Event Handling Mode
      {PP_EVENT_TIMER_COMPLETED, PP_PARAM_NAME},
      {PP_EVENT_TIMER_COMPLETED, PP_PARAM_ACTION},

      {PP_EVENT_ENTER_TRIGGER, PP_PARAM_NAME},
      {PP_EVENT_ENTER_TRIGGER, PP_PARAM_ACTION},

      {PP_EVENT_MATH_CONDITION, PP_PARAM_NAME},
      {PP_EVENT_MATH_CONDITION, PP_PARAM_CONDITION_REQUIREMENT},
      {PP_EVENT_MATH_CONDITION, PP_PARAM_VALUE},
      {PP_EVENT_MATH_CONDITION, PP_PARAM_ACTION},

      {PP_EVENT_ITEM_PICKUP, PP_PARAM_NAME},
      {PP_EVENT_ITEM_PICKUP, PP_PARAM_ACTION},
  };

  private static Dictionary<string, ParseModeDuration> ParseDurations = new Dictionary<string, ParseModeDuration>() {
    {PP_COMMENT_LINE ,ParseModeDuration.Line},
    {PP_NO_MODE, ParseModeDuration.Single},
    {PP_PHASE_OPEN, ParseModeDuration.Cancelled},
    {PP_PHASE_CLOSE, ParseModeDuration.None},
    {PP_CUSTOM_EVENT_OPEN, ParseModeDuration.Cancelled},
    {PP_CUSTOM_EVENT_CLOSE, ParseModeDuration.None},
    {PP_OBJECT_VARIABLE, ParseModeDuration.Line},
    {PP_OBJECT_TIMER, ParseModeDuration.Line},
    //{PP_OBJECT_TRIGGER, ParseModeDuration.Line},
    {PP_OBJECT_SOUND, ParseModeDuration.Line},
    //{PP_OBJECT_CUSTOM_EVENT, ParseModeDuration.Line},
    {PP_VAR_TYPE_INT, ParseModeDuration.Line},
    {PP_VAR_TYPE_FLOAT, ParseModeDuration.Line},
    {PP_VAR_TYPE_BOOL, ParseModeDuration.Line},
    {PP_VAR_TYPE_STRING, ParseModeDuration.Line},
    {PP_EVENT_TIMER_COMPLETED, ParseModeDuration.Line},
    {PP_EVENT_ITEM_PICKUP, ParseModeDuration.Line},
    {PP_EVENT_ENTER_TRIGGER, ParseModeDuration.Line},
    {PP_EVENT_MATH_CONDITION, ParseModeDuration.Line},
    {PP_ACTION_PLAY_SOUND, ParseModeDuration.Line},
    {PP_ACTION_ENABLE_TRIGGER, ParseModeDuration.Line},
    {PP_ACTION_DISABLE_TRIGGER, ParseModeDuration.Line},
    {PP_ACTION_END_PHASE, ParseModeDuration.Single},
    {PP_ACTION_SET_VALUE, ParseModeDuration.Line},
    {PP_ACTION_INCREMENT_VALUE, ParseModeDuration.Line},
    {PP_ACTION_MAKE_INTERACTIBLE, ParseModeDuration.Line},
    {PP_ACTION_REMOVE_INTERACTIBLE, ParseModeDuration.Line},
    {PP_ACTION_WAIT, ParseModeDuration.Line},
    {PP_PARAM_GLOBAL, ParseModeDuration.Single},
    {PP_PARAM_NAME, ParseModeDuration.Single},
    {PP_PARAM_VALUE, ParseModeDuration.Single},
    {PP_PARAM_TYPE, ParseModeDuration.Single},
    {PP_PARAM_DURATION, ParseModeDuration.Single},
    {PP_PARAM_REPEAT, ParseModeDuration.Single},
    {PP_PARAM_AUTOSTART, ParseModeDuration.Single},
    {PP_PARAM_DESTROY_ON_PHASE, ParseModeDuration.Single},
    {PP_PARAM_ACTION, ParseModeDuration.Single},
    //{PP_PARAM_SOUND_LOCATION, ParseModeDuration.Single},
    {PP_PARAM_SOUND_ACTOR, ParseModeDuration.Single},
    {PP_PARAM_CONDITION_REQUIREMENT, ParseModeDuration.Single},
    {PP_COND_LT, ParseModeDuration.None},
    {PP_COND_ET, ParseModeDuration.None},
    {PP_COND_GT, ParseModeDuration.None}
  };

  private static Dictionary<string, System.Type> ParameterTypes = new Dictionary<string, System.Type>() {
    {PP_VAR_TYPE_INT, typeof(int)},
    {PP_VAR_TYPE_FLOAT, typeof(float)},
    {PP_VAR_TYPE_BOOL, typeof(bool)},
    {PP_VAR_TYPE_STRING, typeof(string)},
    {PP_PARAM_GLOBAL, typeof(bool)},
    {PP_PARAM_NAME, typeof(string)},
    {PP_PARAM_VALUE, typeof(float)},
    {PP_PARAM_TYPE, typeof(string)},
    {PP_PARAM_DURATION, typeof(float)},
    {PP_PARAM_REPEAT, typeof(int)},
    {PP_PARAM_AUTOSTART, typeof(bool)},
    {PP_PARAM_DESTROY_ON_PHASE, typeof(bool)},
    {PP_PARAM_ACTION, typeof(string)},
    //{PP_PARAM_SOUND_LOCATION, typeof(string)},
    {PP_PARAM_SOUND_ACTOR, typeof(string)},
    {PP_PARAM_CONDITION_REQUIREMENT, typeof(string)},
    {PP_PARAM_PATH, typeof(string)},
    {PP_PARAM_CONDITION_ITEM, typeof(string)},
  };

  private static Dictionary<string, string> ParameterLiterals = new Dictionary<string, string>() {
    {PP_PARAM_GLOBAL, "Global"},
    {PP_PARAM_NAME, "Name"},
    {PP_PARAM_VALUE, "Value"},
    {PP_PARAM_DURATION, "Duration"},
    {PP_PARAM_REPEAT, "RepeatCount"},
    {PP_PARAM_AUTOSTART, "IsRunning"},
    {PP_PARAM_DESTROY_ON_PHASE, "DestroyOnPhase"},
    {PP_PARAM_ACTION, "Action"},
    //{PP_PARAM_SOUND_LOCATION, "SoundLocation"},
    {PP_PARAM_SOUND_ACTOR, "Actor"},
    {PP_PARAM_CONDITION_REQUIREMENT, "Condition"},
    {PP_PARAM_WAIT_TIME, "Value"}
  };

  public static bool IsValidForMode(string mode, string param)
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
  }

  public static ParseModeDuration GetModeDuration(string mode)
  {
    return ParseDurations[mode];
  }

  public static System.Type GetParameterType(string param)
  {
    return ParameterTypes[param];
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
    return parsed.Replace(PP_STRING_SPACER, " ");
  }
}