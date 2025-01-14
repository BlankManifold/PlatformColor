using Godot;


namespace PlatFormColor.scripts.Interfaces
{
    public interface IEntityWithProperties
    {
        public delegate void NotifySetProperty(Globals.Property property, Variant value);
        public event NotifySetProperty SettingProperty;
        public Variant? GetProperty(Globals.Property property);
        public void AddProperty(Globals.Property property, Variant value, bool changeable = true);
        public void SetProperty(Globals.Property property, Variant value);
        public bool IsPropertyChangeable(Globals.Property property);
    }
}