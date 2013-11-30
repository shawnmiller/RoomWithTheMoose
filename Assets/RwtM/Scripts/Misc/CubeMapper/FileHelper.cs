using System;
using System.IO;

public static class FileHelper
{
  public static bool PathExists(string path)
  {
    if (IsFile(path))
    {
      return File.Exists(path);
    }
    else
    {
      return Directory.Exists(path);
    }
  }

  public static string BuildPath(params string[] pathParts)
  {
    if (pathParts.Length < 2)
    {
      return pathParts[0];
    }

    string path = pathParts[0];

    for (int i = 1; i < pathParts.Length; ++i)
    {
      path = Path.Combine(path, pathParts[i]);
    }

    return path;
  }

  private static bool IsFile(string path)
  {
    return Path.GetFileName(path) != System.String.Empty;
  }
}