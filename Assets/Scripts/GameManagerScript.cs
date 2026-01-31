using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }
    public SpawnManagerScript SpawnManagerScript;
    public Confiance confiance;

    public List<Mask> AllMask;
    private List<Mask> targetMask;
    

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

    private void KillCharacter(CharacterMask character)
    {
        UnregisterEntity(character.gameObject);
        Destroy(character.gameObject);
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
    }
}
