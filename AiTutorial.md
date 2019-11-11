# Top Down Game AI

This tutorial will show you how to set up a a basic pathfinding AI for your 3D game.

## 1. Set up your Scene

Make sure you have a level of some sort, its optional but you can add some obstacles into your level such as walls and boxes.

Select all of your obstacles in your scene or anything that may be an obstacles (walls, crates, anything that would/should stop 
you from walking through it) and in the top left of `Inspector` tab locate a tick box with `Static` next to it. Click the drop down menu
and select `Navigation Static`. 

We need the `Navigation` window. At the top of the Unity window locate `Window` > `AI` > `Navigation`. 

A `Navigation` window should appear.

Now select all of the obstacles in the scene with your floot object (any object you want your AI to walk on or avoid) and in the `Navigation`
window select `Object` tab and make sure the `Navigation Static` is ticked.

Then click `Bake` tab and click the `Bake` button at the bottom. This should create a procedural mesh (`NavMesh`) above and around your level and objects. 
We will use this to tell the AI where it can and can't go.

Scene set up is complete

## 2. Creating the AI

Now we will create a simple AI that will follow and attack the player.

At the top of Unity, go to `GameObject` > `3D Object` > `Capsule`.

While having the `Capsule` selected, go to the `Inspector` window, and set it's y position to 1. You may move this new capsule anywhere 
in the scene you like as long as it is above the `NavMesh`.

You may create a new `Material` and place it on the new `Capsule` and make it a different colour to the `Player`.

Change the `Capsule` name to `Enemy`.

At the top of the `Inspector` window locate the `Tag` drop down menu, click it, then click `Add tag...`. Under the `Tags` list click the plus 
and name the new tag as `Enemy` and click save.

Now select the `Enemy` object, and at the top left of the `Inspector` window click the `Tags` drop down menu and click the `Enemy` tag we just created.

While having the `Enemy` object selected, at the bottom of the `Inspector` window click `Add Component`, type "NavMesh" into the search bar
and click the `NavMesh Agent`. This should add the `NavMesh Agent` component to the `Enemy` object.

We have finished creating the AI.

## 3. AI script.

Now we will be creating the code that makes the AI work.

Create a new Script and call it `EnemyAI`.

Drag and drop the `EnemyAI` script onto the `Enemy` object. 

Put the following code into the `EnemyAI` script.

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    public bool chasePlayer = true;
    public float moveSpeed = 3.0f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if(chasePlayer && player != null && agent.isOnNavMesh)
        {
            agent.speed = moveSpeed;
            agent.destination = player.position;
        }
    }
}

```

Make sure the Script is on the `Enemy` object, then click Play to make sure the AI works.

You may put the `Health` script onto the `Enemy` object which was made in the previous tutorial. This should make the `Enemy` die
if shot enough times. (Make sure to set it's health value)

## 3,5. Enemy Detecting the Player

If you wish to make the AI detect the `Player` if close enough you can follow these extra steps. [If not, skip to step 4]

In the variables section of the `EnemyAI` script add the following:

```
 public float detectionDistance = 5.0f;
 public float curDistance;
```

And in the `void Update` add the following:

```
curDistance = Vector3.Distance(player.position, transform.position);

        if (curDistance <= detectionDistance)
        {
            chasePlayer = true;
        }
        else
            chasePlayer = false;
```

Make sure that specific part of the code is inside of `void Update` and outside of any other function/statement.

Now you can press Play and check if the code works correctly, depending on the `detectionDistance` value, the `Enemy` object should 
start to chase the `Player` if they are close enough, and stop chasing the `Player` if they are far enough.

## 4. Enemy applying damage to the Player

Now we will add the ability for the player to take damage from the AI.

Drag and drop the `Health` script onto the `Player` and set the Health value to something, in this case `100`.

Inside of the `EnemyAI` script we need to change initial `if` statement from:

```
if(chasePlayer && player != null && agent.isOnNavMesh)
```

to:

```
if(chasePlayer && player != null && agent.isOnNavMesh && !isAttacking)
```

And we need to add the following code into the `EnemyAI` script (into the `void Update`):

```
RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, attackDistance) && !isAttacking)
        {
            if(hit.transform.gameObject.name == "Player")
            {
                isAttacking = true;
                hit.transform.gameObject.BroadcastMessage("ApplyDamage", damage);
                Invoke("AttackTimeDelay", attackTimer);
            }
        }
```

And the final part of the code has to go outside of the `void Update`:

```
 void AttackTimeDelay()
    {
        isAttacking = false;
    }
```

Now if we press Play, we should see the `Enemy` stand still unless we get too close, which then it will start to chase us.
If we dont move, the `Enemy` will get close, do damage to the `Player` and stand still for 1 second, then it will proceed 
to chase and attack the `Player` again.

You may drag and drop the `Enemy` object into the `Prefabs` folder, for we will use the `Enemy` prefab to spawn many Enemies in the next
tutorial.

This is the end of this tutorial.
