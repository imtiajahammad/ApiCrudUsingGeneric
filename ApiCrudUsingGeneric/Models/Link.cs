﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Models
{
    [Keyless]
    public class Link
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }
        public Link()
        {
        }
        public Link(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
    public class LinkResourceBase
    {
        public LinkResourceBase()
        {
        }
        public List<Link> Links { get; set; } = new List<Link>();
    }
    public class LinkCollectionWrapper : LinkResourceBase
    {
        public Object Value { get; set; } = new Object();

        public LinkCollectionWrapper()
        {
        }
        public LinkCollectionWrapper(Object param)
        {
            Value = param;
        }
    }
}
