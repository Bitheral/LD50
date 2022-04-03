using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    private static Transform EarthTransform;

    public float spawnTime;
    public float time;
    
    public float minTime = 30;
    public float maxTime = 120;

    // Start is called before the first frame update
    void Start()
    {
        setRandomTime();
        EarthTransform = GameObject.FindGameObjectsWithTag("Planet")[0].transform;
    }

    Vector3 getRandomPosition(Vector3 center, float radius)
    {
        return (Random.onUnitSphere * radius) + center;
    }

    void spawnAsteroid()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.AddComponent<Asteroid>();
        go.transform.position = getRandomPosition(EarthTransform.position, 50);
        go.transform.rotation = Quaternion.FromToRotation(Vector3.forward, EarthTransform.position - go.transform.position);
    }


    void setRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= spawnTime)
        {
            spawnAsteroid();
            setRandomTime();
            time = 0;
        }
    }
}
