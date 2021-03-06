﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChronoZoom.Backend.Data.Interfaces
{
    public interface IContentItemDao
    {
        IEnumerable<Backend.Entities.ContentItem> FindAll(int parentID);
    }
}