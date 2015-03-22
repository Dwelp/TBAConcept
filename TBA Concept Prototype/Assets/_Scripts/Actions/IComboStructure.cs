using UnityEngine;
using System.Collections;

public interface IComboStructure {

    bool CanCombo();
    bool AllowCombo();
    int ComboStepRequired();
    bool CanBeUsed();
}
