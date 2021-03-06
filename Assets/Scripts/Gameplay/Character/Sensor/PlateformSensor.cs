﻿using UnityEngine;
using System.Collections;
using Radix.Event;

public class PlateformSensor : CharacterSensor {

	private float AJUST_X = 0.5f;

	Balloon mBalloon = null;

	protected override void Start ()
	{
		base.Start ();
		
		EventService.Register<CharacterPullDelegate>(EGameEvent.BEGIN_PULLING, OnBeginPulling);
		EventService.Register(EGameEvent.END_PULLING, OnStopPulling);
	}

	void FixedUpdate () {
        if(IsInMovingState() && mBalloon != null)
		{
            Vector2 left = Vector2.zero;
            Vector2 right = Vector2.zero;
            
            GetVector(out left, out right);
            Check(left, right);
		}
	}
	
    private void GetVector(out Vector2 pLeft, out Vector2 pRight)
    {
        float speed = GetSpeedParamater();

        if(speed < 0)
        {
            pLeft = GetTopLeftCorner();
            pLeft.x -= AJUST_X;
            pRight =  GetTopRightCorner();
            
        }
        else
        {
            pRight = GetTopRightCorner();
            pRight.x += AJUST_X;
            pLeft = GetTopLeftCorner();
        }

        pLeft.y += 0.3f;
        pRight.y += 0.3f;
    }

	private void OnBeginPulling(CharacterPull pArg, Balloon pBalloon)
	{
		mBalloon = pBalloon;
	}
	
	private void OnStopPulling()
	{
		mBalloon = null;
	}
	
	private void Check(Vector2 pLeft, Vector2 pRight)
	{
        Debug.DrawLine (pLeft, pRight, Color.yellow);

        RaycastHit2D cast = Physics2D.Linecast(pLeft, pRight, PlateformLayerMask);

        if(cast)
		{
            CheckBalloon(pLeft.y, cast.collider);
		}
	}

	private void CheckBalloon(float pY, Collider2D pPlatformCollider)
	{
        float width = pPlatformCollider.bounds.size.x;
        Vector2 platformPos = pPlatformCollider.transform.position;

        Vector2 balloon = mBalloon.transform.position;
        if (balloon.y > pY 
            && balloon.x > platformPos.x - width / 2
            && balloon.x < platformPos.x + width / 2)
        {
            UpdatePlateformParamaters();
		}
	}

    private void UpdatePlateformParamaters()
    {
        mAnimator.SetBool (JUMP_PARAMATER, true);
        mAnimator.SetBool(PLATEFORM_PARAMATER, true);
    }
}
