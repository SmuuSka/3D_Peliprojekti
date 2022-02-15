using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour 
{
    [SerializeField] private Camera _cam;

    private void Update()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycasthit))
        {
            Debug.DrawRay(ray.origin, raycasthit.point, Color.green);
        }
       
    }

}
