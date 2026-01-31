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

    private List<(Mask mask, float probability)> MasksSpawnPool = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < _entityCount; ++i)
        {
            SpawnEntity();
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

        entity.GetComponent<CharacterMask>().EquipMask(GetRandomMaskFromPool());

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

    Mask GetRandomMaskFromPool()
    {
        var probabilities = MasksSpawnPool.Select(val => val.probability).ToList();

        for (int i = 1; i < probabilities.Count; ++i)
        {
            probabilities[i] += probabilities[i-1];
        }

        var random = Random.Range(0, probabilities[^1]);

        for (int i = 0; i < probabilities.Count; ++i)
        {
            if (probabilities[i] > random)
            {
                return MasksSpawnPool[i].mask;
            }
        }

        return MasksSpawnPool[^1].mask;

    }

    public void AddToPool(Mask mask, float probability)
    {
        MasksSpawnPool.Add((mask, probability));
    }

    public void RemoveFromPool(Mask mask)
    {
        MasksSpawnPool = MasksSpawnPool.Where(val => val.mask != mask).ToList();
    }
}

