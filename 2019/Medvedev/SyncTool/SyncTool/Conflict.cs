﻿using SyncTool.Wrappers;

namespace SyncTool
{
    public class Conflict
    {
        public IFileSystemElementInfoWrapper Source { get; }
        public IFileSystemElementInfoWrapper Destination { get; }

        public Conflict(IFileSystemElementInfoWrapper first, IFileSystemElementInfoWrapper second)
        {
            if (first is null)
            {
                Source = null;
                Destination = second;
            }
            else
            {
                var comparision = first.CompareTo(second);

                if (comparision < 0)
                {
                    Source = first;
                    Destination = second;
                }

                if (comparision > 0)
                {
                    Source = second;
                    Destination = first;
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (!(obj is Conflict))
                return false;

            var other = (Conflict) obj;

            return Source.Equals(other.Source) && Destination.Equals(other.Destination);
        }

        public override int GetHashCode()
        {
            if (Source is null)
                return Destination.GetHashCode();
            if (Destination is null)
                return Source.GetHashCode();
            return Source.GetHashCode() ^ Destination.GetHashCode();
        }
    }
}