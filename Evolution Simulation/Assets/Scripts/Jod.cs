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
    public TextMeshProUGUI animalNumDisplay;
    public TextMeshProUGUI TimerDisplay;
    float speedAvgNum = 0;

    void Start () {
        FoodSpawn.FoodFunction(foodNumber, foodPrefab);
        AnimalSpawn.AnimalSpawning(animalNum, animalPrefab);
        animalNumDisplay.text = "# of Animals: " + animalNum.ToString();
    }

    void FixedUpdate() {
        TimeCount += Time.deltaTime;
        TimerDisplay.text = "Timer: " + TimeCount.ToString();
        if (TimeCount >= 36) {
            animalNumDisplay.text = "# of Animals: " + animalNum.ToString();
            GameObject[] foods = GameObject.FindGameObjectsWithTag("Food");
            foreach(GameObject food in foods) {
                Destroy(food);
            }
            FoodSpawn.FoodFunction(foodNumber, foodPrefab);
            TimeCount = 0;
        } else if (TimeCount <= 3) {
            speedAvg = speedCount(speedAvgNum, speedAvg);
        }
    }

    TextMeshProUGUI speedCount(float speedAvgNum, TextMeshProUGUI speedAvg) {
        GameObject[] animalBois;
        animalBois = GameObject.FindGameObjectsWithTag("Animal");
        speedAvgNum = 0;
        for (int i = 0; i < animalBois.Length; i++) {
            AnimalNav speedy = animalBois[i].GetComponent<AnimalNav>();
            speedAvgNum += speedy.AnimalSpeed;
        }
        speedAvgNum = speedAvgNum / animalBois.Length;
        speedAvg.text = "Speed Average: " + speedAvgNum.ToString();
        return speedAvg;
    }
}
