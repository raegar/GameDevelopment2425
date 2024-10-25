
using UnityEngine;

public class Panel : MonoBehaviour
{
    protected virtual void Awake()
    {
        UIManager.Instance.Register(this);
    }
}
