using System.Collections;
using UnityEngine;
using System;

using UnityEngine.InputSystem;


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
        public Transform map;
        public LayerMask mapLayer, obstacleLayer;
        [Range(0.1f, 10f)] public float distanceFromSurface = 1f;
        public float obstacleDistance = 1f;



        public Vector3 velocity;
        private Vector3 aim;
        private Vector3 local;

        [Space]
        [SerializeField] private int healthMax = 100;
        private int healthCurrent;
        public float movementSpeed = 1f;

        private RectTransform healthUI;   
        

        public event Action DEATH;

        //shift to health component...
        private Coroutine c = null;
        IEnumerator HealthUpdate()
        {
            while(healthUI.localScale.y > healthPercentage)
            {
                healthUI.localScale -= new Vector3(0f, Time.deltaTime, 0f);
                yield return null;
            }

            c = null;
        }
        public float healthPercentage = 0f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
            {
                other.gameObject.SetActive(false);
                Debug.Log("<color=blue>Lost Life</color>");
                //health version
                //FOR now...
                healthCurrent -= other.GetComponent<AEnemy>().GetDamage();
                healthPercentage = (float)healthCurrent / (float)healthMax;
                if (c == null)
                {
                    c = StartCoroutine(HealthUpdate());
                }
                //healthUI.localScale = new Vector3(1f, (float)healthCurrent / (float)healthMax, 1f);

                if(healthCurrent <= 0)
                {
                    //fix things
                    StopCoroutine(c);
                    c = null;
                    DEATH();
                }
            }
        }

        //temp        
        private void DetermineDrop(DropType t)
        {
            switch(t)
            {
                case DropType.HEAL:
                    int temp = healthCurrent + 25;
                    healthCurrent = temp > healthMax ? healthMax : temp;
                    healthUI.localScale = new Vector3(1f, (float)healthCurrent / (float)healthMax, 1f);
                    break;
                case DropType.HEALTH:
                    break;
                case DropType.MOVEMENTSPEED:
                    //could work...
                    movementSpeed = UpgradesController.Instance.GetMovementValue(++playerInfo.levelMovementSpeed);
                    CoroutineEX.Delay(this, () => movementSpeed = UpgradesController.Instance.GetMovementValue(--playerInfo.levelMovementSpeed), 5f);
                    break;
                case DropType.FIRERATE:
                    bullet.AdjustFireRate(UpgradesController.Instance.GetFireRateValue(++playerInfo.levelFireRate));
                    CoroutineEX.Delay(this, () => bullet.AdjustFireRate(UpgradesController.Instance.GetFireRateValue(--playerInfo.levelFireRate)), 5f);
                    break;
                case DropType.ARMOUR:
                    break;
                default:
                    break;
            }
        }


        

        //private InputAction_01 input;        
        private void OnDisable()
        {
            input.GamePlay.Disable();
            Drop.TRIGGER -= DetermineDrop;
        }

        private void OnEnable()
        {
            //listen for drop
            Drop.TRIGGER += DetermineDrop;
        }

        //put player input on player so when it gets destoyed so does the input 
        private Input_Gameplay input;

        private GameStateInfo playerInfo;
        public void Setup(RectTransform healthUI, GameStateInfo info)
        {
            playerInfo = info;
            UpgradesController u = UpgradesController.Instance;
            healthMax = u.GetHealthValue(info.levelHealth);
            healthCurrent = healthMax;
            this.healthUI = healthUI;
            movementSpeed = u.GetMovementValue(info.levelMovementSpeed);
            bullet.AdjustFireRate(u.GetFireRateValue(info.levelFireRate));
            //create object pool for bullets
            bullet.Setup();

            //NEW INPUT SYSTEM
            input = new Input_Gameplay();

            input.GamePlay.Exit.performed += (ctx) =>
            {
                //temporary so player can exit level
                DEATH();
            };
            
            input.GamePlay.Move.performed += (ctx) =>
            {
                velocity = (transform.forward * ctx.ReadValue<Vector2>().y + transform.right * ctx.ReadValue<Vector2>().x).normalized;
            };
            input.GamePlay.Aim.performed += (ctx) =>
            {
                local = (transform.forward * ctx.ReadValue<Vector2>().y + transform.right * ctx.ReadValue<Vector2>().x).normalized;
                //Debug.Log($"<color==green>Hello World!!</color>");
            };
            input.GamePlay.AimMouse.performed += (ctx) =>
            {
                Debug.Log($"<color=white>{ctx.ReadValue<Vector2>()}</color>");
                Vector2 dir = (ctx.ReadValue<Vector2>() - new Vector2(Screen.width / 2f, Screen.height / 2f)).normalized;
                local = (transform.forward * dir.y + transform.right * dir.x).normalized;

            };
            input.GamePlay.Fire.started += (ctx) =>
            {
                bullet.BeginFire(body);
            };
            input.GamePlay.Fire.canceled += (ctx) =>
            {
                bullet.StopFire();
            };

            input.GamePlay.Enable();
            

            //set values
            map = GameController.Instance.GetMap().transform;
            //sketchy...
            transform.position = map.GetChild(0).transform.position;
            mapLayer = GameController.Instance.GetMapLayer();
            obstacleLayer = GameController.Instance.GetObstacleLayer();
            

            //make sure player is setup correctly
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (map.position - transform.position).normalized, out hit, float.PositiveInfinity, mapLayer))
            {
                transform.position = hit.point + hit.normal;
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
        }


        public void Move()
        {
            //keep object on surface of world
            if (velocity != Vector3.zero)
            {
                //Calc next pos and then check for obstacle collision
                RaycastHit hitNext;
                Vector3 nextPos = transform.position + velocity * movementSpeed * Time.fixedDeltaTime;
                if (Physics.SphereCast(transform.position, 1f, velocity, out hitNext, obstacleDistance, obstacleLayer))
                {
                    //obstacle hit...
                    Debug.DrawRay(transform.position, hitNext.point - transform.position, Color.blue, 1f);
                    Debug.DrawRay(hitNext.point, hitNext.normal, Color.green, 1f);
                    Vector3 p = Vector3.Project(transform.position - hitNext.point, hitNext.normal);
                    p = ((hitNext.point - transform.position) + p).normalized;
                    Debug.DrawRay(transform.position, p * 3f, Color.yellow, 1f);
                    nextPos = transform.position + p * movementSpeed * Time.fixedDeltaTime;
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
