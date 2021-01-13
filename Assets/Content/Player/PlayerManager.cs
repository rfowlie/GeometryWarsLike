using System.Collections;
using UnityEngine;
using System;


namespace GeometeryWars
{
    //keep this object a certain distance from the plane...
    public class PlayerManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform body;
        [SerializeField] private BulletManager bullet;
        [Space]

        [Header("Variables")]
        [Range(0.1f, 10f)] public float distanceFromSurface = 1f;
        public float speed = 1f;
        public Vector3 velocity;
        private Vector3 aim;
        private Vector3 local;

        public Transform map;
        public LayerMask mapLayer, obstacleLayer;
        public float obstacleDistance = 1f;

        [SerializeField] private int lives = 3;
        

        public event Action DEATH;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
            {
                Debug.Log("<color=blue>Lost Life</color>");
                lives--;
                if (lives <= 0)
                {
                    //notify listeners of game over
                    DEATH();
                }
            }
        }

        public void Setup(float movementSpeed, float fireRate)
        {
            //set values
            map = GameController.Instance.GetMap().transform;
            mapLayer = GameController.Instance.GetMapLayer();
            obstacleLayer = GameController.Instance.GetObstacleLayer();

            //setup player stats from info
            speed = movementSpeed;
            bullet.AdjustFireRate(fireRate);


            //create object pool for bullets
            bullet.Setup();

            //make sure player is setup correctly
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (map.position - transform.position).normalized, out hit, float.PositiveInfinity, mapLayer))
            {
                transform.position = hit.point + hit.normal;
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
        }

        //get resolution...
        private Vector3 screenSize = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        
        public void UpdatePlayer()
        {
            //movement
            velocity = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")).normalized;

            //get mouse position compare to center of screen to aquire rotation direction, apply to player body
            aim = Input.mousePosition - screenSize;
            local = transform.TransformDirection(new Vector3(aim.x, 0f, aim.y)).normalized;


            //TEMP FIRE BULLETS
            if (Input.GetMouseButtonDown(0))
            {
                bullet.BeginFire(body);
            }
            if (Input.GetMouseButtonUp(0))
            {
                bullet.StopFire();
            }
        }

        public void Move()
        {
            //keep object on surface of world
            if (velocity != Vector3.zero)
            {
                //Calc next pos and then check for obstacle collision
                RaycastHit hitNext;
                Vector3 nextPos = transform.position + velocity * speed * Time.fixedDeltaTime;
                if (Physics.SphereCast(transform.position, 1f, velocity, out hitNext, obstacleDistance, obstacleLayer))
                {
                    //obstacle hit...
                    Debug.DrawRay(transform.position, hitNext.point - transform.position, Color.blue, 1f);
                    Debug.DrawRay(hitNext.point, hitNext.normal, Color.green, 1f);
                    Vector3 p = Vector3.Project(transform.position - hitNext.point, hitNext.normal);
                    p = ((hitNext.point - transform.position) + p).normalized;
                    Debug.DrawRay(transform.position, p * 3f, Color.yellow, 1f);
                    nextPos = transform.position + p * speed * Time.fixedDeltaTime;
                }

                //calc move after factoring obstacle avoidance
                RaycastHit hit;
                if (Physics.Raycast(nextPos, -transform.up, out hit, distanceFromSurface + 2f, mapLayer))
                {
                    transform.position = hit.point + hit.normal * distanceFromSurface;
                    transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                }
            }

            //rotate body to face mouse
            body.rotation = Quaternion.FromToRotation(body.forward, local) * body.rotation;
        }
    }
}
