using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canonball : MonoBehaviour
{
    private CanonballBase canonballBase;

    public void SetCanonballBase(CanonballBase cBase)
    {
        canonballBase = cBase;
    }

    public int GetDamage()
    {
        return canonballBase.damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(SetCannoballActiveFalse());
    }

    private IEnumerator SetCannoballActiveFalse()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
    
}
