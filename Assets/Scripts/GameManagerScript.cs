using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }
    public MaskTargetDisplay maskTargetDisplay;
    public MiniSceneManager miniSceneManager;
    public SpawnManagerScript SpawnManagerScript;
    public Confiance confiance;

    [Header("Masks")]
    public List<Mask> AllMask;
    public float maskIntroInterval = 10f;
   

    private List<Mask> AvailableMasks = new();

    // (Mask, Probability)
    private List<(Mask mask, float probability)> MasksSpawnPool = new();

    // How many alive characters per mask
    private Dictionary<Mask, int> alivePerMask = new();
    private List<Mask> targetedMask = new();
    private Coroutine maskRoutine;
    private int aliveEntities;
    public float confianceGains = 3f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        AvailableMasks.AddRange(AllMask);

        AddRandomMaskToPool();
        AddRandomMaskToPool();

        AddTargetedMask();

    }

    private void Start()
    {
        maskRoutine = StartCoroutine(MaskProgressionRoutine());
        
    }



    // Mask Progression, basically adds new masks to the spawn pool over time

    private IEnumerator MaskProgressionRoutine()
    {
        yield return new WaitForSeconds(1f);
        AddRandomMaskToPool();

        while (true)
        {
            yield return new WaitForSeconds(maskIntroInterval);
            AddRandomMaskToPool();
        }
    }

    private void AddRandomMaskToPool()
    {
        if (AvailableMasks.Count == 0)
            return;

        Mask newMask = AvailableMasks[Random.Range(0, AvailableMasks.Count)];
        AvailableMasks.Remove(newMask);

        AddToPool(newMask, 1f);
    }

    






    public void AddToPool(Mask mask, float probability)
    {
        if (mask == null || probability <= 0f)
            return;

        if (MasksSpawnPool.Any(p => p.mask == mask))
            return;

        MasksSpawnPool.Add((mask, probability));

        alivePerMask[mask] = 0;
    }

    public void RemoveFromPool(Mask mask)
    {
        var removed = MasksSpawnPool.RemoveAll(p => p.mask == mask);
        alivePerMask.Remove(mask);
    }

    public Mask GetRandomMaskFromPool()
    {
        if (MasksSpawnPool.Count == 0)
            return null;

        float total = MasksSpawnPool.Sum(p => p.probability);
        float roll = Random.Range(0f, total);

        float cumulative = 0f;
        foreach (var entry in MasksSpawnPool)
        {
            cumulative += entry.probability;
            if (roll <= cumulative)
                return entry.mask;
        }

        return MasksSpawnPool[^1].mask;
    }








    public void RegisterMaskInstance(Mask mask)
    {
        if (mask == null) return;
        aliveEntities++;

        if (!alivePerMask.ContainsKey(mask))
            return; // Mask not in pool anymore

        alivePerMask[mask]++;
    }

    private void KillCharacter(CharacterMask character)
    {
        Mask mask = character.equippedMask;

        if (mask != null && alivePerMask.ContainsKey(mask))
        {
            alivePerMask[mask]--;

            if (alivePerMask[mask] <= 0)
            {
                RemoveFromPool(mask);
                RemoveTargetedMask(mask);
                AddTargetedMask();
            }
        }
        if (targetedMask.Contains(mask)) {
            confiance.AddConfiance(confianceGains);
        }
        Destroy(character.gameObject);
    }

    private IEnumerator AddTargetCoroutine(float interval)
    {
        yield return new WaitForSeconds(interval);
        AddTargetedMask();
        AddTargetCoroutine(interval);
    }
    public void AddTargetedMask()
    {
        Mask randomMask = GetRandomMaskFromPool();

        if (randomMask is null)
        {
            return;
        }

        int i = 0;
        while (targetedMask.Contains(randomMask) && i<10)
        {
            randomMask = GetRandomMaskFromPool();
            i++;
        }
        targetedMask.Add(randomMask);
        maskTargetDisplay.AddTarget(randomMask);
    }

    public void RemoveTargetedMask(Mask mask)
    {
        targetedMask.Remove(mask);
        maskTargetDisplay.RemoveTarget(mask);
    }
    public void OnCharacterHit(CharacterMask character)
    {
        KillCharacter(character);

        aliveEntities--;

        if (aliveEntities <= 0)
        {
            EndGame();
        }
    }

    public void KillBadGuy()
    {
        EndGame();
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
        miniSceneManager.LoadLoose();
    }
}
