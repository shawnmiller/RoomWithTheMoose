using System;

public static class TypeConversion
{
  public static object Convert(Type type, string value)
  {
    if(type == typeof(int))
    {
      return Int32.Parse(value);
    }
    if(type == typeof(float))
    {
      return Single.Parse(value);
    }
    if(type == typeof(bool))
    {
      switch (value)
      {
        case "true":
        case "True":
        case "yes":
        case "Yes":
        case "1":
          return true;
        case "false":
        case "False":
        case "no":
        case "No":
        case "0":
          return false;
      }
    }
    return value;
  }
}