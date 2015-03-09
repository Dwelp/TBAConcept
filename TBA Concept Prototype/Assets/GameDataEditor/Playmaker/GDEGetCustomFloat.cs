using UnityEngine;
using System.Collections.Generic;
using GameDataEditor;


#if GDE_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Game Data Editor")]
    [Tooltip("Gets a Float from a GDE Custom Item")]
    public class GDEGetCustomFloat : GDEActionBase
    {   
        [UIHint(UIHint.FsmString)]
        [Tooltip("The field name of the float inside the custom item.")]
        public FsmString CustomField;
        
        [UIHint(UIHint.FsmFloat)]
        public FsmFloat StoreResult;
        
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
                    
                    float val;
                    customData.TryGetFloat(CustomField.Value, out val);
                    StoreResult.Value = val;

					StoreResult.Value = GDEDataManager.GetFloat(customKey+"_"+CustomField.Value, StoreResult.Value);
                }
                else
                {
                    LogError(string.Format(GDMConstants.ErrorLoadingValue, "float", ItemName.Value, FieldName.Value));
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
