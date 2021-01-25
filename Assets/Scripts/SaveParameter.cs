using System.Collections;
using UnityEngine;

[System.Serializable]
public class SaveParameter
{
    private static SaveParameter _current;
    public static SaveParameter current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveParameter();
            }
            return _current;
        }
        set
        {
            if (value != null)
            {
                _current = value;
            }
        }
    }

    public bool createdByManager;
    public int InputMode;
    public bool canUseInputs;

    public void ResetValueToDefault()
    {
        InputMode = 0;
        canUseInputs = true;
    }
}
