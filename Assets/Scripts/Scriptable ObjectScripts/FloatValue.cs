﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu]
public class FloatValue : ScriptableObject,ISerializationCallbackReceiver
{
    public float initialValue;
    [NonSerialized]
    public float RuntimeValue;
    public void OnBeforeSerialize()
    {
       
    }
    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }
}

