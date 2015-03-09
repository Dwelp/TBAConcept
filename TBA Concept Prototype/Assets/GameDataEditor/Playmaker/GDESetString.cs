using UnityEngine;
using System.Collections.Generic;
using GameDataEditor;

#if GDE_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Game Data Editor")]
	[Tooltip("Sets a String on a GDE Item")]
	public class GDESetString : GDEActionBase
	{   
		[UIHint(UIHint.FsmString)]
		public FsmString StringValue;
		
		public override void Reset()
		{
			base.Reset();
			StringValue = null;
		}
		
		public override void OnEnter()
		{
			try
			{
				GDEDataManager.SetString(FieldKey, StringValue.Value);
			}
			catch(UnityException ex)
			{
				LogError(string.Format(GDMConstants.ErrorSettingValue, "string", ItemName.Value, FieldName.Value));
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

