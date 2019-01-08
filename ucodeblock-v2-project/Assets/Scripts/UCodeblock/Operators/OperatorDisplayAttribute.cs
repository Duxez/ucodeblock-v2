using System;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
sealed class OperatorDisplayAttribute : Attribute
{
    public string Display => _display;
    private string _display;
    
    public OperatorDisplayAttribute(string display)
    {
        _display = display;
    }
}