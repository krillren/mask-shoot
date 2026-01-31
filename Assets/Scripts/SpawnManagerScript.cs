using UnityEngine;
using System.Collections.Generic;
using System.Linq; 

public class SpawnManagerScript : MonoBehaviour
{
    public GameObject Prefab;
    public Camera cam;

    private int _entityCount = 20;

    public float timeBetweenSpawns = 5;
    private float _timeSinceLastSpawn = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < _entityCount; ++i)
        {
            SpawnEntity();
            print("Spawned entity " + i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn >= timeBetweenSpawns)
        {
            SpawnEntity();
            _timeSinceLastSpawn -= timeBetweenSpawns;
        }
    }

    void SpawnEntity(){
        Vector3 worldMin = cam.ScreenToWorldPoint(new Vector2(0, 0));
        Vector3 worldMax = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        float x = worldMax.x - worldMin.x;
        float y = worldMax.y - worldMin.y;  

        Vector3 spawnPosition = PositionOnSides(x, y);

        GameObject entity = Instantiate(Prefab, spawnPosition,Quaternion.identity);

        entity.GetComponent<CharacterMask>().EquipMask(
            GameManagerScript.Instance.GetRandomMaskFromPool()
        );

        GameManagerScript.Instance.RegisterEntity(entity);
    }

    // Creates a random position on the sides of a rectangle of width x and height y
    Vector3 PositionOnSides(float x, float y)
    {
        var posOnRectangleLength = Random.Range(0, x * 2 + y * 2);
        if (posOnRectangleLength < x)
        {
            return new Vector3(posOnRectangleLength - x/2, -y/2, 0);
        }
        posOnRectangleLength -= x;
        if (posOnRectangleLength < y)
        {
            return new Vector3(x/2, posOnRectangleLength - y/2, 0);
        }
        posOnRectangleLength -= y;
        if (posOnRectangleLength < x)
        {
            return new Vector3(posOnRectangleLength - x/2, y/2, 0);
        }
        return new Vector3(-x/2, posOnRectangleLength - x - y/2, 0);
    }
}

