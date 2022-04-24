using System;
using UnityEngine;

public class TriggerBridge : MonoBehaviour
{
	public event Action<Collider> CollisionBegin;
	public event Action<Collider> CollisionEnd;
	public event Action<Collider> CollisionStays; 

	void OnTriggerEnter(Collider coll)
	{
		if (CollisionBegin != null)
			CollisionBegin(coll);
	}

	void OnTriggerExit(Collider coll)
	{
		if (CollisionEnd != null)
			CollisionEnd(coll);
	}

	void OnTriggerStay(Collider coll)
	{
		if (CollisionStays != null)
			CollisionStays(coll);
	}
}
