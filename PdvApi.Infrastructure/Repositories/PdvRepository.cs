using Dapper;
using Dapper.Contrib.Extensions;
using Npgsql;
using PdvApi.Infrastructure.Dtos;
using PdvApi.Infrastructure.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PdvApi.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class PdvRepository : IPdvRepository
    {
        public string ConnectionString { get; }

        public PdvRepository(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void CreatePdv(PdvDto pdv)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Insert(pdv);
            }
        }

        public PdvDto GetPdv(Guid pdvId)
        {
            var query = GetPdvIdQuery;

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return connection.Query(query, new { Id = pdvId.ToString() }).FirstOrDefault();
            }
        }

        public IList<PdvDto> GetPdv(string lng, string lat)
        {
            //var query = GetCardIdQuery;

            //using (var connection = new NpgsqlConnection(ConnectionString))
            //{
            //    return connection.Query(query, new { Proxy = proxy }).FirstOrDefault();
            //}

            return new List<PdvDto>();
        }

        private string GetPdvIdQuery => @"select * from pdv where id = @Id";
    }
}
