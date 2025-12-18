using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "StrategySO", menuName = "Scriptable Objects/StrategySO")]
public abstract class StrategySO : ScriptableObject
{
    protected GameObject owner;

    public virtual void Init(GameObject owner)
    {
        this.owner = owner;
    }

    public abstract IEnumerator AI();
}
