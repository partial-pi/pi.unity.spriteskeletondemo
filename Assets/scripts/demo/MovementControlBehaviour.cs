// --------------------------------------------------------------------------------------------- //
// (c) partial pi 2012-2014
// Creative Commons Attribution-NonCommercial 4.0 International (CC BY-NC 4.0)
// http://creativecommons.org/licenses/by-nc/4.0/deed.en_US
// --------------------------------------------------------------------------------------------- //

using UnityEngine;

namespace pi.unity.demo
{
	/// <summary>
	/// Class that takes care of the movement behaviour of a gameobject
	/// and steers it to the left of the screen after which it will terminate
	/// the gameobject.
	/// </summary>
	public class MovementControlBehaviour : MonoBehaviour
	{
		/// <summary>
		/// The minimum x coordinate. If the game object reaches this position, 
		/// this behaviour will destroy the game object.
		/// </summary>
		public float minX;
		private MovementBehaviour _controller;

		public void Start()
		{
			_controller = GetComponent<MovementBehaviour>();

			_controller.destination = new Vector3( minX - 1, transform.position.y, transform.position.z );
			_controller.enabled     = true;
		}

		/// <summary>
		/// Checks if the game object has reached the given position. If so the gameobject will be 
		/// destroyed.
		/// </summary>
		public void Update()
		{
			if ( transform.position.x < minX )
			{
				GameObject.Destroy( gameObject );
			}
		}
	}
}