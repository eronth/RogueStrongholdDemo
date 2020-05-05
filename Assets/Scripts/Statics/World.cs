using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class World
{
    public static Grid Grid 
        = GameObject.Find("WorldGrid").GetComponent<Grid>();
}
