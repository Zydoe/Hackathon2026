using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogre : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetMaxHp(7);
        SetHp(GetMaxHp());
        SetStrength(2);
        SetSpeed(0.4f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
