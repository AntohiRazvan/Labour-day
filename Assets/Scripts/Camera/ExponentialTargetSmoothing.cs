using UnityEngine;

[DisallowMultipleComponent]
public class ExponentialTargetSmoothing : MonoBehaviour, ITargetSmoothingAlgorithm
{
	public GameObject target;

	private Vector3 lastState;
	private Vector3 currentState;

	private Vector3 lastTrendSmoothing;

	private bool initialized = false;

	[Range(0.0f, 1.0f)]
	public float alpha;

	[Range(0.0f, 1.0f)]
	public float beta;

	public Vector3 SmoothedTargetPosition
	{
		get
		{
			return initialized ? currentState : target.transform.position;
		}
	}

	public void Awake()
	{
		initialized = true;

		lastState = target.transform.position;
		currentState = target.transform.position;
		lastTrendSmoothing = Vector3.zero;
	}

	public void Update()
	{
		lastState = currentState;
		currentState = alpha * target.transform.position + (1.0f - alpha) * (lastState + lastTrendSmoothing);
		lastTrendSmoothing = beta * (currentState - lastState) + (1.0f - beta) * lastTrendSmoothing;
		transform.LookAt(target.transform);

	}

}
