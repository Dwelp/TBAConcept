using UnityEngine;
using System.Collections.Generic;
using GameDataEditor;


#if GDE_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Game Data Editor")]
    [Tooltip("Gets a Vector3 from a GDE Custom Item")]
    public class GDEGetCustomVector3 : GDEActionBase
    {   
        [UIHint(UIHint.FsmString)]
        [Tooltip("The field name of the Vector3 inside the custom item.")]
        public FsmString CustomField;
        
        [UIHint(UIHint.FsmVector3)]
        public FsmVector3 StoreResult;
        
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
                    
                    Vector3 val;
                    customData.TryGetVector3(CustomField.Value, out val);
                    StoreResult.Value = val;

					StoreResult.Value = GDEDataManager.GetVector3(customKey+"_"+CustomField.Value, StoreResult.Value);
                }
                else
                {
                    LogError(string.Format(GDMConstants.ErrorLoadingValue, "Vector3", ItemName.Value, FieldName.Value));
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
