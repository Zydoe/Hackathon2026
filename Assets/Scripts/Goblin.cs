using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        SetMaxHp(3);
        SetHp(GetMaxHp());
        SetStrength(2);
        SetSpeed(0.9f);
        SetCoins(10);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
