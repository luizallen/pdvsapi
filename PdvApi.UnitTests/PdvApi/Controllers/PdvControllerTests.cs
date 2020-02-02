using AutoFixture.Idioms;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using PdvApi.Controllers;
using PdvApi.Infrastructure.Dtos;
using PdvApi.Models;
using PdvApi.UnitTests.AutoFixture;
using System;
using System.Collections.Generic;
using Xunit;

namespace PdvApi.UnitTests.PdvApi.Controllers
{
    public class PdvControllerTests
    {
        [Theory, AutoNSubstituteData]
        public void Sut_ShouldGuardItsClause(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(PdvController).GetConstructors());

        [Theory, AutoNSubstituteData]
        public void Create_WhenGetPdvReturnAnyPdv_ShouldReturnBadRequest(
            Pdv pdvRequest,
            PdvDto pdvDto,
            PdvController sut)
        {
            sut.PdvQueryRepository.GetPdv(Arg.Any<Guid>()).Returns(pdvDto);

            var response = sut.Create(pdvRequest);

            sut.PdvCommandRepository.DidNotReceive().CreatePdv(Arg.Any<PdvDto>());

            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Theory, AutoNSubstituteData]
        public void Create_ShouldReturnOkay(
            Pdv pdvRequest,
            PdvDto pdvDto,
            PdvController sut)
        {
            sut.PdvQueryRepository.GetPdv(Arg.Any<Guid>()).Returns((PdvDto)null);

            sut.Mapper.Map<PdvDto>(Arg.Any<Pdv>()).Returns(pdvDto);

            var response = sut.Create(pdvRequest);

            sut.PdvCommandRepository.Received().CreatePdv(pdvDto);

            response.Should().BeOfType<CreatedResult>();
        }

        [Theory, AutoNSubstituteData]
        public void Get_WhenGetPdvReturnNull_ShouldReturnNotFound(
            Guid pdvId,
            PdvController sut)
        {
            sut.PdvQueryRepository.GetPdv(pdvId).Returns((PdvDto)null);

            var response = sut.Get(pdvId);

            response.Should().BeOfType<NotFoundResult>();
        }

        [Theory, AutoNSubstituteData]
        public void Get_ShouldReturnPdv(
            Guid pdvId,
            PdvDto pdvDto,
            Pdv pdv,
            PdvController sut)
        {
            sut.PdvQueryRepository.GetPdv(pdvId).Returns(pdvDto);
            sut.Mapper.Map<Pdv>(pdvDto).Returns(pdv);

            var response = sut.Get(pdvId);

            response.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)response).Value.Should().Be(pdv);
        }

        [Theory, AutoNSubstituteData]
        public void GetPdvs_WhenPassLngAndLatAndRepositoryReturnEmpty_ShouldReturnEmptyList(
            PdvController sut)
        {
            List<PdvDto> listOfPdvDto = new List<PdvDto>();
            List<Pdv> listOfPdv = new List<Pdv>();

            sut.PdvQueryRepository.GetPdvs().Returns(listOfPdvDto);
            sut.Mapper.Map<List<Pdv>>(listOfPdvDto).Returns(listOfPdv);

            var response = sut.GetPdvs();

            response.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)response).Value.Should().Be(listOfPdv);
        }

        [Theory, AutoNSubstituteData]
        public void GetPdvs_WhenPassLngAndLat_ShouldReturnListOfPdvs(
            List<PdvDto> pdvDtos,
            List<Pdv> pdvs,
            PdvController sut)
        {
            sut.PdvQueryRepository.GetPdvs().Returns(pdvDtos);
            sut.Mapper.Map<List<Pdv>>(pdvDtos).Returns(pdvs);

            var response = sut.GetPdvs();

            response.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)response).Value.Should().Be(pdvs);
        }

        [Theory, AutoNSubstituteData]
        public void GetByCoordinates_WhenPassLngAndLatAndRepositoryReturnEmpty_ShouldReturnEmptyList(
            string lng,
            string lat,
            PdvController sut)
        {
            List<PdvDto> listOfPdvDto = new List<PdvDto>();
            List<Pdv> listOfPdv = new List<Pdv>();

            sut.PdvQueryRepository.GetInAreaPvs(lng, lat).Returns(listOfPdvDto);
            sut.Mapper.Map<List<Pdv>>(listOfPdvDto).Returns(listOfPdv);

            var response = sut.GetByCoordinates(lng, lat);

            response.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)response).Value.Should().Be(listOfPdv);
        }

        [Theory, AutoNSubstituteData]
        public void GetByCoordinates_WhenPassLngAndLat_ShouldReturnListOfPdvs(
            string lng,
            string lat,
            List<PdvDto> pdvDtos,
            List<Pdv> pdvs,
            PdvController sut)
        {
            sut.PdvQueryRepository.GetInAreaPvs(lng, lat).Returns(pdvDtos);
            sut.Mapper.Map<List<Pdv>>(pdvDtos).Returns(pdvs);

            var response = sut.GetByCoordinates(lng, lat);

            response.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)response).Value.Should().Be(pdvs);
        }
    }
}
