﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAheadEnemy : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        target = PlayerController.instance.aheadTarget;
    }
}
