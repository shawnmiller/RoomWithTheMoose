using System.Collections.Generic;

public static class ListHelper
{
  public static T GetLast<T>(this List<T> list)
  {
    return list[list.Count - 1];
  }
}