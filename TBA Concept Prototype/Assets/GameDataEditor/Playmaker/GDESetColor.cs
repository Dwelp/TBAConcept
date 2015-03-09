using UnityEngine;
using System.Collections.Generic;
using GameDataEditor;

#if GDE_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Game Data Editor")]
	[Tooltip("Sets a Color on a GDE Item")]
	public class GDESetColor : GDEActionBase
	{   
		[UIHint(UIHint.FsmColor)]
		public FsmColor ColorValue;
		
		public override void Reset()
		{
			base.Reset();
			ColorValue = null;
		}
		
		public override void OnEnter()
		{
			try
			{
				GDEDataManager.SetColor(FieldKey, ColorValue.Value);
			}
			catch(UnityException ex)
			{
				LogError(string.Format(GDMConstants.ErrorSettingValue, "Color", ItemName.Value, FieldName.Value));
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

