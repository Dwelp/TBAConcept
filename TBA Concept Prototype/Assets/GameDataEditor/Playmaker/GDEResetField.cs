using UnityEngine;
using System.Collections.Generic;
using GameDataEditor;

#if GDE_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Game Data Editor")]
	[Tooltip("Resets a Field to the original value on a GDE Item")]
	public class GDEResetField : GDEActionBase
	{   
		public override void OnEnter()
		{
			try
			{
				GDEDataManager.ResetToDefault(ItemName.Value, FieldName.Value);
			}
			catch(UnityException ex)
			{
				LogError(string.Format(GDMConstants.ErrorResettingValue, ItemName.Value, FieldName.Value));
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

