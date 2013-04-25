using System;

namespace Codesmith.SmithShooter
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SmithShooter game = new SmithShooter())
            {
                game.Run();
            }
        }
    }
#endif
}

