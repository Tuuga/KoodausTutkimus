﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSDisplay : MonoBehaviour {
	float deltaTime = 0.0f;
	SpawnerUIManager gm;
    public Text fpsText;

	void Update () {
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        fpsText.text = text;
    }
}
