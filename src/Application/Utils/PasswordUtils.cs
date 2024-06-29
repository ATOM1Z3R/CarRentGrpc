using System.Security.Cryptography;
using System.Text;
using Application.Utils.Params;

namespace Application.Utils;

public static class PasswordUtils
{
    public static Tuple<string, byte[]> Hash(PasswordParams passwordParams)
    {
        var salt = RandomNumberGenerator.GetBytes(passwordParams.KeySize);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(passwordParams.UnhashedPassword),
            salt,
            passwordParams.Iterations,
            passwordParams.HashAlgorithm,
            passwordParams.KeySize
        );

        return new Tuple<string, byte[]>(Convert.ToHexString(hash), salt);
    }

    public static bool Verify(PasswordParams passwordParams, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
            passwordParams.UnhashedPassword,
            salt,
            passwordParams.Iterations,
            passwordParams.HashAlgorithm,
            passwordParams.KeySize
        );

        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
}
