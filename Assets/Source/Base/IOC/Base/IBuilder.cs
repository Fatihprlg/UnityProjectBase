using System.Collections.Generic;
using UnityEngine;

interface IBuilder
{
    public void Build(Container container);
    public void MapClasses();
}