using LoyaltySystemApi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;

namespace LoyaltySystemApi.Infrastructure.EF.Configs.Conversions;

public sealed class PasswordEncryptingConversion : ValueConverter<string, string>
{
    public PasswordEncryptingConversion(Expression<Func<string, string>> convertToProviderExpression, Expression<Func<string, string>> convertFromProviderExpression, ConverterMappingHints? mappingHints = null) :
        base(to=>to.Encrypt(), from=>from.Decrypt(), mappingHints)
    {
      
    }
}
