using System.Security.Cryptography;

namespace Application.Utils.Params;

public class PasswordParams
{
    public string UnhashedPassword { get; set; } = string.Empty;
    public int KeySize { get; set; } = Consts.PASSWORD_DEFAULT_KEY_SIZE;

    public int Iterations { get; set; } = Consts.PASSWORD_DEFAULT_ITERATIONS;

    public HashAlgorithmName HashAlgorithm { get; set; } = HashAlgorithmName.SHA512;
}
