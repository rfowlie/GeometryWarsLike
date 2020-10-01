using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotentialFunction : MonoBehaviour
{
    public Transform target;
    // -A/x^m + R/x^n
    public float attraction = 2000f;
    public float repulsion = 4000f;
    public float attractionAttenuation = 2f;
    public float repulstionAttentuation = 3f;

    public float speedMove = 1f;
    public float speedRotate = 30f;

    public void Calculate()
    {
        //get distance from center as vector
        Vector3 r = target.position - transform.position;
        //normalize
        Vector3 rn = r.normalized;
        //calc potential
        float potential = repulsion / Mathf.Pow(r.magnitude, repulstionAttentuation) - attraction / Mathf.Pow(r.magnitude, attractionAttenuation);
        Debug.Log("Potential: " + potential);

        //temp rotate
        transform.Rotate(0f, potential * speedRotate * Time.deltaTime, 0f);
        //temp move
        transform.position += transform.forward * speedMove * Time.deltaTime;
    }

    private void Update()
    {
        Calculate();
    }
}
