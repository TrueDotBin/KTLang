using KTLang;

namespace KTLang.Cli
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                if (args.Length > 0)
                {
                    var first = args[0];
                    Runner.RunScript(first);
                }
                else
                    Runner.StartRepl();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                if (!string.IsNullOrEmpty(ex.StackTrace))
                    Console.WriteLine(ex.StackTrace);

                return 1;
            }
        }
    }
}