using System.Security.Claims;
using Domain.Enums;
using Grpc.Core;

namespace GrpcInterface.Helpers;

public static class CheckRoleHelper
{
    public static void Check(HttpContext context, EmployeeType role)
    {
        if (context?.Items[ClaimTypes.Role]?.ToString() != role.ToString())
        {
            throw new RpcException(new Status(StatusCode.PermissionDenied, "You have no access to this resource"));
        }
    }
}
