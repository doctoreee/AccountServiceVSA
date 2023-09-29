namespace AccountService.API.Common;

public interface IHandler<T> where T: class
{
    IHandler<T> SetNext(IHandler<T> next);
    void Handle(T request);
}

public abstract class Handler<T>: IHandler<T> where T: class
{
    protected IHandler<T> Next { get; set; }

    public virtual void Handle(T request)
    {
        Next?.Handle(request);
    }

    public IHandler<T> SetNext(IHandler<T> next)
    {
        Next = next;

        return Next;
    }
}