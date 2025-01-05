using Godot;
using GCs = Godot.Collections;

namespace PlatFormColor.scripts.Resources
{
    public partial class EntityWithPropertiesRes : Resource
    {
        public GCs::Dictionary<Globals.Property, Variant> PropertiesDict = new();

        public EntityWithPropertiesRes(GCs::Dictionary<Globals.Property, Variant> dict = null)
        {
            PropertiesDict = (dict == null) ? new() : dict;
        }
        public Variant? GetProperty(Globals.Property property)
        {
            if (PropertiesDict.TryGetValue(property, out Variant value))
                return value;

            return null;
        }
        public virtual void AddProperty(Globals.Property property, Variant value)
        {
            if (PropertiesDict.ContainsKey(property))
                return;

            PropertiesDict.Add(property, value);
        }
        public virtual void SetProperty(Globals.Property property, Variant value)
        {
            if (!PropertiesDict.ContainsKey(property))
                return;

            PropertiesDict[property] = value;
        }

    }
}