using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaContainer : MonoBehaviour
{
    public Meta[,] metaData= new Meta[5,5]
    {
        { new Meta(), new Meta(), new Meta(), new Meta(), new Meta()},
        { new Meta(), new Meta(), new Meta(), new Meta(), new Meta()},
        { new Meta(), new Meta(), new Meta(), new Meta(), new Meta()},
        { new Meta(), new Meta(), new Meta(), new Meta(), new Meta()},
        { new Meta(), new Meta(), new Meta(), new Meta(), new Meta()},
    };
}
