using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombController : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;
    }
}
