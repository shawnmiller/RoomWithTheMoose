public static class PathMaker
{
  ///<summary>
  /// Takes any amount of strings and converts them to a file path.
  /// Prereq: All separators have been removed from each string in pathPieces.
  ///</summary>
  ///<param name="separator"> The character used for separating each folder/file. </param>
  ///<param name="pathPieces"> A string array with a sorted path from root -> filename.ext </param>
  public static string BuildPath(string[] pathPieces, string separator)
  {
    if(pathPieces.Length == 1)
    {
      return pathPieces[0];
    }
    
    string result = "";
    
    for(int i=0; i<pathPieces.Length; ++i)
    {
      results += pathPieces[i];
      if(i != pathPieces.Length - 1)
      {
        results += separator;
      }
    }
    
    return results;
  }
}