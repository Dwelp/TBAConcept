﻿using UnityEngine;
using System.Collections.Generic;
using GameDataEditor;

#if GDE_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Game Data Editor")]
    public abstract class GDEActionBase : FsmStateAction
    {
        [RequiredField]
        [UIHint(UIHint.FsmString)]
        [Tooltip("Item Name")]
        public FsmString ItemName;
        
        [RequiredField]
        [UIHint(UIHint.FsmString)]
        [Tooltip("Field Name")]
        public FsmString FieldName;

        public override void Reset()
        {
            ItemName = null;
            FieldName = null;
        }
        
        public abstract override void OnEnter();

		protected string FieldKey
		{
			get {
				return ItemName.Value+"_"+FieldName.Value;
			}

			private set {}
		}
    }
}

#endif
