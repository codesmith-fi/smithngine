using System;

namespace Codesmith.SmithTest
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SmithGame game = new SmithGame())
            {
                game.Run();
            }
        }
    }
#endif
}

