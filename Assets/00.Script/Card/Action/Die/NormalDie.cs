using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDie : MonoBehaviour,IDie
{
    public void Die()
    {
        Destroy(gameObject);
        Debug.Log("Normal Die");
    }
}