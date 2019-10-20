# Top Down Character Controller Tutorial

This tutorial will show how to create and set up a 3D top down player controller with camera movement and player movement.

## 1. Set up your Scene

Start by creating a new scene and naming it what ever you like.

Create a Plane and its Position to `[ 0, 0, 0]`.

Then create a Capsule and name it `Player`.

Make sure the `Player` is positioned at `[ 0, 1, 0]` so that it is not clipping through the ground.

Now parent the `Main Camera` to `Player` and set the `Main Camera` position to `[0, 10, 0]` and rotation to `[ 90, 0 0]`.

Once the Transform is set, unparent the `Main Camera` from `Player`.

Make sure in the Game view you can see the `Player` and the Ground.

You may add some obstacles around the level with basic 3D shapes.

## 2. Set up the Character Controller

Create a new script c# and name it `PlayerController`.

Put this code inside of the `PlayerController` script and Save it.

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Variables")]
    CharacterController cC;
    public float moveSpeed = 5.0f;
    public float jumpHeight = 10.0f;
    public float gravity = 20.0f;
    Vector3 moveDirection = Vector3.zero;


    void Start()
    {
        cC = GetComponent<CharacterController>();
    }


    void Update()
    {
        var newMoveDirection = new Vector3(Input.GetAxis("Horizontal"),0 , Input.GetAxis("Vertical"));
        newMoveDirection *= moveSpeed;
        if(cC.isGrounded)
        {
            moveDirection = newMoveDirection;
        }
        else
        {
            moveDirection = new Vector3(newMoveDirection.x, moveDirection.y, newMoveDirection.z);
        }

        if(Input.GetKeyDown(KeyCode.Space) && cC.isGrounded)
        {
            moveDirection.y = jumpHeight;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        cC.Move(moveDirection * Time.deltaTime);
    }
}
```

This code allows the player to move forwards/backwards and left/right. It also lets the player Jump, and applies gravity.

Drag and Drop this script onto the `Player`.

Press play to make sure it works. [W A S D or Arrow Keys to move and Space to jump]

## 2.Set up Camera Follow

Now we will set up the `Main Camera` to follow the `Player`.

Add the following Variables beneath the already existing Variables.

```
    [Header("Camera Variables")]
    public Transform camera;
    public float ofsetY = 10.0f;
    public float smooth = 5.0

```

And add the following code into the `Void Update()`.

```
camera.position = Vector3.Lerp(camera.position, new Vector3(transform.position.x, transform.position.y + ofsetY, transform.position.z), smooth * Time.deltaTime);

```
Assign the `Main Camera` to the `camera` variable in the `PlayerController` script.

Play the game and make sure the `Main Camera` follows the `Player`. If need be, you may increase or decrease the value of `smooth` if you want the follow speed to be increased or decreased.


## 3. End

You have finished the `PlayerController` tutorial, you can run the game to test if the components work. 

You can also change the public variables in Unity to customize the controller to how ever you like it.

To finish off, create a new folder and name it `Prefabs`.

Drag and drop the `Player` and the `Main Camera` into the `Prefabs` folder.

Now you may drag and drop the `Player` and the `Main Camera` into your new scene to have a functioning Controller.
