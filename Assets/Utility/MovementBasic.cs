using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBasic : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
            );

        transform.position += temp * speed * Time.deltaTime;
    }
}
