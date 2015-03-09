using UnityEngine;
using System.Collections.Generic;
using GameDataEditor;

#if GDE_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Game Data Editor")]
	[Tooltip("Sets a Vector3 on a GDE Custom Item")]
	public class GDESetCustomVector3 : GDEActionBase
	{   
		[UIHint(UIHint.FsmString)]
		[Tooltip("The field name of the bool inside the custom item.")]
		public FsmString CustomField;
		
		[UIHint(UIHint.FsmVector3)]
		public FsmVector3 Vector3Value;
		
		public override void Reset()
		{
			base.Reset();
			Vector3Value = null;
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

					GDEDataManager.SetVector3(customKey+"_"+CustomField.Value, Vector3Value.Value);
				}
			}
			catch(UnityException ex)
			{
				LogError(string.Format(GDMConstants.ErrorSettingCustomValue, "Vector3", ItemName.Value, FieldName.Value, CustomField.Value));
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

