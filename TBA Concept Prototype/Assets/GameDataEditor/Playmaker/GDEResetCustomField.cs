using UnityEngine;
using System.Collections.Generic;
using GameDataEditor;

#if GDE_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Game Data Editor")]
	[Tooltip("Resets a Field to the original value in a GDE Custom Item")]
	public class GDEResetCustomField : GDEActionBase
	{   
		[UIHint(UIHint.FsmString)]
		[Tooltip("The name of the field inside the custom item.")]
		public FsmString CustomField;

		public override void OnEnter()
		{
			try
			{
				Dictionary<string, object> data;
				if (GDEDataManager.Get(ItemName.Value, out data))
				{
					string customKey;
					data.TryGetString(FieldName.Value, out customKey);
					customKey = GDEDataManager.GetString(ItemName.Value+"_"+FieldName.Value, customKey);

					GDEDataManager.ResetToDefault(customKey, CustomField.Value);
				}
			}
			catch(UnityException ex)
			{
				LogError(string.Format(GDMConstants.ErrorResettingCustomValue, ItemName.Value, FieldName.Value, CustomField.Value));
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

