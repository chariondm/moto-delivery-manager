using System.Data;

using Dapper;

namespace Adapters.Outbounds.PostgresDbAdapter.Infrastructure.TypeHandlers;

/// <summary>
/// Represents the type handler for the <see cref="DateOnly"/> type.
/// </summary>
/// <remarks>
/// This class is used to handle the conversion between the <see cref="DateOnly"/> type and the <see cref="DateTime"/> type
/// when reading and writing data from and to the database.
/// </remarks>
/// <seealso cref="SqlMapper.TypeHandler{DateOnly}" />
public class DapperSqlDateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly date)
        => parameter.Value = date.ToDateTime(new TimeOnly(0, 0));
    
    public override DateOnly Parse(object value)
        => DateOnly.FromDateTime((DateTime)value);
}