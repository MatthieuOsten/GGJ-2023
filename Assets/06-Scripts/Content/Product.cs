using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProduct",menuName = "Product")]
public class Product : ScriptableObject
{
    public new string name;
    public Sprite sprite;
}
