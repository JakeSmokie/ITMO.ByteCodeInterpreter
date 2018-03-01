namespace YIPP
{
    class Program
    {
        static void Main(string[] args)
        {
            PTPTBHandler.ReadFile(args);
            Interpreter.Run();
        }
    }
}
