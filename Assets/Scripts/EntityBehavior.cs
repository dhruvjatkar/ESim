using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EntityBehavior : MonoBehaviour
{

    public GameObject Entity1;

    public bool hasReproduced = false;

    float timer = 0.0f;
    float reproduceTimer = 0.0f;

    public float seconds = 0.0f;

    bool exploreOnce = false;
    bool reproOnce = false;
    bool huntOnce = false;

    public EntityVitals myVitals = new EntityVitals();

    public HealthBar myHealthBar;

    [SerializeField]

    NavMeshAgent _navMeshAgent;

    void OnTriggerEnter(Collider c){

    if(c.tag.ToString().TrimEnd(c.tag.ToString()[c.tag.ToString().Length -1]).ToString() == "Spawner"){
      huntOnce = false;
      myVitals.hunger += 10;

      Debug.Log("I DONE NOMMED IT");
    }

    return;

      }


    public int numGBsInList(GameObject[] gbList)
    {
        int numTotal = 0;
        foreach(GameObject g in gbList)
		{
            numTotal += 1;
		}
        return numTotal;
    }


    public void checkIfDead(){
      if(myVitals.manageHealth() == 0){
        Destroy(this.gameObject);

      }

    }

    public void reproduceBehavior(){

      reproduceTimer += Time.deltaTime;
      if(reproOnce == false){
        findClosest("Entity1");
        reproOnce = true;
      }
      else{

    //  Debug.Log("Reproducing" + r.ToString());
      if((reproduceTimer % 60) >= 5){
          reproduceTimer = 0.0f;
          Instantiate(Entity1, findClosest("Entity1").transform.position, Quaternion.identity);
          hasReproduced = true;

        }
      }

    }


    public void hungerBehavior(){
      if(huntOnce == false){
        System.Random rFood = new System.Random();
        int whichOne = rFood.Next(1, 8);
      findClosest("Spawner" + whichOne.ToString());
      huntOnce = true;
    }
    else{

      return;
}

    }

    public GameObject exploreBehavior()
	{
        //every x seconds, the navAgent finds a new random grass tile to move towards

        timer += Time.deltaTime;
        seconds = timer % 60;

        if (seconds <= 6)
            {
                if(exploreOnce == false)
			    {

                    GameObject[] grassTiles;

                    grassTiles = GameObject.FindGameObjectsWithTag("Grass");

                    System.Random randomTile = new System.Random();


                    int Index = randomTile.Next(numGBsInList(grassTiles));


                    Vector3 targetVector = grassTiles[Index].transform.position;

                    _navMeshAgent.SetDestination(targetVector);

                    exploreOnce = true;

                    return null;
                }

            }

            if (seconds > 6)
            {
                exploreOnce = false;
                timer -= seconds;

            }

        return null;
	}

    public GameObject findClosest(string tile)
	{
        GameObject[] tiles;

        tiles = GameObject.FindGameObjectsWithTag(tile);

        GameObject closest = null;

        float distance = Mathf.Infinity;

        Vector3 position = transform.position;


        foreach (GameObject go in tiles)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if((curDistance < distance) && (go != this.gameObject))
            {
                closest = go;
                distance = curDistance;
            }
        }
        if(tile == "Entity1"){
        Vector3 targetVector = closest.transform.position - ((closest.transform.position - position)/2);
        _navMeshAgent.SetDestination(targetVector);
        return closest;
        }
        else{
        Vector3 targetVector = closest.transform.position;
        _navMeshAgent.SetDestination(targetVector);
        return closest;

        }



    }


    void Start()

	{
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        myHealthBar.setMaxHealth(100);




    }
    // Update is called once per frame
    public void Update()
    {
      checkIfDead();

      myVitals.manageHunger();
      myHealthBar.SetHealth(myVitals.manageHealth());


      if(myVitals.hunger >= 90){
        exploreBehavior();

      }
      if(myVitals.hunger < 90 && myVitals.health >= 50){
        hungerBehavior();
        Debug.Log("Foraging");

      }

      if(myVitals.hunger < 90 && myVitals.health < 50 && hasReproduced == false){
        reproduceBehavior();
        Debug.Log("Mating");
      }

      if(hasReproduced == true){
        hungerBehavior();
      }


    }
}
