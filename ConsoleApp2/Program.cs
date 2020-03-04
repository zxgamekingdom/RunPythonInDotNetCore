using System;
using System.Threading.Tasks;
using Python.Included;
using Python.Runtime;

namespace ConsoleApp2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Installer.SetupPython();
            using (Py.GIL())
            {
                string a = "def funcname(a):"
                           + Environment.NewLine
                           + "    i = 12"
                           + Environment.NewLine
                           + "    a = i+25"
                           + Environment.NewLine
                           + "    print(i)"
                           + Environment.NewLine
                           + "    print(a)"
                           + Environment.NewLine
                           + "    return a";
                PyScope pyScope = Py.CreateScope();
                PyObject pyObject = PythonEngine.ModuleFromString("a", a);
                pyScope.ImportAll(pyObject);
                PyObject eval = pyScope.Eval("funcname(15)");
                Console.Out.WriteLine("eval = {0}", eval);
                eval.GetPythonType().WriteLine();
                var @as = eval.As<int>();
                Console.Out.WriteLine("@as = {0}", @as);
            }

            Console.ReadKey();
        }
    }

    public static class ConsoleExtensions
    {
        public static void WriteLine(this object o)
        {
            Console.WriteLine(o);
        }
    }
}