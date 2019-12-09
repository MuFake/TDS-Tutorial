using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookScript : MonoBehaviour
{
    public Camera mainCam;

    void Update()
    {
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            transform.LookAt(hit.point);
        }

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
