﻿using System.Text.RegularExpressions;

public static class StringHelper
{
  public static string[] SplitTextDelimited(this string text, string delimiter)
  {
    return text.Split(new string[] { delimiter }, System.StringSplitOptions.RemoveEmptyEntries);
    //Regex lineSplitter = new Regex(delimiter);
    //return lineSplitter.Split();
  }

  /// <summary>
  /// Returns the text enclosed between a starting and ending
  /// </summary>
  /// <param name="text">The text to split</param>
  /// <param name="open">string that heads the enclosed text</param>
  /// <param name="close">string that ends the enclosed text</param>
  public static string GetInnerText(string text, string open, string close)
  {
    Regex leftCrop = new Regex(@".*" + open);
    Regex rightCrop = new Regex(close + @".*");
    string newString = leftCrop.Replace(text, "");
    newString = rightCrop.Replace(newString, "");
    return newString;
  }

  public static string[] SplitTextAt(string text, string splitAt)
  {
    string[] split = new string[2];
    int index = text.IndexOf(splitAt);
    if (index != -1)
    {
      split[0] = text.Substring(0, index);
      split[1] = text.Substring(index);
    }
    else
    {
      split[0] = text;
      split[1] = null;
    }
    return split;
  }

  public static string GetWord(this string text, int index, string delimiter)
  {
    try
    {
      return SplitTextDelimited(text, delimiter)[index];
    }
    catch (System.Exception e)
    {
      UnityEngine.Debug.Log("Error found in " + text);
      UnityEngine.Debug.Log(e.Message);
      return text;
    }
  }

  public static string GetLastWord(this string text, string delimiter)
  {
    string[] words = SplitTextDelimited(text, delimiter);
    return words[words.Length - 1];
  }
}