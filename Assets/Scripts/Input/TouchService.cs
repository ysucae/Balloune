﻿/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using Radix.Logging;
using UnityEngine;

#if UNITY_IOS
public class TouchService : iOSTouchService
#else
public class TouchService : DefaultTouchService
#endif
{
    #region static function
    private static TouchService mInstance = null;

    public static Vector2 CurrentTouchPosition
    {
        get
        {
            if (mInstance)
            {
                return mInstance.mTouchControl.Position;
            }
            else
            {
                return Vector2.zero;
            }
        }
    }

    protected override void Start()
    {
        mInstance = this;
        base.Start();
    }
    #endregion

    #region TouchBegan
    protected override void OnTouchBegan(Touch pTouch)
    {
        if(mTouchControl == null)
        {
            mTouchControl = new TouchControl(pTouch);

            HandlerNewTouch();
        }
    }

    private void HandlerNewTouch()
    {
        if (mTouchControl.IsDoubleTap)
        {
            DispatchDoubleTapEvent();
        }
        else
        {
            DispatchTapEvent();
        }
    }

    private void DispatchTapEvent()
    {
        EventService.DispatchEvent(ETouchEvent.TAP, mTouchControl.Position);
        Log.Create("Touch Begin: TAP", ELogType.DEBUG);
    }

    private void DispatchDoubleTapEvent()
    {
        EventService.DispatchEvent(ETouchEvent.DOUBLE_TAP, mTouchControl.Position);
        Log.Create("Touch begin: DOUBLE TAP", ELogType.DEBUG);
    }

    #endregion

    #region TouchMoved

    protected override void OnTouchMoved(Touch pTouch)
    {
        if(IsCurrentTouch(pTouch))
        {
            mTouchControl.UpdateTouch(pTouch);

            bool oldSwipe = mTouchControl.IsSwiping;

            mTouchControl.Moved();

            if(!oldSwipe && mTouchControl.IsSwiping)
            {
                DispatchSwipeBeginEvent();
            }
        }
    }

    private void DispatchSwipeBeginEvent()
    {
        EventService.DispatchEvent(ETouchEvent.SWIPE_BEGIN, mTouchControl.SwipeDirection);
        Log.Create("SWIPE_BEGIN, " + mTouchControl.SwipeDirection.ToString(), ELogType.DEBUG);
    }

    #endregion

    #region TouchStationary

    protected override void OnTouchStationary(Touch pTouch)
    {
        if (IsCurrentTouch(pTouch))
        {
            mTouchControl.UpdateTouch(pTouch);
            mTouchControl.OnStationary();
            if (mTouchControl.IsStationary)
            {
                EndSwipe();
            }
        }
    }

    #endregion

    #region TouchEnded

    protected override void OnTouchEnded(Touch pTouch)
    {
        //check touch null.. Dispose ???
        if (IsCurrentTouch(pTouch))
        {
            EndSwipe();
            mTouchControl = null;
            DispatchTouchEnded();
        }
    }

    private void DispatchTouchEnded()
    {
        EventService.DispatchEvent(ETouchEvent.END);
        Log.Create("TOUCH END", ELogType.DEBUG);
    }

    #endregion

    #region TouchCanceled

    protected override void OnTouchCanceled(Touch pTouch)
    {
        //Touch cancelled
        OnTouchEnded(pTouch);
    }

    #endregion

    #region Private Utilities Method

    private bool IsCurrentTouch(Touch pTouch)
    {
		return mTouchControl != null && mTouchControl.ID == pTouch.fingerId;
    }

    private void UpdateCurrentTouch(Touch pTouch)
    {
        mTouchControl.UpdateTouch(pTouch);
    }

    private void EndSwipe()
    {
        if(mTouchControl.IsSwiping)
        {
            DispatchSwipeEndEvent();
            mTouchControl.EndSwipe();
        }
    }

    private void DispatchSwipeEndEvent()
    {
        EventService.DispatchEvent(ETouchEvent.SWIPE_END, mTouchControl.SwipeDistance);
        Log.Create("SWIPE_END, " + mTouchControl.SwipeDistance.ToString(), ELogType.DEBUG);
    }
    #endregion
}
