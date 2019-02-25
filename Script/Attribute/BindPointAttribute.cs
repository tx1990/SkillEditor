using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillEditor
{
    public class BindPointAttribute : PropertyAttribute
    {
        public string[] Points { get; }

        public BindPointAttribute(string[] points)
        {
            Points = points;
        }
    }
}
