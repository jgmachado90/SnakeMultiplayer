using System;
[Serializable]
public class IntReference
{
    public bool useConstant = true;
    public int ConstantValue;
    public IntVariable Variable;

    public int Value
    {
        get
        {
            return useConstant ? ConstantValue : Variable.Value;
        }
        set
        {
            if (useConstant)
                ConstantValue = value;
            else
                Variable.Value = value;
        }
    }
}
