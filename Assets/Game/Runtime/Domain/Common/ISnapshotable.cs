namespace Game.Runtime.Domain.Common
{
    public interface ISnapshotable<T>
    {
        public T GetSnapshot();
        public void Update(T snapshot);
    }
}
