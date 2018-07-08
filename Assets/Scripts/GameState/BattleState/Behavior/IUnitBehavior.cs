﻿/* 
 * Author: Gregory Maynard <CinderScript@gmail.com>
 * Copyright (C) - All Rights Reserved
 */

using UnityEngine;

namespace AIRogue.GameState.Battle.Behavior
{

	abstract class IUnitBehavior {

		protected readonly UnitActionController actionController;
		public IUnitBehavior(UnitActionController actionController)
		{
			this.actionController = actionController;
		}

        public abstract void Perform();
    }

	class InputListenerBehavior : IUnitBehavior
	{
		public InputListenerBehavior(UnitActionController actionController) : base( actionController ) { }

		public override void Perform()
		{
			/// GET PLAYER CONTROLLER INPUT
			int thrustInput = (int)Input.GetAxisRaw( "Vertical" );
			float rotationInput = Input.GetAxis( "Horizontal" );

			bool primaryAttackInput = Input.GetButton( "Fire1" );
			bool secondaryAttackInput = Input.GetButton( "Fire1" );

			/// APPLY INPUT TO UnitActionController
			if (thrustInput > 0)                                // If thrusting
			{
				actionController.ForwardThrust();
			}
			else if (thrustInput < 0 && rotationInput == 0)     // If ReversTurning and not rotating
			{
				actionController.ReverseTurn();
			}

			if (rotationInput != 0)                             // If rotating
			{
				actionController.Rotate( rotationInput );
			}

			if (primaryAttackInput)
			{
				actionController.PrimaryAttack();
			}

			if (secondaryAttackInput)
			{
				actionController.SecondaryAttack();

			}
		}
	}
}