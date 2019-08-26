namespace GeneralizeSynchLibrary
{
    public static class SynchronizerFactory
    {
        public static ISynchronizer Create(bool noRemove)
        {
            switch (noRemove)
            {
                case true:
                    return new NoRemoveSynchronizer();
                case false:
                    return new RemoveSynchronizer();
            }
            return new RemoveSynchronizer();
        }
    }
}
