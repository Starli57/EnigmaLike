using System;

public class ReactValue<T>
{
    public Action<T> OnChanged;
    public T Val
    {
        get { return _val; }
        set
        {
            if (_val.Equals(value) == false)
            {
                _val = value;
                OnChanged?.Invoke(_val);
            }
        }
    }

    private T _val;
}