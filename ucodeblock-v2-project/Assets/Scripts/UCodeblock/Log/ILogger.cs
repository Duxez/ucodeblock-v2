namespace UCodeblock.Log
{
    internal interface ILogger
    {
        void Write(object obj);
        void WriteLine(object obj);
    }
}
