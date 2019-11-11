# Enemy Spawner Tutorial for Basic TDS game.

This tutorial will show you how to set up a basic Enemy spawner for your 3D Game. (Make sure you followed the previous EnemyAI tutorial for this to work)

# 1. Set up your scene.

Make sure you have everything from the previous tutorials in your scene: `Player`, `Enemy`, some obstacles/objects.

Double check that you have made the `Enemy` object a prefab. If not, drag and drop the `Enemy` object into the `Prefabs` folder. 

Then delete the `Enemy` object that is inside of the `Scene`.

Create an Empty GameObject, at the top of Unity click `GameObject` > `Create Empty`, and name it `SpawnManager`. You can place this anywhere you like, 
it will not make a difference.

Now create an Empty GameObject, at the top of Unity click `GameObject` > `Create Empty`.

Change the name of the new object to `SpawnPosition`.

Now we need to change it's tag. While having the `SpawnPosition` selected, go to the `Inspector` window, and locate the `Tag` drop down menu,
click the drop down menu and click `Add Tag...`. Under the `Tags` list, click the plus, and type in `Spawner` and click save. Now select
your `SpawnPosition` object, and in the `Inspector` window set it's `Tag` to `Spawner`.

Whilst you are still in the `Inspector`, left of the `Tag` you will see a gray cube with a drop down indicator, click on the gray cube, and select
any colour you would like to indicate the spawner with. (This is just for visual, helps you visualise where the object is)

Parent the `SpawnPosition` object to the `SpawnManger` object, do this by dragging and dropping the `SpawnPosition` object onto the `SpawnManager` object
in the `Hierarchy` window.

Make sure the `SpawnPosition` object's y position is set to `1.0` in the `Inspector` window.

Drag and drop the `SpawnPosition` object into the `Prefabs` folder.

Now lets duplicate the spawn positions around the play area, have the `SpawnPosition` object selected, and tap `ctrl + D` to duplicate the 
object. 

Place as many spawners you want into the scene and anywhere you want them to be, the `Enemy` will be spawning from each one of these positions.

Set up is complete.

# 2. Spawner Code

Now we will create the code that will make the Enemy spawn.

Create a new Script and call it `SpawnScript`.

Drag and drop the `SpawnScript` onto the `SpawnManager`.

Add the following code into the `SpawnScript`:

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public bool canSpawn = true;

    GameObject[] spawnPositions;
    public GameObject enemyPrefab;
    GameObject[] enemiesInScene;
    public float spawnTimer = 2.0f;
    public int maxNumberOfEnemies = 4;
    int randomNumber;

    void Update()
    {
        spawnPositions = GameObject.FindGameObjectsWithTag("Spawner");
        enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");

        if(canSpawn && enemiesInScene.Length < maxNumberOfEnemies)
        {
            canSpawn = false;
            randomNumber = Random.Range(0, spawnPositions.Length);
            Instantiate(enemyPrefab, spawnPositions[randomNumber].transform.position, spawnPositions[randomNumber].transform.rotation);
            Invoke("SpawnReset", spawnTimer);
        }
    }

    void SpawnReset()
    {
        canSpawn = true;
    }
}
```

You will need to select the `SpawnManager`, and inside the `Inspector` window next to the empty `Enemy Prefab` variable there should be a
circle, click the circle, a small window should pop up. Click on the `Assets` tab inside the new window and select the `Enemy` object. 

Now you can close the small window.

Press play and make sure that the spawner works fine. You may add more spawn positions, and the code will automatically find all the `SpawnPositions`
and make the enemy spawn a random one.

Now we have a small game that we can play, there is way more things we can do to make the game more fun and appealing, but this is only the building
blocks of a game, its core mechanics.

End of tutorial.

