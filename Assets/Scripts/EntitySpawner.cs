using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{

    public GameObject entity = null;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(entity, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }


}
