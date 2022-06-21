using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  obtain objects from pool
public class SpawnManager : MonoBehaviour
{   
    public GameConstants gameConstants;

    public int greenEnemyCount = 3;
    public int gombaEnemyCount = 0;
    public GameObject mario;

    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j < greenEnemyCount; j++)
            spawnFromPooler(ObjectType.greenEnemy);

        for (int k = 0; k < gombaEnemyCount; k++)
            spawnFromPooler(ObjectType.gombaEnemy);

        // subscribe to event
        GameManager.OnEnemyDeath += EnemySpawnSequence;
    }


    void  spawnFromPooler(ObjectType i){
        // static method access
        /* GameObject item =  ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item  !=  null){
            //set position, and other necessary states
            item.transform.position  =  new  Vector3(Random.Range(-4.5f, 4.5f), item.transform.position.y, 0);
            item.SetActive(true);
        }
        else{
            Debug.Log("not enough items in the pool.");
        } */




        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);

        if (item != null)
        {
            //set position
            // item.transform.localScale = new Vector3(1, 1, 1);

            float Xvalue = Random.Range(-4.5f, 4.5f);
/*             while (System.Math.Abs(Xvalue - mario.transform.position.x) < 2.0f)
            {
                Xvalue = Random.Range(-9.5f, 9.5f);
            } */

            //item.transform.position = new Vector3(Xvalue, groundDistance + item.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            item.transform.position = new Vector3(Xvalue, -0.6f, 0);
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool!");
        }
    }

    void EnemySpawnSequence(){
        ObjectType enemyType =  Random.Range(0, 2) ==  0  ?  ObjectType.gombaEnemy  :  ObjectType.greenEnemy;
        spawnFromPooler(enemyType);
    }
    
}
