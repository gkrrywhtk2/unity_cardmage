using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
   
    public Transform spawnpoint;
    public GameObject mob_0;
    
 
    public void Awake()
    {
        StartCoroutine(SpawnMonster());
    }

    IEnumerator SpawnMonster()
   {
        Instantiate(mob_0, new Vector2(spawnpoint.transform.position.x, spawnpoint.transform.position.y),Quaternion.identity);
        yield return new WaitForSeconds(10f);
        StartCoroutine(SpawnMonster());
    }

}
