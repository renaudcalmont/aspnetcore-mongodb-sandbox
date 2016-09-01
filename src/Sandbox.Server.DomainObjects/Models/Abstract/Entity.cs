using System;
using System.Linq;
using System.Reflection;
using Sandbox.Server.DomainObjects.Interfaces.Models.Abstract;

namespace Sandbox.Server.DomainObjects.Models.Abstract
{
  public abstract class Entity : Model, IEntity
  {
    private static readonly Random RandomGenerator;

    private Guid? _id;

    static Entity()
    {
      RandomGenerator = new Random();
    }

    public Guid Id
    {
      get
      {
        if (_id != null) return _id.Value;

        var date = BitConverter.GetBytes(DateTime.Now.ToBinary());
        Array.Reverse(date);

        var random = new byte[8];
        RandomGenerator.NextBytes(random);

        var guid = new byte[16];
        Buffer.BlockCopy(date, 0, guid, 0, 8);
        Buffer.BlockCopy(random, 0, guid, 8, 8);

        _id = new Guid(guid);

        return _id.Value;
      }

      set
      {
        if (_id == null)
        {
          _id = value;
        }
      }
    }

    public Guid Revision { get; set; }

    public DateTime GetCreationDate()
    {
      return Decode(Id);
    }

    public DateTime GetModificationDate()
    {
      return Decode(Revision);
    }

    public override bool Equals(object obj)
    {
      if (obj == null) return false;
      if (GetType() != obj.GetType()) return false;

      foreach (var propertyInfo in GetType().GetRuntimeProperties().Where(x => x.Name != "Revision"))
      {
        var mine = propertyInfo.GetValue(this, null);
        var theirs = propertyInfo.GetValue(obj, null);
        if (mine != null)
        {
          //TODO: copy algo
        }
        else if (theirs != null)
        {
          return false;
        }
      }

      return true;
    }

    public override int GetHashCode()
    {
      return Id.ToByteArray().Concat(Revision.ToByteArray()).GetHashCode();
    }

    private static DateTime Decode(Guid item)
    {
      var date = new byte[8];
      Buffer.BlockCopy(item.ToByteArray(), 0, date, 0, 8);
      Array.Reverse(date);

      try
      {
        return DateTime.FromBinary(BitConverter.ToInt64(date, 0));
      }
      catch
      {
        // ignored
      }

      return DateTime.MinValue;
    }
  }
}