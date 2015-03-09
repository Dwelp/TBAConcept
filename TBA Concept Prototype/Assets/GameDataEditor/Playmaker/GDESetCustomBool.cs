using UnityEngine;
using System.Collections.Generic;
using GameDataEditor;

#if GDE_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Game Data Editor")]
	[Tooltip("Sets a Bool on a GDE Custom Item")]
	public class GDESetCustomBool : GDEActionBase
	{   
		[UIHint(UIHint.FsmString)]
		[Tooltip("The field name of the bool inside the custom item.")]
		public FsmString CustomField;

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
				Dictionary<string, object> data;
				if (GDEDataManager.Get(ItemName.Value, out data))
				{
					string customKey;
					data.TryGetString(FieldName.Value, out customKey);
					customKey = GDEDataManager.GetString(ItemName.Value+"_"+FieldName.Value, customKey);

					GDEDataManager.SetBool(customKey+"_"+CustomField.Value, BoolValue.Value);
				}
			}
			catch(UnityException ex)
			{
				LogError(string.Format(GDMConstants.ErrorSettingCustomValue, "bool", ItemName.Value, FieldName.Value, CustomField.Value));
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

