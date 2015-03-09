using UnityEngine;
using System.Collections.Generic;
using GameDataEditor;

#if GDE_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Game Data Editor")]
	[Tooltip("Sets a Vector2 on a GDE Item")]
	public class GDESetVector2 : GDEActionBase
	{   
		[UIHint(UIHint.FsmVector2)]
		public FsmVector2 Vector2Value;
		
		public override void Reset()
		{
			base.Reset();
			Vector2Value = null;
		}
		
		public override void OnEnter()
		{
			try
			{
				GDEDataManager.SetVector2(FieldKey, Vector2Value.Value);
			}
			catch(UnityException ex)
			{
				LogError(string.Format(GDMConstants.ErrorSettingValue, "Vector2", ItemName.Value, FieldName.Value));
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

