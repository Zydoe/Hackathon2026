using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetMaxHp(3);
        SetHp(GetMaxHp());
        SetStrength(1);
        SetSpeed(0.7f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

}
