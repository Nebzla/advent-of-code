
namespace _2024.src.Interfaces
{
    public interface IDeepCopyable<T>
    {
        T DeepCopy();
    }

    public interface IShallowCopyable<T>
    {
        T ShallowCopy();
    }

    public interface ICopyable<T>
    {
        T DeepCopy();
        T ShallowCopy();
    }
}