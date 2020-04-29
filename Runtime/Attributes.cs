using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robotfists
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ServiceAttribute : PropertyAttribute
    {
        public bool Optional { get; set; } = false;
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ComponentAttribute : PropertyAttribute
    {
        public bool Optional { get; set; } = false;

        internal string Path { get; set; }

        public ComponentAttribute(string path = null)
        {
            Path = path;
        }
    }
}
