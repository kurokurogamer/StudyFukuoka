using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class Book1 : ScriptableObject
{
    public List<MstItemEntity> Sheet1; // Replace 'EntityType' to an actual type that is serializable.
}
