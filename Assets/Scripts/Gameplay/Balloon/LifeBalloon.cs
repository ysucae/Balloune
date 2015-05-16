﻿using UnityEngine;
using System.Collections;

public class LifeBalloon : Balloon {

	private const float LIFE_BALLOON_DISTANCE = 4f;
    override public void Init()
    {
        base.Init();
        ChangeColor(Color.red);
        AddBehavior<LifeBehavior>();
        AddBehavior<CharacterPullBehavior>();
		m_MaxBalloonDistance = LIFE_BALLOON_DISTANCE;
	}
	
	void Update () {
	    
	}
}