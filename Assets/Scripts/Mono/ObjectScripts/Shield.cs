﻿using UnityEngine;

namespace AIRogue.GameObjects {

	/// <summary>
	/// A gameplay unit used in Pawn of Kings.
	/// </summary>
	abstract class Shield : MonoBehaviour, IDamageable
	{
		public float HitPointCapacity { get; private set; }
		public float HitPoints { get; private set; }
		public float ShieldPercentage
		{
			get {
				return HitPoints / HitPointCapacity;
			}
		}

		private float secSinceLastDamage; //updated each frame

		protected virtual void Awake(){}
		protected virtual void Start(){}
		protected virtual void Update()
		{
			secSinceLastDamage += Time.deltaTime;
		}

		public void Initialize(float shieldCapacity)
		{
			HitPointCapacity = shieldCapacity;
			HitPoints = shieldCapacity;
		}

		/// <summary>
		/// Damages this Shields and returns the amount of damage that 
		/// could not be absorbed.  Should only be called via the IDamageable 
		/// interface obtained on collision (so only called when this shield collider
		/// is hit directly).
		/// </summary>
		/// <param name="damage"></param>
		/// <returns>Through Damage</returns>
		public void TakeDamage(float damage, Collision collision)
		{
			var throughDamage = 0f;

			HitPoints -= damage;
			secSinceLastDamage = 0; ////////////////////////fsdfasdfasdf

			if (HitPoints < 0)
			{
				HitPoints = 0;
				throughDamage = HitPoints * -1f;
			}

			SetConditionApperance();

			// turn shield off *after* setting apperance
			if (HitPoints == 0)
			{
				ShieldOff();
			}

			if (throughDamage > 0)
			{
				// damage the ship
			}
		}

		protected abstract void SetConditionApperance();
		protected abstract void ShieldOff();
		protected abstract void ShieldOn();
	}
}