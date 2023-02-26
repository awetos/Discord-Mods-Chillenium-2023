using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class screenshot : MonoBehaviour{
    public string filename = "screenshot";
    public string folder = "Screenshots";
	int i = 0;

	void Update() {
		if(Input.GetKeyUp(KeyCode.Space)){
			Capture();
		}
	}
	public void Capture(){
        // Create a folder for the screenshots if it doesn't exist
        if (!Directory.Exists(folder)){
            Directory.CreateDirectory(folder);
        }

        // Set the file path and name
        string filePath = Path.Combine(folder, filename+i+".png");
		i++;

        // Capture the screenshot and save it to the file
        ScreenCapture.CaptureScreenshot(filePath);
    }
}
