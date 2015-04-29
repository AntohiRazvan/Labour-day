using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(ITargetSmoothingAlgorithm))]
public class VolumeBoundedCamera : MonoBehaviour
{
	public ITargetSmoothingAlgorithm targetSmoothingAlgorithm = null;

	public Vector3 Offset
	{
		get
		{
			return offset;
		}

		set
		{
			offset = value;
		}
	}

	private Vector3 offset;

	public Bounds boundsVolume;

	public void Awake()
	{
		targetSmoothingAlgorithm = gameObject.GetComponent<ITargetSmoothingAlgorithm>();

		Offset = transform.position - targetSmoothingAlgorithm.SmoothedTargetPosition;
	}

	public void Update()
	{
		if (boundsVolume.size.sqrMagnitude > 0.0f)
			transform.position = Vector3.Max(Vector3.Min(boundsVolume.max, targetSmoothingAlgorithm.SmoothedTargetPosition + Offset), boundsVolume.min);
		else
		{
			transform.position = targetSmoothingAlgorithm.SmoothedTargetPosition + Offset;
		}
	}
}