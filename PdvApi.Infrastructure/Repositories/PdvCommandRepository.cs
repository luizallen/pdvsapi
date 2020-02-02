using Dapper;
using Newtonsoft.Json;
using Npgsql;
using PdvApi.Infrastructure.Dtos;
using PdvApi.Infrastructure.Repositories.Abstractions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace PdvApi.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class PdvCommandRepository : IPdvCommandRepository
    {
        private string ConnectionString { get; }

        public PdvCommandRepository(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void CreatePdv(PdvDto pdv)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Execute(CommandPdvInsert,
                    new
                    {
                        pdv.Id,
                        pdv.TradingName,
                        pdv.OwnerName,
                        pdv.Document,
                        MultiPolygon = JsonConvert.SerializeObject(pdv.CoverageArea),
                        AddresLng = pdv.Address.Coordinates.Longitude,
                        AddresLat = pdv.Address.Coordinates.Latitude
                    });
            }
        }

        public string CommandPdvInsert =>
            @"INSERT INTO Pdv (id, tradingName, ownerName, document, coverageArea, address)"
            + " VALUES(@Id, @TradingName, @OwnerName, @Document," +
            " ST_Force2D(ST_Multi(ST_GeomFromGeoJSON(@MultiPolygon)))," +
            " ST_SetSRID(ST_MakePoint(@AddresLng, @AddresLat), 4326))";
    }
}
