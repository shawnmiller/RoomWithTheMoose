using System.Collections.Generic;

public class MultiDictionary<TKey, TValue>
{
  private Dictionary<TKey, List<TValue>> _data;

  public void Add(TKey key, TValue value)
  {
    if (_data.ContainsKey(key))
    {
      _data[key].Add(value);
    }
    else
    {
      _data.Add(key, new List<TValue>() { value });
    }
  }

  public TValue[] GetValues(TKey key)
  {
    if (_data.ContainsKey(key))
    {
      return _data[key].ToArray();
    }

    return null;
  }

  public void Replace(TKey key, TValue[] values)
  {
    if(_data.ContainsKey(key))
    {
      _data[key] = new List<TValue>(values);
    }
  }

  public void Remove(TKey key)
  {
    try
    {
      _data.Remove(key);
    }
    catch
    {
      // There was no matching key, still correct execution
    }
  }
}