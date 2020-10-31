using System.Collections;
using UnityEngine;
using System;


//keep this object a certain distance from the plane...
public class SurfacePlayer : MonoBehaviour
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
    private Vector3 aim;
    private Vector3 local;
    [SerializeField] private LayerMask mapLayer;


    public static event Action DEATH;
    [SerializeField] private int lives = 3;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("<color=blue>Lost Life</color>");
            lives--;
            if(lives <= 0)
            {
                //game over...
                DEATH();
            }
        }
    }

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
        aim = Input.mousePosition - new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        local = transform.TransformDirection(new Vector3(aim.x, 0f, aim.y)).normalized;
        

        //TEMP FIRE BULLETS
        if (Input.GetMouseButtonDown(0))
        {
            isFire = true;
            c = StartCoroutine(Fire());
        }
        if(Input.GetMouseButtonUp(0))
        {
            isFire = false;
            c = null;
        }
    }

    private void FixedUpdate()
    {
        //keep object on surface of world
        if (velocity != Vector3.zero)
        {
            RaycastHit hit;
            Vector3 nextPos = transform.position + velocity * speed * Time.fixedDeltaTime;
            if (Physics.Raycast(nextPos, -transform.up, out hit, distanceFromSurface + 2f, mapLayer))
            {
                transform.position = hit.point + hit.normal * distanceFromSurface;
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
        }

        //rotate body to face mouse
        body.rotation = Quaternion.FromToRotation(body.forward, local) * body.rotation;
    }


    Coroutine c = null;
    [SerializeField] private bool isFire = false;
    [Range(0.01f, 1f)]
    [SerializeField] private float fireRate = 0.1f;

    IEnumerator Fire()
    {
        float count = 0f;
        while(isFire)
        {
            count += Time.deltaTime;
            if(count > fireRate)
            {
                count -= fireRate;
                GameObject bullet = bulletPool.Get();
                bullet.transform.position = body.position + body.forward;
                bullet.transform.rotation = body.rotation;
            }

            yield return null;
        }
    }
}
