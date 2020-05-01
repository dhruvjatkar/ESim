using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehavior : MonoBehaviour
{

  public GameObject food;
  public bool isFoodPresent = false;
  public float foodTime = 0.0f;

    // Start is called before the first frame update

    void Start(){
      Instantiate(food, this.gameObject.transform.position, Quaternion.identity);
      isFoodPresent = true;
  //    Debug.Log();
    }

    void OnTriggerEnter(Collider col){
      Destroy(GameObject.FindGameObjectWithTag(destroyRelative()));
      isFoodPresent = false;
    }

    public void foodRespwnTimer(){
        if(isFoodPresent ==  false){
          foodTime += Time.deltaTime;

          if((foodTime % 60) >= 15){
            Instantiate(food, this.gameObject.transform.position, Quaternion.identity);
            isFoodPresent = true;
            foodTime = 0;

          }


        }

      else{

        return;
      }



    }

    void Update(){
      foodRespwnTimer();

    }

    public string destroyRelative(){
      string strTag = this.gameObject.tag.ToString();
      Debug.Log(strTag[strTag.Length - 1].ToString() + "THIS IS MY TAG");
      return("Food" + strTag[strTag.Length - 1].ToString());
    }

}
