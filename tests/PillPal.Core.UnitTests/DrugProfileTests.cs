using AutoMapper;
using FluentAssertions;
using PillPal.Core.Dtos.Drug.Commands;
using PillPal.Core.Dtos.Drug.Queries;
using PillPal.Core.Mappings;
using PillPal.Core.Models;

namespace PillPal.Core.UnitTests;

[TestFixture]
public class DrugProfileTests
{
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperConfigure>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Test]
    public void CreateMap_DrugToCreateDrugCommand_MapsCorrectly()
    {
        // Arrange
        var drug = new Drug();

        // Act
        var result = _mapper.Map<CreateDrugCommand>(drug);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CreateDrugCommand>();
    }

    [Test]
    public void CreateMap_DrugToGetDrugQuery_MapsCorrectly()
    {
        // Arrange
        var drug = new Drug();

        // Act
        var result = _mapper.Map<GetDrugQuery>(drug);

        // Assert
        result.Should().NotBeNull();
    }
}