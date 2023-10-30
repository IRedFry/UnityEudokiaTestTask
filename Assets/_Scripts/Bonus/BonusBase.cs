using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusBase : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CanonBall")
        {
            OnBonusUse();
            Destroy(gameObject);
        }
    }
    protected abstract void OnBonusUse();
}
