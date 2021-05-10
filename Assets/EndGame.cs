using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public void EndAnimationPlayer()
    {
        print("UEPA");
        Destroy(GameObject.Find("MonsterH"));
        GameObject.FindWithTag("Player").GetComponent<Animator>().Play("RaincoatEnd");
    }
}
