using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Data
{
    public Transform _wall;
    public Transform _tile;
}

public class Container : MonoBehaviour
{
    public int index;
    public Data R_data;
    public Data L_data;
    public Data U_data;
    public Data D_data;
    public bool visited;






}
