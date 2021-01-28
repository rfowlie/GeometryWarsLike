using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace GeometeryWars
{
    public class SpawnManager 
    {
        public SpawnManager(MonoBehaviour inst, SO_LevelPattern levelPatterns, Transform map)
        {
            this.inst = inst;
            //get values from global variables...
            this.levelPatterns = levelPatterns;
            this.map = map;

            //create pools
            pools = new ObjectPool<AEnemy>[levelPatterns.enemyPrefabs.Length];
            for (int i = 0; i < pools.Length; i++)
            {
                pools[i] = new ObjectPool<AEnemy>(levelPatterns.enemyPrefabs[i], 100);
            }

            levelIndex = 0;
        }

        private MonoBehaviour inst;
        public int levelIndex = 0;
        public float spawnCount = 0f;
        bool isSpawn = true;
        //Coroutine c = null;
        private SO_LevelPattern levelPatterns;
        private Transform map;
        private ObjectPool<AEnemy>[] pools;

        public ObjectPool<AEnemy>[] GetPools()
        {
            return pools;
        }
        //return all active objects
        public List<AEnemy> GetActiveFromPools()
        {
            List<AEnemy> temp = new List<AEnemy>();
            for (int i = 0; i < pools.Length; i++)
            {
                foreach(var e in pools[i].GetActiveObjects())
                {
                    temp.Add(e);
                }                
            }

            return temp;
        }               
               

        public void Execute(float timeFromZero)
        {
            if (isSpawn)
            {
                //check time against current index
                if (levelPatterns.spawnTimes[levelIndex] < timeFromZero)
                {
                    inst.StartCoroutine(SpawnUnits(levelIndex, levelPatterns));                    

                    //prime next pattern, activate delay
                    levelIndex++;

                    //if we've reached the end of the spawn list turn spawning off
                    if (levelIndex == levelPatterns.spawnTimes.Length)
                    {
                        isSpawn = false;
                    }
                }
            }
        }

        //spawn units one on each frame... 
        IEnumerator SpawnUnits(int levelIndex, SO_LevelPattern levelPatterns)
        {
            //int length = levelPatterns.patterns[levelPatterns.patternIndex[levelIndex]].points.Length;

            //calculate points from PatternInfo
            PatternCreator.PatternInfo p = levelPatterns.container.GetValues()[levelPatterns.patternIndex[levelIndex]];

            //calculate points for each pattern info
            Vector3[] points = new Vector3[0];
            Vector3[] normals = new Vector3[0];
            Vector3 direction = map.position - map.TransformDirection(p.relativePosition);
            List<Vector3> list = new List<Vector3>(PatternCreator.Shapes.GetShape(
                p.shape, p.amountOfPoints, p.radius, direction, p.rotation, p.angleOffset));

            int lengthWithPercent = Mathf.RoundToInt(list.Count * (p.percentage * 0.01f));
            //to properly place points from surface after raycast
            float distanceFromSurface = GameController.Instance.GetDistanceFromSurface();
            //store proper amount of points based on percentage
            if (lengthWithPercent > 0)
            {
                int remove = list.Count - lengthWithPercent;
                list.RemoveRange(lengthWithPercent - 1, remove);
                points = list.ToArray();
                normals = new Vector3[points.Length];
                //raycast onto map for exact points
                RaycastHit hit;
                Vector3 position = map.TransformDirection(p.relativePosition);
                float distanceToMap = direction.magnitude;
                for (int k = 0; k < points.Length; k++)
                {
                    points[k] += position;
                    //calculate raycast direction, depends on which bool is selected
                    //Vector3 dir = towardsCenter ? (map.position - shapePoints[i]).normalized : -transform.up;
                    //Debug.DrawLine(points[k], points[k] + -transform.transform.up, Color.yellow);
                    if (Physics.Raycast(points[k], direction, out hit, distanceToMap))
                    {
                        points[k] = hit.point + hit.normal * distanceFromSurface;
                        //needed for rotation
                        normals[k] = hit.normal;
                    }
                }
            }

            
            //spawn objects
            for (int i = 0; i < points.Length; i++)
            {                
                AEnemy o = pools[levelPatterns.enemyTypeIndex[levelIndex]].Retrieve();

                if(o != null)
                {
                    //get position
                    //o.transform.position = levelPatterns.patterns[levelPatterns.patternIndex[levelIndex]].points[i];
                    o.transform.position = points[i];
                    //point transform down towards map
                    o.transform.rotation = Quaternion.FromToRotation(o.transform.up, normals[i]) * o.transform.rotation;
                }
                
                yield return null;
            }
        }
    }
}