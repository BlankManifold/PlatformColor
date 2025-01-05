using System;
using Godot;
using GCs = Godot.Collections;


namespace PlatFormColor.scripts.Interfaces
{
    public interface IEntityWithProperties
    {
        public Variant? GetProperty(Globals.Property property);
        public void AddProperty(Globals.Property property, Variant value);
        public void SetProperty(Globals.Property property, Variant value);
    }
}