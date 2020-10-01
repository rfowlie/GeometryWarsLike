using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugThrustSteer : MonoBehaviour
{
    public float speedThrust = 3f;
    public float speedSteer = 45f;

   
    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        transform.position += transform.forward * speedThrust * v * Time.deltaTime;
        transform.localEulerAngles += new Vector3(0f, h * speedSteer, 0f) * Time.deltaTime;
    }
}
