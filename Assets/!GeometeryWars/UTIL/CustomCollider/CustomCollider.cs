using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//put outside class so accessible to all classes
public enum ColliderType { sphere, box, capsule };
public enum CapsuleDirection { Xaxis, Yaxis, Zaxis }


//Rob
//Custom editor gives a handle to any object that has this script
//Than on game start it will create a sphere collider the same size of the handler
public class CustomCollider : MonoBehaviour
{
    [SerializeField] public bool handleOn = true;
    [SerializeField] public float handleSize = 1f;
    [SerializeField] public ColliderType currentCollider;
    [SerializeField] public Vector3 center;

    [SerializeField] public float radiusSphere = 1f;
    
    [SerializeField] public float length = 1f;
    [SerializeField] public float width = 1f;
    [SerializeField] public float depth = 1f;
   
    [SerializeField] public float radiusCapsule = 1f;
    [SerializeField] public float height = 1f;
    [SerializeField] public CapsuleDirection direction = CapsuleDirection.Xaxis;

    [SerializeField] public bool isTrigger = true;

    private Collider newCollider;


    private void Start()
    {
        //create collider using settings
        if (currentCollider == ColliderType.sphere)
        {
            //create new sphere collider
            newCollider = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;

            //set sphere colliders radius to size of handleSize
            SphereCollider temp = newCollider.GetComponent<SphereCollider>();
            temp.center = center;
            temp.radius = radiusSphere;

            if (isTrigger)
            {
                //set to trigger
                temp.isTrigger = true;
            }
        }
        else if (currentCollider == ColliderType.box)
        {
            //create new sphere collider
            newCollider = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;

            //set box collider dimensions...
            BoxCollider temp = newCollider.GetComponent<BoxCollider>();
            temp.center = center;
            temp.size = new Vector3(width, depth, length);

            if (isTrigger)
            {
                //set to trigger
                temp.isTrigger = true;
            }
        }
        else if (currentCollider == ColliderType.capsule)
        {
            //create new sphere collider
            newCollider = gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;

            //set box collider dimensions...
            CapsuleCollider temp = GetComponent<CapsuleCollider>();
            temp.direction = (int)direction;
            temp.center = center;
            temp.radius = radiusCapsule;
            temp.height = height;

            if (isTrigger)
            {
                //set to trigger
                temp.isTrigger = true;
            }
        }

        //destroy script after
        Destroy(this);
    }
}
