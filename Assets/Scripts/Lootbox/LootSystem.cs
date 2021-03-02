using UnityEngine;

[System.Serializable]

public class ItemToSpawn
{
    public GameObject item;
    public float spawnRate;

    public float minSpawnProb, maxSpawnProb;
}

public class LootSystem : MonoBehaviour
{
    public ItemToSpawn[] itemToSpawn;
    public LootBox lootBox;

    void Start()
    {
        for (int i = 0; i < itemToSpawn.Length; i++)
        {
            if (i == 0)
            {
                itemToSpawn[i].minSpawnProb = 0;
                itemToSpawn[i].maxSpawnProb = itemToSpawn[i].spawnRate - 1; //60 - 1 = 59
            }
            else
            {
                itemToSpawn[i].minSpawnProb = itemToSpawn[i - 1].maxSpawnProb + 1; //79 + 1 = 80
                itemToSpawn[i].maxSpawnProb = itemToSpawn[i].minSpawnProb + itemToSpawn[i].spawnRate - 1; //80 + 10 = 90 - 1 = 89
            }
        }
    }

    public void Spawner()
    {
        float randomNum = Random.Range(0, 100); //56

        for (int i = 0; i < itemToSpawn.Length; i++)
        {
            if(randomNum >= itemToSpawn[i].minSpawnProb && randomNum <= itemToSpawn[i].maxSpawnProb)
            {
                Debug.Log(randomNum + " " + itemToSpawn[i].item.name);
                Instantiate(itemToSpawn[i].item, lootBox.lootPosition.transform);
                break;
            }
        }
    }
}
