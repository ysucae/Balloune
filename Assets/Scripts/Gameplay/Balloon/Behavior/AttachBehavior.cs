using UnityEngine;
using System.Collections;
using Radix.Event;
using System;

public class AttachBehavior : BalloonBehavior
{
	private BalloonPhysics mPhysics;
	
	protected override void Start () {
		base.Start ();
		mPhysics = mBalloon.Physics;
		EventListener.Register(EGameEvent.DROP_BALLOON, OnDropBalloon);
	}
	
	void Update () {
		
	}
	
	void FixedUpdate() {

	}
	
	private void OnDropBalloon(Enum pEvent, object pBalloon)
	{
		if (((Balloon) pBalloon).GameObject == mBalloon.gameObject) {
			Vector2 position = transform.position;
			EventService.DipatchEvent(EGameEvent.ATTEMPT_ATTACH_BALLOON, pBalloon, position);
		}
	}
}
