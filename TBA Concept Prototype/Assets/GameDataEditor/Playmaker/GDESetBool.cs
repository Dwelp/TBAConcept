using UnityEngine;
using System.Collections.Generic;
using GameDataEditor;

#if GDE_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Game Data Editor")]
	[Tooltip("Sets a Bool on a GDE Item")]
	public class GDESetBool : GDEActionBase
	{   
		[UIHint(UIHint.FsmBool)]
		public FsmBool BoolValue;
		
		public override void Reset()
		{
			base.Reset();
			BoolValue = null;
		}
		
		public override void OnEnter()
		{
			try
			{
				GDEDataManager.SetBool(FieldKey, BoolValue.Value);
			}
			catch(UnityException ex)
			{
				LogError(string.Format(GDMConstants.ErrorSettingValue, "bool", ItemName.Value, FieldName.Value));
				LogError(ex.ToString());
			}
			finally
			{
				Finish();
			}
		}
	}
}

#endif

