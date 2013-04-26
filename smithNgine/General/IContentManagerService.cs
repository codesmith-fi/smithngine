/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
namespace Codesmith.SmithNgine.General
{
    using Microsoft.Xna.Framework.Content;

    public interface IContentManagerService
    {
        ContentManager Content
        {
            get;
        }
    }
}
