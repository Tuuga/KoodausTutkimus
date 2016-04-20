using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ReverbDistVisualization : MonoBehaviour {

    public AudioReverbZone reverbZone;
    public Transform visualMinDist;
    public Transform visualMaxDist;

    void Update () {

        float minDist = reverbZone.minDistance;
        float maxDist = reverbZone.maxDistance;

        Vector3 minDistScale = Vector3.one * minDist * 2;
        Vector3 maxDistScale = Vector3.one * maxDist * 2;

        visualMinDist.localScale = minDistScale;
        visualMaxDist.localScale = maxDistScale;
    }
}
