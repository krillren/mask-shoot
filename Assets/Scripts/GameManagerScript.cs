using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }
    public SpawnManagerScript SpawnManagerScript;
    public Confiance confiance;

    public List<Mask> AllMask;
    private List<Mask> targetMask;



    public float maskIntroInterval = 30f;

    private List<Mask> activeMaskPool = new();
    private Dictionary<Mask, int> alivePerMask = new();

    private Coroutine maskRoutine;


    private int aliveEntities;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        maskRoutine = StartCoroutine(MaskProgressionRoutine());
    }

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
        List<Mask> available = AllMask.FindAll(m => !activeMaskPool.Contains(m));

        if (available.Count == 0)
            return;

        Mask newMask = available[Random.Range(0, available.Count)];

        activeMaskPool.Add(newMask);
        alivePerMask[newMask] = 0;

        Debug.Log($"New mask added to pool: {newMask.maskName}");
    }

    public Mask GetRandomActiveMask()
    {
        if (activeMaskPool.Count == 0)
            return null;

        return activeMaskPool[Random.Range(0, activeMaskPool.Count)];
    }

    public void RegisterMaskInstance(Mask mask)
    {
        if (mask == null) return;

        if (!alivePerMask.ContainsKey(mask))
            alivePerMask[mask] = 0;

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
                RemoveMaskFromGame(mask);
            }
        }

        UnregisterEntity(character.gameObject);
        Destroy(character.gameObject);
    }

    private void RemoveMaskFromGame(Mask mask)
    {
        activeMaskPool.Remove(mask);
        alivePerMask.Remove(mask);

        Debug.Log($"Mask removed from game: {mask.maskName}");
    }

    public void RegisterEntity(GameObject entity)
    {
        aliveEntities++;
    }

    public void UnregisterEntity(GameObject entity)
    {
        aliveEntities--;

        if (aliveEntities <= 0)
        {
            EndGame();
        }
    }

    public void OnCharacterHit(CharacterMask character)
    {
        // Central decision logic
        if (character.equippedMask != null)
        {
            character.UnequipMask();
        }
        else
        {
            KillCharacter(character);
        }
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
    }
}
