using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject,ISerializationCallbackReceiver
{
    [Header("Value Running in Game")]
    public Vector2  initialValue;
    [Header("Value By Default")]
    public Vector2 defaultValue;
    [Header("AnimationPositions")]
    public float moveX;
    public float moveY;
    public void OnBeforeSerialize()
    {

    }
    public void OnAfterDeserialize()
    {
        initialValue = defaultValue;
    }
    // Start is called before the first frame update

}
