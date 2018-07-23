﻿using System.Linq;
using AIRogue.GameObjects;
using AIRogue.GameState.Battle.BehaviorTree;
using UnityEngine;

namespace AIRogue.GameState.Battle
{
	/// <summary>
	/// Implements UnitController and used by ArmyManager as a possible Generic type.
	/// This is a controller for AI units and its behaviors will not be choosen 
	/// through the GUI, but through AI logic.
	/// </summary>
	class AIController : UnitController {

		protected bool NewTargetSelected { get; set; }
		private void newTargetNotification(UnitController controller)
		{
			NewTargetSelected = true;
		}

		private Behavior behaviorTree;

		public override void Initialize(Unit unit, Squad squad)
		{
			behaviorTree = new AIBehaviorRoot( this );
			base.Initialize( unit, squad );

			OnTargetChosen += newTargetNotification;
		}

		protected override Behavior GetUnitBehavior()
		{
			return behaviorTree.EvaluateTree();
		}

		public void UpdateTarget()
		{
			Target = closestAttacker();

			// if attacker exists
			if (Target == null)
			{
				Target = closestAllyTarget();
			}
		}

		private float DistanceToUnit(Unit unit)
		{
			return Vector3.Distance( Unit.transform.position, unit.transform.position );
		}
		private Unit closestAttacker()
		{
			Unit attacker = null;

			if (Attackers.Length > 0)
			{
				float shortest = DistanceToUnit( Attackers[0] );
				attacker = Attackers[0];

				for (int i = 1; i < Attackers.Length; i++)
				{
					float dist = DistanceToUnit( Attackers[i] );
					if (dist < shortest)
					{
						shortest = dist;
						attacker = Attackers[i];
					}
				}
			}

			return attacker;
		}
		// maybe change to closestAllyAttacker
		private Unit closestAllyTarget()
		{
			Unit allyTarget = null;

			if (AlliesWithTargets.Length > 0)
			{
				float shortest = DistanceToUnit( AlliesWithTargets[0].Target );
				allyTarget = Attackers[0];

				for (int i = 1; i < Attackers.Length; i++)
				{
					float dist = DistanceToUnit( AlliesWithTargets[i].Target );
					if (dist < shortest)
					{
						shortest = dist;
						allyTarget = AlliesWithTargets[i].Target;
					}
				}
			}

			return allyTarget;
		}

		public override void Update()
		{
			if (Target == null)
			{
				UpdateTarget();
			}

			base.Update(); // updates selected behavior
		}
	}
}