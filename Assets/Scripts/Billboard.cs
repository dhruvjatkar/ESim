using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
  //public Transform cam = Find("Main Camera").transform.position;

    // Start is called before the first frame update
    void LateUpdate(){
        transform.LookAt(Camera.current.transform);

    }
}
