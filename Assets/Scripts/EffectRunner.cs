using UnityEngine;
using System.Collections;

public abstract class EffectRunner : MonoBehaviour
{
    public string effectName;
    public int effectId;

    public abstract int AttachTo(Transform t);
    public abstract void PassParameters(object[] parameters);

    public void DestroyObject()
    {
        EffectSystem.Instance.RemoveEffect(this.effectId);
    }

}
