using Kf.Essentials.Diagnostics.Debugging;
using System;
using System.Diagnostics;

namespace Kf.Essentials.CleanArchitecture
{
    /// <summary>
    /// Defines an entity.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplayString,nq}")]
    public abstract class Entity<TId>
        : IDebuggerDisplayString,
          IEquatable<Entity<TId>>
    {
        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TId> a, Entity<TId> b)
            => !(a == b);

        protected Entity(TId id)
            => Id = id;

        public TId Id { get; }

        public override bool Equals(object @object)
        {
            if (@object == null) return false;
            if (@object is Entity<TId> entity) return Equals(entity);
            return false;
        }

        public bool Equals(Entity<TId> other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            return CompareId(Id, other.Id);
        }

        /// <summary>
        /// Compares two <typeparamref name="TId"/>'s and returns true when they match, false when not.
        /// Override when using custom id's, or when applying special logic (e.g. when "0" doesn't matter).
        /// </summary>        
        protected virtual bool CompareId(TId a, TId b)
            => a.Equals(b);

        public override int GetHashCode()
            => $"{GetType()}{Id}".GetHashCode();

        public virtual string DebuggerDisplayString
            => this.CreateDebugString(x => x.Id);

        public override string ToString()
            => DebuggerDisplayString;
    }
}
