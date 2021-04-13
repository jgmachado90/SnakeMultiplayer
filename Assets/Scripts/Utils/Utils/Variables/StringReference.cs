using System;
[Serializable]
public class StringReference
{
    public bool useConstant = true;
    public string ConstantValue;
    public StringVariable Variable;

    public string Value
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
