/*using System.Collections.Generic;

public static class StackHelper
{
  /// <summary>
  /// Returns the value held count places under the top of the stack.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="Number of places from the top. Starts at 0."></param>
  /// <returns>The nth object from the top of the stack, null if count exceeds the size of the stack.</returns>
  public static System.Nullable<T> Peek<T>(this Stack<T> stack, int count)
  {
    if(stack.Count < count)
      return null;
    return stack.ToArray()[stack.Count - 1 - count];
  }
}*/