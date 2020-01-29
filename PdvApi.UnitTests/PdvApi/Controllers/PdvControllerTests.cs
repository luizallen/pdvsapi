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
            PdvController sut)
        {
            sut.PdvRepository.GetPdv(Arg.Any<Guid>()).Returns(new PdvDto());

            var response = sut.Create(pdvRequest);

            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Theory, AutoNSubstituteData]
        public void Create_ShouldReturnOkay(
            Pdv pdvRequest,
            PdvDto pdvDto,
            PdvController sut)
        {
            sut.Mapper.Map<PdvDto>(Arg.Any<Pdv>()).Returns(pdvDto);

            var response = sut.Create(pdvRequest);

            sut.Received().PdvRepository.CreatePdv(pdvDto);

            response.Should().BeOfType<CreatedResult>();
        }

        [Theory, AutoNSubstituteData]
        public void Get_WhenGetPdvReturnNull_ShouldReturnNotFound(
            Guid pdvId,
            PdvController sut)
        {
            sut.PdvRepository.GetPdv(pdvId).Returns((PdvDto)null);

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
            sut.PdvRepository.GetPdv(pdvId).Returns(pdvDto);
            sut.Mapper.Map<Pdv>(pdvDto).Returns(pdv);

            var response = sut.Get(pdvId);

            response.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)response).Value.Should().Be(pdv);
        }

        [Theory, AutoNSubstituteData]
        public void Get_WhenPassLngAndLatAndRepositoryReturnEmpty_ShouldReturnEmptyList(
            string lng,
            string lat,
            PdvController sut)
        {
            List<PdvDto> listOfPdvDto = new List<PdvDto>();
            List<Pdv> listOfPdv = new List<Pdv>();

            sut.PdvRepository.GetPdv(lng, lat).Returns(listOfPdvDto);
            sut.Mapper.Map<List<Pdv>>(listOfPdvDto).Returns(listOfPdv);

            var response = sut.GetByCoordinates(lng, lat);

            response.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)response).Value.Should().Be(listOfPdv);
        }

        [Theory, AutoNSubstituteData]
        public void Get_WhenPassLngAndLat_ShouldReturnListOfPdvs(
            string lng,
            string lat,
            List<PdvDto> pdvDtos,
            List<Pdv> pdvs,
            PdvController sut)
        {
            sut.PdvRepository.GetPdv(lng, lat).Returns(pdvDtos);
            sut.Mapper.Map<List<Pdv>>(pdvDtos).Returns(pdvs);

            var response = sut.GetByCoordinates(lng, lat);

            response.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)response).Value.Should().Be(pdvs);
        }
    }
}
