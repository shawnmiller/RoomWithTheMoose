using UnityEngine;
using System.Collections;
using System.IO;

public class FilePathTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
    StreamWriter writer = new StreamWriter(@"C:\Users\Shawn\Desktop\FilePath.txt");
    writer.WriteLine("Persistent Data Path: " + Application.persistentDataPath);
    writer.WriteLine("Data Path: " + Application.dataPath);
    writer.Close();
	}
}
