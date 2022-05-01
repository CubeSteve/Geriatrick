using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3Int position;
    public int gCost = 0;
    public int hCost = 0;
    public int fCost = 0;

    public Node parent;
}
