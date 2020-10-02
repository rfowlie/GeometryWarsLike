using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//keep this object a certain distance from the plane...
public class SurfaceMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform body;
    [SerializeField] private Poolable bullet;
    private ObjectPool bulletPool;
    [Space]

    [Header("Variables")]
    [Range(0.1f, 10f)] public float distanceFromSurface = 1f;
    public float speed = 1f;
    public Vector3 velocity;


    private void Start()
    {
        //create bullet pool
        bulletPool = new ObjectPool(bullet);
    }

    private void Update()
    {
        //movement
        velocity = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")).normalized;

        //get mouse position compare to center of screen to aquire rotation direction, apply to player body
        Vector3 aim = Input.mousePosition - new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector3 local = transform.TransformDirection(new Vector3(aim.x, 0f, aim.y)).normalized;
        body.rotation = Quaternion.FromToRotation(body.forward, local) * body.rotation;

        //TEMP FIRE BULLETS
        if (Input.GetMouseButtonUp(0))
        {
            GameObject temp = bulletPool.Get();
            temp.transform.position = body.position + body.forward;
            temp.transform.rotation = body.rotation;
        }
    }

    
    private void FixedUpdate()
    {
        //keep object on surface of world
        if(velocity != Vector3.zero)
        {
            RaycastHit hit;
            Vector3 nextPos = transform.position + velocity * speed * Time.fixedDeltaTime;
            if (Physics.Raycast(nextPos, -transform.up, out hit, float.PositiveInfinity))
            {
                transform.position = hit.point + hit.normal * distanceFromSurface;
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
        }
    }
}
