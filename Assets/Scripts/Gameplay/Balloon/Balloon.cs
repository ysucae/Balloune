﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Balloon : MonoBehaviour {

    public EBalloonType Type { get; set; }
    public float Mass { get; set; }

    private CircleCollider2D mCircleCollider = null;
    private BalloonPhysics mPhysics = null;

    private List<BalloonBehavior> mBehaviors;
	private BalloonHolder mBalloonHolder;
	private int mBalloonIndex;

	virtual public void Init () {
        mBehaviors = new List<BalloonBehavior>();
        mCircleCollider = GetComponent<CircleCollider2D>();
        mPhysics = GetComponent<BalloonPhysics>();
        AddBehavior<DefaultBehavior>();
	}

    protected void ChangeColor(Color pColor)
    {
        GetComponent<SpriteRenderer>().color = pColor;
    }

	void Update () {
	
	}

    protected void AddBehavior<T>() where T : BalloonBehavior
    {
        var behavior = gameObject.AddComponent<T>();
        mBehaviors.Add(behavior);
    }

    public CircleCollider2D CircleCollider
    {
        get { return mCircleCollider; }
    }

    public BalloonPhysics Physics
    {
        get { return mPhysics; }
    }

    public virtual void OnMove(float pDistance)
    {
        foreach(BalloonBehavior behavior in mBehaviors)
        {
            behavior.OnMove(pDistance);
        }
    }

    public virtual void OnPop()
    {
        foreach (BalloonBehavior behavior in mBehaviors)
        {
            behavior.OnPop();
        }
    }

	public void SetBalloonHolder(BalloonHolder pBalloonHolder)
	{
		mBalloonHolder = pBalloonHolder;
		mPhysics.SetBalloonHolder (mBalloonHolder);
	}

	public void SetBalloonIndex(int pBalloonIndex)
	{
		mBalloonIndex = pBalloonIndex;
		mPhysics.SetBalloonIndex (mBalloonIndex);
	}

	public int BalloonIndex
	{
		get { return mBalloonIndex; }
	}
}
