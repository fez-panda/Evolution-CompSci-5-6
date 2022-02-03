using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawn : MonoBehaviour
{

    public static void AnimalSpawning (int AnimalNum, Transform AnimalPrefab) {
        float randomness1;
        float randomness2;
        Vector3 spawnPosition;
        for (int i = 0; i < AnimalNum; i++) {
            randomness1 = Random.Range(-24f, 24f);
            randomness2 = Random.Range(1f,4f);
            if (randomness2 <= 1) {
                spawnPosition = new Vector3(48, 1, randomness1);
                Transform animal = UnityEngine.Object.Instantiate(AnimalPrefab);
                animal.transform.position = spawnPosition;
            } else if (randomness2 <= 2) {
                spawnPosition = new Vector3(-48, 1, randomness1);
                Transform animal = UnityEngine.Object.Instantiate(AnimalPrefab);
                animal.transform.position = spawnPosition;
            } else if (randomness2 <= 3) {
                spawnPosition = new Vector3(randomness1, 1, 48);
                Transform animal = UnityEngine.Object.Instantiate(AnimalPrefab);
                animal.transform.position = spawnPosition;
            } else if (randomness2 <= 4) {
                spawnPosition = new Vector3(randomness1, 1, -48);
                Transform animal = UnityEngine.Object.Instantiate(AnimalPrefab);
                animal.transform.position = spawnPosition;
            }
        }
    }
}
