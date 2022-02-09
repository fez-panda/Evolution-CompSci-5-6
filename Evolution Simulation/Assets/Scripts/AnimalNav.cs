 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Mathf;

public class AnimalNav : MonoBehaviour
{
    public int foodCount = 0; 

    NavMeshAgent _navMeshAgent;
    private GameObject closest;
    private float distance;
    public GameObject AnimalPrefab;
    public float AnimalSense;
    public float AnimalSpeed;
    private float randomness1;
    private float randomness2;
    private float randomness3;
    private bool analyzed = true;
    private Vector3 closestP;
    private float timerWander = 0;
    private Vector3 previous;
    private float energy = 100f;

    public GameObject ground;
    private Jod jod;


    void Start () {
        foodCount = 0;
        previous = transform.position;
        _navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        ground = GameObject.FindGameObjectWithTag("Ground");
        jod = ground.GetComponent<Jod>();
        Debug.Log("here");
        //mutations
        randomness1 = Random.Range(1f, 10f);
        if (randomness1 > 7.5f) {
            AnimalSpeed += Random.Range(-.5f, .5f);
            AnimalSense += Random.Range(-2f, 2f);
        }
    }

    void FixedUpdate()
    {
        _navMeshAgent.speed = AnimalSpeed * 10f;
        if (energy > 0) {
            Vector3 place = navigation(_navMeshAgent, jod, foodCount, closestP, timerWander, previous);
            _navMeshAgent.SetDestination(place);
            previous = place;
            energy -= (Time.deltaTime * Pow(AnimalSpeed, 2));
        } else if (energy <= 0) {
            _navMeshAgent.SetDestination(transform.position);
        }
        timerWander++;
        if (jod.TimeCount >= 35 && analyzed == false) {
            analyzed = true;
            EndOfDay(foodCount, jod, AnimalPrefab, transform.position);
            foodCount = 0;
            energy = 100f;
        } else if (jod.TimeCount < 10) {
            analyzed = false;
        }

    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Food")) {
            Destroy(other.gameObject);
            foodCount++;
        }
    }

    void EndOfDay (int foodCount, Jod jod, GameObject AnimalPrefab, Vector3 current) {
        GameObject animal;
        float posX = current.x;
        float posZ = current.z;
        bool homeSafe = false;
        if (Abs(posX) >= 47 || Abs(posZ) >= 47) {
            homeSafe = true;
        }
        if (foodCount >= 2 && homeSafe) {
            jod.animalNum++;
            Debug.Log("Spawned new: "+ jod.animalNum);
            animal = CreateChildren(AnimalPrefab);
            Instantiate(animal); 
        } else if  (foodCount == 1 && homeSafe) {
            Debug.Log("Survived " + jod.animalNum);
        } else {
            jod.animalNum--; 
            Debug.Log(jod.animalNum);
            Debug.Log("Died " + jod.animalNum);
            Destroy(gameObject);
        }

    }

    GameObject CreateChildren (GameObject AnimalPrefab) {
        GameObject animalNew;
        Vector3 spawnPosition = new Vector3(AnimalPrefab.transform.position.x, AnimalPrefab.transform.position.y, AnimalPrefab.transform.position.z);
        animalNew = AnimalPrefab;
        animalNew.transform.position = spawnPosition;
        AnimalNav anav = animalNew.GetComponent<AnimalNav>();
        anav.AnimalSense = this.AnimalSense;
        anav.AnimalSpeed = this.AnimalSpeed;
        return animalNew;
    }

    Vector3 wander (Vector3 animalPosition) {
        float randomness1;
        float randomness2;
        randomness1 = Random.Range(-10f, 10f);
        randomness2 = Random.Range(-10f, 10f);
        Vector3 newTarget = new Vector3(animalPosition.x + randomness1, animalPosition.y, animalPosition.z + randomness2);
        return newTarget;
    } 

    Vector3 navigation (NavMeshAgent _navMeshAgent, Jod jod, float foodCount, Vector3 closestP, float timerWander, Vector3 previous) {
        closestP = previous;
        if (jod.TimeCount >= 30 && foodCount >= 1 ) {
            closestP = transform.position;
            if (Abs(closestP.x) >= Abs(closestP.z)) {
                if (closestP.x >= 0) {
                    closestP.x = 49;
                } else {
                    closestP.x = -49;
                }
            } else if (Abs(closestP.z) > Abs(closestP.x)) {
                if (closestP.z >= 0) {
                    closestP.z = 49;
                } else {
                    closestP.z = -49;
                }
            }
        } else {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Food");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            for (int i = 0; i < gos.Length; i++)
            {
                Vector3 diff = gos[i].transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = gos[i];
                    distance = curDistance;
                }
            }
            if (distance <= AnimalSense * 10) {
                closestP = closest.transform.position;
            } else if (timerWander % 3 == 0) {
                closestP = wander(transform.position);
            }
        }
        return closestP;
    }
}
