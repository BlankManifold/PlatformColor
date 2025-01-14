using Godot;
using SCs = System.Collections.Generic;


namespace PlatFormColor.scripts.Resources
{
    public partial class EntityWithPropertiesRes : Resource
    {
        public SCs::Dictionary<Globals.Property, (Variant Value, bool Changeable)> PropertiesDict = new();

        public EntityWithPropertiesRes(SCs::Dictionary<Globals.Property, (Variant Value, bool Changeable)> dict = null)
        {
            PropertiesDict = dict ?? new();
        }
        public Variant? GetProperty(Globals.Property property)
        {
            if (PropertiesDict.TryGetValue(property, out var value))
                return value.Value;

            return null;
        }
        public virtual void AddProperty(Globals.Property property, Variant value, bool changeable = true)
        {
            if (PropertiesDict.ContainsKey(property))
                return;

            PropertiesDict.Add(property, (value, changeable));
        }
        public virtual void SetProperty(Globals.Property property, Variant value)
        {
            if (!PropertiesDict.ContainsKey(property))
                return;

            PropertiesDict[property] = (value, PropertiesDict[property].Changeable);
        }

    }
}