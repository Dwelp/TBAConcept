using UnityEngine;
using System.Collections.Generic;
using GameDataEditor;

#if GDE_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Game Data Editor")]
	[Tooltip("Sets a String on a GDE Custom Item")]
	public class GDESetCustomString : GDEActionBase
	{   
		[UIHint(UIHint.FsmString)]
		[Tooltip("The field name of the bool inside the custom item.")]
		public FsmString CustomField;
		
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
				Dictionary<string, object> data;
				if (GDEDataManager.Get(ItemName.Value, out data))
				{
					string customKey;
					data.TryGetString(FieldName.Value, out customKey);
					customKey = GDEDataManager.GetString(ItemName.Value+"_"+FieldName.Value, customKey);

					GDEDataManager.SetString(customKey+"_"+CustomField.Value, StringValue.Value);
				}
			}
			catch(UnityException ex)
			{
				LogError(string.Format(GDMConstants.ErrorSettingCustomValue, "string", ItemName.Value, FieldName.Value, CustomField.Value));
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

