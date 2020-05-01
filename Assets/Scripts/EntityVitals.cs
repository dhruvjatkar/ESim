using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityVitals
{
    public int health = 100;
    public float hunger = 100f;
    public float hungerTimer = 0.0f;
    public float healthTimer = 0.0f;
    public float secondsHealth = 0.0f;
    public float secondsHunger = 0.0f;



    public int manageHealth()
	{
        if(health != 0){
            if(hunger <= 90)
			{
                healthTimer += Time.deltaTime;
                secondsHealth = healthTimer % 60;
                //Debug.Log("My health Is" + health.ToString());



                if(secondsHealth >= 1)
				{
                    healthTimer -= secondsHealth;
                    health -= 1;

				}
			}
		}

        if(health == 0)
		{
            Debug.Log("This entity is dead");
		}
          return health;

	}

    public void manageHunger()
    {
        if(hunger != 0){
        //  Debug.Log("My hunger is " + hunger.ToString());
            hungerTimer += Time.deltaTime;
            secondsHunger = hungerTimer % 60;
          //  Debug.Log("HungerSeconds = " + secondsHunger.ToString());

            if (secondsHunger >= 2)
            {
                hunger -= 1;
                hungerTimer = 0;

            }

          }



	}

}
