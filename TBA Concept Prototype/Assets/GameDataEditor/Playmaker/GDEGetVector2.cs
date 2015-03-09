using UnityEngine;
using System.Collections.Generic;
using GameDataEditor;


#if GDE_PLAYMAKER_SUPPORT

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Game Data Editor")]
    [Tooltip("Gets a Vector2 from a GDE Item")]
    public class GDEGetVector2 : GDEActionBase
    {   
        [UIHint(UIHint.FsmVector2)]
        public FsmVector2 StoreResult;
        
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
                    Vector2 val;
                    data.TryGetVector2(FieldName.Value, out val);
                    StoreResult.Value = val;

					StoreResult.Value = GDEDataManager.GetVector2(FieldKey, StoreResult.Value);
                }
                else
                {
                    LogError(string.Format(GDMConstants.ErrorLoadingValue, "Vector2", ItemName.Value, FieldName.Value));
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


