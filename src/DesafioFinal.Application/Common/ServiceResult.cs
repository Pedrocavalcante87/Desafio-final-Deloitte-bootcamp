namespace DesafioFinal.Application.Common;

public class ServiceResult<T>
{
    public bool Ok { get; }
    public string? Error { get; }
    public T? Data { get; }

    private ServiceResult(bool ok, T? data, string? error)
    {
        Ok = ok;
        Data = data;
        Error = error;
    }

    public static ServiceResult<T> Success(T data) => new(true, data, null);
    public static ServiceResult<T> Fail(string error) => new(false, default, error);
}

public class ServiceResult
{
    public bool Ok { get; }
    public string? Error { get; }

    private ServiceResult(bool ok, string? error)
    {
        Ok = ok;
        Error = error;
    }

    public static ServiceResult Success() => new(true, null);
    public static ServiceResult Fail(string error) => new(false, error);
}
