using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
public class SpawnerScript : MonoBehaviour {

	public GameObject ball;
	SpawnerUIManager gm;
	public float spawnOffset;
	public float spawnInterval;
	float spawnX = -4.5f;
	float spawnZ = -4.5f;
	float spawnY = 0.5f;
	float spawnTimer;
	int ballsSpawned;

	void Start () {
		spawnX = -spawnOffset;
		spawnZ = -spawnOffset;
		gm = GameObject.Find("SpawnerUIManager").GetComponent<SpawnerUIManager>();
	}
	
	void Update () {
		spawnTimer += Time.deltaTime;
		while (spawnTimer > spawnInterval && spawnInterval > 0) {
			spawnTimer -= spawnInterval;

			ballsSpawned++;
			/*
			float xOffset = Random.Range(-spawnOffset, spawnOffset);
			float zOffset = Random.Range(-spawnOffset, spawnOffset);
			Vector3 spawnPos = new Vector3(xOffset, transform.position.y, zOffset);
			*/

			Vector3 spawnPos = new Vector3(spawnX,spawnY,spawnZ);
			spawnX++;
			if (spawnX > spawnOffset) {
				spawnZ++;
				spawnX = -spawnOffset;
			}
			if (spawnZ >= spawnOffset) {
				spawnY++;
				spawnZ = -spawnOffset;
			}
			Instantiate(ball, spawnPos, transform.rotation);
			gm.SetText(ballsSpawned);
		}
	}
}
#endif