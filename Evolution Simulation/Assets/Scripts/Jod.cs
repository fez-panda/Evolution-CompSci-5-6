using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Jod : MonoBehaviour
{
    [SerializeField]
    public int foodNumber;
    public Transform foodPrefab;
    public Transform animalPrefab;
    public int animalNum;
    public float TimeCount;
    private GameObject[] deathList;
    private float foodsCount;
    public TextMeshProUGUI speedAvg;
    private GameObject[] animalBois;
    private float speedAvgNum;

    void Start () {
        FoodSpawn.FoodFunction(foodNumber, foodPrefab);
        AnimalSpawn.AnimalSpawning(animalNum, animalPrefab);
    }

    void FixedUpdate() {
        TimeCount += Time.deltaTime;
        if (TimeCount >= 37) {
            GameObject[] foods = GameObject.FindGameObjectsWithTag("Food");
            foreach(GameObject food in foods) {
                Destroy(food);
            }
            FoodSpawn.FoodFunction(foodNumber, foodPrefab);
            TimeCount = 0;
        }
        animalBois = GameObject.FindGameObjectsWithTag("Animal");
        speedAvgNum = 0;
        for (int i = 0; i < animalBois.Length; i++) {
            AnimalNav speedy = animalBois[i].GetComponent<AnimalNav>();
            speedAvgNum += speedy.AnimalSpeed;
        }
        speedAvgNum = speedAvgNum / animalBois.Length;
        speedAvg.text = "Speed Average: " + speedAvgNum.ToString();
    }
}
