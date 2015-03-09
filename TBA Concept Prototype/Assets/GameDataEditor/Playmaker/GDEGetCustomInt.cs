using UnityEngine;
using System.Collections.Generic;
using GameDataEditor;


#if GDE_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Game Data Editor")]
    [Tooltip("Gets a Int from a GDE Custom Item")]
    public class GDEGetCustomInt : GDEActionBase
    {   
        [UIHint(UIHint.FsmString)]
        [Tooltip("The field name of the int inside the custom item.")]
        public FsmString CustomField;
        
        [UIHint(UIHint.FsmInt)]
        public FsmInt StoreResult;
        
        public override void Reset()
        {
            base.Reset();
            StoreResult = null;
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

                    Dictionary<string, object> customData;
                    GDEDataManager.Get(customKey, out customData);
                    
                    int val;
                    customData.TryGetInt(CustomField.Value, out val);
                    StoreResult.Value = val;

					StoreResult.Value = GDEDataManager.GetInt(customKey+"_"+CustomField.Value, StoreResult.Value);
                }
                else
                {
                    LogError(string.Format(GDMConstants.ErrorLoadingValue, "int", ItemName.Value, FieldName.Value));
                }
            }
            catch(UnityException ex)
            {
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
