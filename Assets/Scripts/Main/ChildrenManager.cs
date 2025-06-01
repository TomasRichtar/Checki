using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;

public class ChildrenManager : SingletonMonoBehaviour<ChildrenManager>
{
    public Children CreateChildren(string name)
    {
        Children child = new Children();
        child.Name = name;

        return child;
    }
}
