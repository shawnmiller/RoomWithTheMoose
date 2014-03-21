public static class PPS
{
  public const string PP_COMMENT_LINE = "//";                           // Indicates that the line should be ignored

  // Building Phases
  public const string PP_PHASE_OPEN = "Phase";
  public const string PP_PHASE_CLOSE = "EndPhase";

  // Phase Setup
  public const string PP_PHASE_SETUP_BEGIN = "BeginSetup";
  public const string PP_PHASE_SETUP_END = "EndSetup";

  // Custom Event Setup
  public const string PP_CUSTOM_EVENT_BEGIN = "BeginCustomEvent";
  public const string PP_CUSTOM_EVENT_END = "EndCustomEvent";

  // Creatable Objects
  public const string PP_OBJECT_VARIABLE = "Variable";
  public const string PP_OBJECT_TIMER = "Timer";
  public const string PP_OBJECT_TRIGGER = "Trigger";
  public const string PP_OBJECT_SOUND = "Sound";
  public const string PP_OBJECT_CUSTOM_EVENT = "CustomEvent";

  // Variable Types
  public const string PP_VAR_TYPE_INT = "Integer";
  public const string PP_VAR_TYPE_FLOAT = "Float";
  public const string PP_VAR_TYPE_BOOL = "Bool";
  public const string PP_VAR_TYPE_STRING = "String";

  // Predefined Events
  public const string PP_EVENT_TIMER_COMPLETED = "OnTimerComplete";
  public const string PP_EVENT_ITEM_PICKUP = "OnPickUpItem";
  public const string PP_EVENT_ENTER_TRIGGER = "OnTriggerEnter";
  public const string PP_EVENT_MATH_CONDITION = "Condition";

  // Actions
  public const string PP_ACTION_PLAY_SOUND = "PlaySound";
  public const string PP_ACTION_ENABLE_TRIGGER = "EnableTrigger";
  public const string PP_ACTION_DISABLE_TRIGGER = "DisableTrigger";
  public const string PP_ACTION_END_PHASE = "CompletePhase";
  public const string PP_ACTION_SET_VALUE = "SetValue";
  public const string PP_ACTION_INCREMENT_VALUE = "IncrementValue";
  public const string PP_ACTION_MAKE_INTERACTIBLE = "MakeInteractible";
  public const string PP_ACTION_REMOVE_INTERACTIBLE = "RemoveInteractible";
  public const string PP_ACTION_WAIT = "Wait";
 
  // Parameters
  public const string PP_PARAM_GLOBAL = "Global";
  public const string PP_PARAM_NAME = "Name";
  public const string PP_PARAM_VALUE = "Value";                         // Variable
  public const string PP_PARAM_TYPE = "Type";                           // Variable
  public const string PP_PARAM_DURATION = "Duration";                   // Timer
  public const string PP_PARAM_REPEAT = "Repeat";                       // Timer
  public const string PP_PARAM_AUTOSTART = "AutoStart";                 // Timer, Sound
  public const string PP_PARAM_DESTROY_ON_PHASE = "DestroyOnPhase";     // Timer, Variable, Trigger
  public const string PP_PARAM_ACTION = "Action";                       // Predefined Events
  public const string PP_PARAM_SOUND_LOCATION = "PlayAt";               // Sound
  public const string PP_PARAM_SOUND_ACTOR = "PlayFrom";                // Sound
  public const string PP_PARAM_CONDITION_REQUIREMENT = "ConditionReq";  // Condition

  // Conditionals
  public const string PP_COND_GT = "GreaterThan";
  public const string PP_COND_LT = "LessThan";
  public const string PP_COND_ET = "EqualTo";



  private static MultiDictionary<string, string> ValidInputs = new MultiDictionary<string, string>() {
      // Phase Mode
      {PP_PHASE_OPEN, PP_PHASE_CLOSE},
      {PP_PHASE_OPEN, PP_PHASE_SETUP_BEGIN},

      // Phase Setup Mode
      {PP_PHASE_SETUP_BEGIN, PP_PHASE_SETUP_END},
      {PP_PHASE_SETUP_BEGIN, PP_OBJECT_SOUND},
      {PP_PHASE_SETUP_BEGIN, PP_OBJECT_TIMER},
      {PP_PHASE_SETUP_BEGIN, PP_OBJECT_TRIGGER},
      {PP_PHASE_SETUP_BEGIN, PP_OBJECT_VARIABLE},
      {PP_PHASE_SETUP_BEGIN, PP_CUSTOM_EVENT_BEGIN},
      {PP_PHASE_SETUP_BEGIN, PP_ACTION_MAKE_INTERACTIBLE},

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
      {PP_OBJECT_TRIGGER, PP_PARAM_NAME},

      // Variable Creation Mode
      {PP_OBJECT_VARIABLE, PP_PARAM_GLOBAL},
      {PP_OBJECT_VARIABLE, PP_PARAM_NAME},
      {PP_OBJECT_VARIABLE, PP_PARAM_VALUE},
      {PP_OBJECT_VARIABLE, PP_PARAM_TYPE},
      {PP_OBJECT_VARIABLE, PP_PARAM_DESTROY_ON_PHASE},

      // Custom Event Creation Mode
      {PP_OBJECT_CUSTOM_EVENT, PP_PARAM_NAME},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_ENABLE_TRIGGER},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_DISABLE_TRIGGER},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_END_PHASE},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_INCREMENT_VALUE},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_SET_VALUE},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_MAKE_INTERACTIBLE},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_REMOVE_INTERACTIBLE},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_PLAY_SOUND},
      {PP_OBJECT_CUSTOM_EVENT, PP_ACTION_WAIT},

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
      {PP_ACTION_PLAY_SOUND, PP_PARAM_SOUND_LOCATION},

      // Wait Mode
      {PP_ACTION_WAIT, PP_PARAM_VALUE},

      // Event Handling Mode
      {PP_EVENT_TIMER_COMPLETED, PP_PARAM_NAME},
      {PP_EVENT_TIMER_COMPLETED, PP_PARAM_ACTION},

      {PP_EVENT_ENTER_TRIGGER, PP_PARAM_NAME},
      {PP_EVENT_ENTER_TRIGGER, PP_PARAM_ACTION},

      {PP_EVENT_MATH_CONDITION, PP_PARAM_NAME},
      {PP_EVENT_MATH_CONDITION, PP_PARAM_CONDITION_REQUIREMENT},
      {PP_EVENT_MATH_CONDITION, PP_PARAM_ACTION},

      {PP_EVENT_ITEM_PICKUP, PP_PARAM_NAME},
      {PP_EVENT_ITEM_PICKUP, PP_PARAM_ACTION},
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
}