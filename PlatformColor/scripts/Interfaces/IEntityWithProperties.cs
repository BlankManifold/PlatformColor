using System;
using Godot;
using GCs = Godot.Collections;


namespace PlatFormColor.scripts.Interfaces
{
    public interface IEntityWithProperties
    {
        private GCs::Dictionary<Globals.Property, Variant> PropertiesDict
        {
            get { throw new Exception("Cannot directly get entire PropertiesDict."); }
            set { throw new Exception("Cannot directly set entire PropertiesDict."); }
        }

        public Variant? GetProperty(Globals.Property property);
        public void AddProperty(Globals.Property property, Variant value);
        public void SetProperty(Globals.Property property, Variant value);
    }
}