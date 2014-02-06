// --------------------------------------------------------------------------------------------- //
// (c) partial pi 2012-2014
// Creative Commons Attribution-NonCommercial 4.0 International (CC BY-NC 4.0)
// http://creativecommons.org/licenses/by-nc/4.0/deed.en_US
// --------------------------------------------------------------------------------------------- //

using UnityEngine;
using System.Collections;

using pi.unity.spriteskeleton;
using pi.unity.util;

namespace pi.unity.demo
{
	/// <summary>
	/// Moves the game object to a specific destination 
	/// </summary>
	public class MovementBehaviour : MonoBehaviour 
	{
		/// <summary>
		/// The current destination to walk to.
		/// </summary>
		public Vector3 destination;

		/// <summary>
		/// The max speed in units / second.
		/// </summary>
		public float   maxSpeed	= 1;

		/// <summary>
		/// The deceleration, units / second slow down.
		/// </summary>
		public float   deceleration = 5;

		/// <summary>
		/// The acceleration: units / speed increase.
		/// </summary>
		public float   acceleration = 1;

		/// <summary>
		/// The dead zone radius. If the behaviour is within this radius
	    /// to the target 
		/// </summary>
		public float   deadZoneRadius = 0.2f;

		/// <summary>
		/// The minimum required speed. Below this speed the behaviour will
		/// stop moving.
		/// </summary>
		public float   minSpeed = 0.1f;

		/// <summary>
		/// The current speed.
		/// </summary>
		private float	 		  _speed;

		/// <summary>
		/// The cached _animator, used to set the 'velocity' parameter. 
		/// </summary>
		private Animator 		  _animator;

		void Start () 
		{
			_animator    = gameObject.FindComponentByType<Animator>();
		}
						 
		/// <summary>
		/// Updates the position moving the gameobject to the destination if
		/// necessary. When the destination is reached the behaviour
		/// will disable itself. 
		/// This behaviour will also update the angle so the sprite
		/// will always face the right direction.
		/// Finally the velocity parameter of the animator (if any)
		/// will be set to match the speed.
		/// </summary>
		void Update () 
		{
			Vector3 direction = destination - transform.position;

			direction.z  = 0;

			if ( direction.sqrMagnitude > deadZoneRadius * deadZoneRadius )
			{
				_speed = Mathf.Min( maxSpeed, _speed + acceleration * Time.deltaTime );
			}
			else
			{
				_speed = Mathf.Max( 0, _speed - deceleration * Time.deltaTime );

				if ( _speed * _speed < minSpeed * minSpeed )
				{
					_speed = 0;
					enabled = false;
				}
			}

			transform.position = transform.position + ( direction.normalized * Time.deltaTime * _speed );

			if ( direction.x < 0 )
			{
				Vector3 euler = transform.rotation.eulerAngles; 
				euler.y = 0;
				transform.rotation = Quaternion.Euler( euler );
			}
			else if ( direction.x > 0 )
			{
				Vector3 euler = transform.rotation.eulerAngles; 
				euler.y = -180;
				transform.rotation = Quaternion.Euler( euler );
			}

			if ( _animator != null )
			{
				_animator.SetFloat( "velocity", _speed / maxSpeed );
			}
		}
	}
}
