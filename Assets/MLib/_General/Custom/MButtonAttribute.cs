using System;
using UnityEngine;

namespace MLib
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class MButtonAttribute : Attribute
    {
        public string ButtonLabel { get; }

        public MButtonAttribute(string label = null)
        {
            ButtonLabel = label;
        }
    }
}
