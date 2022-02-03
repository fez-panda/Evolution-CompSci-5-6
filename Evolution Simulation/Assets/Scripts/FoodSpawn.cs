using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn: MonoBehaviour
{
    public static Transform[] FoodFunction (int foodNumber, Transform foodPrefab) {
        Transform foodPrefabed = foodPrefab;
        Transform[] foods;
        float randomness1;
        float randomness2;
        Vector3 placement;
        foods = new Transform[foodNumber];
        for (int i = 0; i< foodNumber; i++) {
            randomness1 = Random.Range(-45f, 45f);
            randomness2 = Random.Range(-45f, 45f);
            placement = new Vector3(randomness1, .5f, randomness2);
            Transform food = foods[i] = UnityEngine.Object.Instantiate (foodPrefabed);
            food.transform.position = placement;
        }
        return foods;
    }
}
