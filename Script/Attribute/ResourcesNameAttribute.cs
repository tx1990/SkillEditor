using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillEditor
{
    public class ResourcesNameAttribute : PropertyAttribute
    {
        public Type ResourceType { get; }

        public ResourcesNameAttribute(Type type)
        {
            ResourceType = type;
        }
    }
}
