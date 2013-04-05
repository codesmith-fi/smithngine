using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithNgine.General
{
    public class FrameworkContentService : IContentManagerService
    {
        public ContentManager Content
        {
            get;
            private set;
        }

        public FrameworkContentService(ContentManager content)
        {
            Content = content;
        }
    }
}
