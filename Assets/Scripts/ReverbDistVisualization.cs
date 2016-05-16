#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;


[ExecuteInEditMode]
public class ReverbDistVisualization : MonoBehaviour {

    public AudioReverbZone[] reverbZone;
    public AudioSource aSource;

    void Update () {

        for (int i = 0; i < reverbZone.Length; i++) {
            float minDist = reverbZone[i].minDistance;
            float maxDist = reverbZone[i].maxDistance;

            Vector3 minDistScale = Vector3.one * minDist * 2;
            Vector3 maxDistScale = Vector3.one * maxDist * 2;

            Transform visualMinDist = reverbZone[i].transform.Find("VisualMinDist");
            Transform visualMaxDist = reverbZone[i].transform.Find("VisualMaxDist");

            if (visualMinDist != null && visualMaxDist != null) {
                visualMinDist.localScale = minDistScale;
                visualMaxDist.localScale = maxDistScale;
            }            
        }
    }
}
#endif