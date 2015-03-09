using UnityEngine;
using System.Collections.Generic;
using GameDataEditor;

#if GDE_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Game Data Editor")]
	[Tooltip("Sets an Int on a GDE Custom Item")]
	public class GDESetCustomInt : GDEActionBase
	{   
		[UIHint(UIHint.FsmString)]
		[Tooltip("The field name of the bool inside the custom item.")]
		public FsmString CustomField;
		
		[UIHint(UIHint.FsmInt)]
		public FsmInt IntValue;
		
		public override void Reset()
		{
			base.Reset();
			IntValue = null;
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

					GDEDataManager.SetInt(customKey+"_"+CustomField.Value, IntValue.Value);
				}
			}
			catch(UnityException ex)
			{
				LogError(string.Format(GDMConstants.ErrorSettingCustomValue, "int", ItemName.Value, FieldName.Value, CustomField.Value));
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

