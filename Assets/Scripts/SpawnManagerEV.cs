using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManagerEV : MonoBehaviour
{
    public GameConstants gameConstants;
    void Start()
    {
        Debug.Log("Spawnmanager start");
        for (int j = 0; j < 2; j++)
            spawnFromPooler(ObjectType.greenEnemy);

    }

    void startSpawn(Scene scene, LoadSceneMode mode)
    {
        for (int j = 0; j < 1; j++)
            spawnFromPooler(ObjectType.gombaEnemy);
    }


    void spawnFromPooler(ObjectType i)
    {
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);

        if (item != null)
        {
            //set position
            item.transform.localScale = new Vector3(1, 1, 1);
            //item.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), gameConstants.groundDistance + item.GetComponent<SpriteRenderer>().bounds.extents.y, 0);

            //idk why the y position is always fixed, so i hard coded the spawn position
            item.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), -2.5f, 0);
            
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool!");
        }
    }

    public void spawnNewEnemy()
    {   Debug.Log("spawned new Enemy");
        ObjectType i = Random.Range(0, 2) == 0 ? ObjectType.gombaEnemy : ObjectType.greenEnemy;
        spawnFromPooler(i);
    }

}