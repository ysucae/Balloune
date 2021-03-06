﻿using UnityEngine;
using System.Collections;
using Radix.Event;

public class BalloonSensor : CharacterSensor {
	private CharacterPull mCurrentPull = null;

	protected override void Start ()
	{
		base.Start ();

		EventService.Register<CharacterPullDelegate>(EGameEvent.BEGIN_PULLING, OnBeginPulling);
		EventService.Register(EGameEvent.END_PULLING, OnStopPulling);
	}

	void Update () {
		if (mCurrentPull != null) 
        {
			UpdateSpeedParamater(mCurrentPull.Strength);
		} else 
        {
			UpdateSpeedParamater(0f);
		}
	}

	public void OnBeginPulling(CharacterPull pArg, Balloon pBalloon)
	{
		mCurrentPull = pArg;
	}
	
	public void OnStopPulling()
	{
		mCurrentPull = null;
	}

	private void UpdateIsPullingParamater()
	{
		mAnimator.SetBool(IS_PULLING_PARAMATER, mCurrentPull != null);
	}

	private void UpdateSpeedParamater(float pSpeed)
	{
		mAnimator.SetFloat(SPEED_PARAMATER, pSpeed);
	}
}
