using UnityEngine;
using System.Collections.Generic;
using GameDataEditor;

#if GDE_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Game Data Editor")]
	[Tooltip("Sets a Float on a GDE Custom Item")]
	public class GDESetCustomFloat : GDEActionBase
	{   
		[UIHint(UIHint.FsmString)]
		[Tooltip("The field name of the bool inside the custom item.")]
		public FsmString CustomField;
		
		[UIHint(UIHint.FsmFloat)]
		public FsmFloat FloatValue;
		
		public override void Reset()
		{
			base.Reset();
			FloatValue = null;
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

					GDEDataManager.SetFloat(customKey+"_"+CustomField.Value, FloatValue.Value);
				}
			}
			catch(UnityException ex)
			{
				LogError(string.Format(GDMConstants.ErrorSettingCustomValue, "float", ItemName.Value, FieldName.Value, CustomField.Value));
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

