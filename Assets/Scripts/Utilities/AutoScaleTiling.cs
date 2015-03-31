﻿using UnityEngine;
using System.Collections;

//prototype code
public class AutoScaleTiling : MonoBehaviour {

	public Vector2 m_TileRatio = new Vector2(1,1);

	void Start () 
	{
		this.GetComponent<Renderer>().material.mainTextureScale=new Vector2(this.transform.localScale.x*m_TileRatio.x,this.transform.localScale.z*m_TileRatio.y);
		Destroy(this);
	}

}