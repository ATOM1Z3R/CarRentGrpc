using Grpc.Core;
using Grpc.Core.Interceptors;

namespace GrpcInterface.Interceptors;

public class ErrorHandlingInterceptor : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation
    )
    {
        try
        {
            return await continuation(request, context);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new RpcException(new Status(StatusCode.PermissionDenied, ex.Message));
        }
        catch (RpcException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }
}