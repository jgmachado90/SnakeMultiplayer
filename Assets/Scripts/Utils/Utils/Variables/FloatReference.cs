using System;
[Serializable]
public class FloatReference
{
    public bool useConstant = true;
    public float ConstantValue;
    public FloatVariable Variable;

    public float Value
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
