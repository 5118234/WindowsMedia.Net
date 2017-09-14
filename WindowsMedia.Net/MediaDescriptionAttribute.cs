using System;

namespace MediaPortal.DShowNET
{
  [AttributeUsage(AttributeTargets.All)]
  public class MediaDescriptionAttribute : Attribute
  {
    public MediaDescriptionAttribute(string id, string name)
    {
      Id = new Guid(id);
      Name = name;
    }

    public string Name { get; set; }

    public Guid Id { get; set; }
  }
}