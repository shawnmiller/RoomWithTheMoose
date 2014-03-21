using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

delegate object BuildHandler(string line);

public static class SceneParser
{
  private const int CMD = 0;
  private const int VAL = 1;

  static BuildHandler buildHandler;
  static Stack<string> modeStack;


  public static Phase[] BuildScene(TextAsset sceneFile, MonoBehaviour caller)
  {
    Regex regex = new Regex(@"\t*");
    string temp = regex.Replace(sceneFile.text, "");
    regex = new Regex(@" {2,}");
    temp = regex.Replace(temp, " ");
    regex = new Regex(@" *= *");
    temp = regex.Replace(temp, "=");

    StreamWriter newFile = new StreamWriter(@"C:\Users\Shawn\Desktop\Testing.txt");
    newFile.Write(temp);
    newFile.Close();
    //Debug.Log("Done.");

    StreamReader fileReader = new StreamReader(@"C:\Users\Shawn\Desktop\Testing.txt");
    int lineNumber = 0;
    while (!fileReader.EndOfStream)
    {
      ++lineNumber;
      string line = fileReader.ReadLine();

      // Check for missing spaces between inputs
      for (int i = 0; i < line.Length; ++i)
      {
        if (line[i] == '=')
        {
          int nextSpace = line.IndexOf(' ', i+1);
          int nextEquals = line.IndexOf('=', i + 1);
          if ((nextSpace != -1) && (nextEquals != -1) && (nextSpace > nextEquals))
          {
            Debug.LogError("Two inputs were defined but not separated by a space. (Line #" + lineNumber + ") Failing to fix this will cause unintended results and potential crashes.");
            Debug.LogError(line);
            Debug.Break();
          }
        }
      }

      string[] inputs = StringHelper.SplitTextDelimited(line, " ");

      for (int j = 0; j < inputs.Length; ++j)
      {
        string[] inputComponents = StringHelper.SplitTextDelimited(inputs[j], "=");

        switch (inputComponents[CMD])
        {
          case PPS.PP_COMMENT_LINE:
            continue;
          case PPS.PP_PHASE_OPEN:
            
            break;
        }
      }
    }

    fileReader.Close();

    return null;
    //throw new System.NotImplementedException();
  }
}