using UnityEngine;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class CoreExtraction : GameComponent
{
  void Start()
  {
    if (Application.isEditor)
    {
      return;
    }

    string path = Application.dataPath + Globals.FILE_EXTRACTION_ROOT;
    if(!Directory.Exists(path))
    {
      ExtractFiles();
    }
  }

  void ExtractFiles()
  {
    /*******************************************************************************
     * The majority--if not the entirety--of the files that we will be extracting
     * are XML files. Because they are going to be grabbed as TextAssets from the
     * Resources folder, we have no information on them at all outside of the
     * text contained within them. We need a way to determine where these files
     * are going to be extracted to. We also need to allow the developers to run
     * the game without having to build every single time, so this requires that
     * the information not impede on the file's content in any way. In order to
     * meet both of these requirements, the first line of every file will contain
     * this path and file name within a comment using the
     * Globals.EXTRACTION_FILE_HEADER_OPEN and Globals.EXTRACTION_FILE_HEADER_CLOSE
     * values.
     ******************************************************************************/
    TextAsset[] pull = Resources.LoadAll(Globals.EXTRACTION_RESOURCE_FOLDER) as TextAsset[];

    foreach (TextAsset asset in pull)
    {
      string[] lines = asset.text.Split(new char[] {'\n'});
      
      Regex cRemove = new Regex("[<>!- ]");
      string path = cRemove.Replace(lines[0], "");
      
      List<string> pathBreakdown = new List<string>(Application.dataPath.Split('/'));
      pathBreakdown.Add(Globals.FILE_EXTRACTION_ROOT);
      pathBreakdown.AddRange(path.Split(new char[] {'/'}));
      path = PathMaker.BuildPath(pathBreakdown.ToArray(), @"\");
      
      string[] fileData = new string[lines.Length-1];
      System.Array.Copy(lines, 1, fileData, 0, fileData.Length-1);
      
      string directory = path.Substring(0, path.LastIndexOf('/'));
      
      if (!Directory.Exists(directory))
      {
        Directory.CreateDirectory(directory);
      }
      
      File.WriteAllLines(path, fileData);
    }
  }
}
