// --------------------------------------------------------------------------------------------- //
// (c) partial pi 2012-2014
// Creative Commons Attribution-NonCommercial 4.0 International (CC BY-NC 4.0)
// http://creativecommons.org/licenses/by-nc/4.0/deed.en_US
// --------------------------------------------------------------------------------------------- //

using UnityEngine;

namespace pi.unity.demo
{
	/// <summary>
	/// Spawns a random prefab at the current position at regular intervals.
	/// Moves from a start position X number of units.
	/// </summary>
	public class SpawnerBehaviour : MonoBehaviour
	{
		/// <summary>
		/// List of prefabs from which a random prefab will be selected
		/// </summary>
		public GameObject[] prefabs;

		/// <summary>
		/// Max range over which the spawner moves
		/// </summary>
		public float		rangeY;

		/// <summary>
		/// speed of movement
		/// </summary>
		public float		speedY;

		/// <summary>
		/// The spawn interval in seconds.
		/// </summary>
		public float		spawnInterval = 1;

		/// <summary>
		/// Spawns will move until (0, minX, 0) then they will be destroyed
		/// </summary>
		public float		minX = -6;

		/// <summary>
		/// Prefabs will move at a random speed
		/// </summary>
		public float        deltaSpeed  = 0.4f;

		/// <summary>
		/// Prefabs will be spawned with a random height. 
		/// </summary>
		public float        deltaHeight = 0.1f;

		/// <summary>
		/// Spawned prefabs will start at the spawner's position - 0, deltaSpawnPosition, 0 
		/// </summary>
		public float  		deltaSpawnPosition = 4;

		/// <summary>
		/// Keeps track of when the last spawn took place 
		/// </summary>
		private float		_lastSpawnTime = 0;

		/// <summary>
		/// Start position of the spawner
		/// </summary>
		private Vector3		_startPosition;

		/// <summary>
		/// The _distance traveled by the spawner thus far.
		/// </summary>
		private float		_distance = 0;

		public void Start()
		{
			_startPosition = gameObject.transform.position;
		}

		public void Update()
		{
			// time to spawn yet ?
			if ( ( Time.time - _lastSpawnTime ) > spawnInterval )
			{
				_lastSpawnTime = Time.time;

				// spawn a random prefab
				GameObject spawn = (GameObject) GameObject.Instantiate( prefabs[Random.Range(0, prefabs.Length )], transform.position, transform.rotation ); 

				// add a control behaviour to make the spawn move by itself
				spawn.AddComponent<MovementControlBehaviour>().minX = minX;

				// add some variation to the movespeed
				spawn.GetComponent<MovementBehaviour>().maxSpeed += Random.Range ( -deltaSpeed, deltaSpeed );

				// add some variation to the scale height
				spawn.transform.localScale = spawn.transform.localScale * ( 1.0f + Random.Range( -deltaHeight, deltaHeight ) ); 

				// move the spawn somewhat around so not all the spawns appear on the screen at the same time
				spawn.transform.position   = spawn.transform.position + Vector3.right * Random.Range( 0, deltaSpawnPosition );
			}

			// move the spawner
			_distance += speedY * Time.deltaTime;
			transform.position = new Vector3( _startPosition.x, _startPosition.y + _distance, _startPosition.z );

			// time to restart from the beginning ?
			if ( _distance > rangeY )
			{
				_distance = 0;
			}
		}
	}
}


