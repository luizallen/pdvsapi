using BAMCIS.GeoJSON;
using Dapper;
using Newtonsoft.Json;
using Npgsql;
using PdvApi.Infrastructure.Dtos;
using PdvApi.Infrastructure.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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

        public PdvDto GetPdv(Guid pdvId)
        {
            var query = $"{DefaultQuery} where id = '{pdvId.ToString()}'"; ;

            return GetSinglePdv(query);
        }

        public PdvDto GetPdv(string document)
        {
            var query = $"{DefaultQuery} where document = '{document}'"; ;

            return GetSinglePdv(query);
        }

        public IList<PdvDto> GetPdvs()
        {
            var query = DefaultQuery;

            return GetListOfPdv(query);
        }

        public IList<PdvDto> GetInAreaPvs(string lng, string lat)
        {
            var query = $"{DefaultQuery}" +
                        $"where ST_Within(coverageArea, ST_SetSRID(ST_MakePoint({lng}, {lat}), 4326)) ";

            return GetListOfPdv(query);
        }

        private PdvDto GetSinglePdv(string query)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand(query, connection);

                var reader = command.ExecuteReader();

                if (!reader.HasRows)
                    return null;

                reader.Read();

                return GetPdvDto(reader);
            }
        }

        private List<PdvDto> GetListOfPdv(string query)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand(query, connection);

                var reader = command.ExecuteReader();

                var listOfPdv = new List<PdvDto>();

                while (reader.Read())
                {
                    listOfPdv.Add(GetPdvDto(reader));
                }

                return listOfPdv;
            }
        }

        private PdvDto GetPdvDto(NpgsqlDataReader reader)
        => new PdvDto(reader.GetString(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetString(3),
            JsonConvert.DeserializeObject<MultiPolygon>(reader.GetString(4)),
            JsonConvert.DeserializeObject<Point>(reader.GetString(5)));

        private string DefaultQuery =>
            @"select id, tradingName, ownerName, document, ST_AsGeoJSON(coverageArea) as coverageArea, ST_AsGeoJSON(address) as address from Pdv";

        private string CommandPdvInsert =>
            @"INSERT INTO Pdv (id, tradingName, ownerName, document, coverageArea, address)"
            + " VALUES(@Id, @TradingName, @OwnerName, @Document," +
            " ST_Force2D(ST_Multi(ST_GeomFromGeoJSON(@MultiPolygon)))," +
            " ST_SetSRID(ST_MakePoint(@AddresLng, @AddresLat), 4326))";
    }
}
