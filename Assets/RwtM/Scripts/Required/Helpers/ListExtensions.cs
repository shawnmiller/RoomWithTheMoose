using System.Collections.Generic;

public static class ListExtensions
{
  /// <summary>
  /// Creates a list of all items in pickFrom which do not exist in notFoundIn.
  /// </summary>
  /// <param name="remove">The exclusion list.</param>
  /// <returns>
  /// Returns an empty list if pickFrom and notFoundIn are identical.
  /// Otherwise, returns a list of all items not found in remove.
  /// </returns>
  /// 
  public static List<T> MutuallyExclusivePick<T>(this List<T> pickFrom, List<T> remove)
  {
    List<T> picks = new List<T>();
    foreach (T obj in pickFrom)
    {
      if (!remove.Contains(obj))
      {
        picks.Add(obj);
      }
    }

    return picks;
  }

  /// <summary>
  /// Adds an item to list if it does not already exist within the list.
  /// If the item already exists, it will not be added.
  /// </summary>
  /// <param name="item">The item which is being added.</param>
  public static void ExclusiveAdd<T>(this List<T> list, T item)
  {
    if (!list.Contains(item))
    {
      list.Add(item);
    }
  }
}