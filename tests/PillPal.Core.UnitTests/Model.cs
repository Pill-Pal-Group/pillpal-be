using AutoMapper;
using FluentAssertions;
using PillPal.Core.Dtos.Drugs.Commands;
using PillPal.Core.Models;

namespace PillPal.Core.UnitTests;

public class Model
{
    [Test]
    public void Test_Drud_and_DrugDtos_Prop()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Drug, CreateDrugCommand>().ReverseMap();
        });

        IMapper mapper = new Mapper(config);

        CreateDrugCommand drug = new CreateDrugCommand(
            DrugCode: "DC1",
            DrugName: "DrugName1",
            DrugDescription: "This is a description of the drug.",
            SideEffect: "Possible side effects...",
            Indication: "Indications...",
            Contraindication: "Contraindications...",
            Warning: "Warnings...",
            ImageUrl: "http://example.com/image.jpg"
        );

        //map dto to model
        Drug model = mapper.Map<Drug>(drug);

        //validate properties
        model.DrugCode.Should().Be(drug.DrugCode);
        model.DrugName.Should().Be(drug.DrugName);

        model.Id.Should().NotBe(Guid.Empty);
        model.CreatedAt.Should().NotBeNull();
        model.UpdatedAt.Should().BeNull();

        //print test result
        TestContext.WriteLine("Drug and DrugDtos models are validated");
        TestContext.WriteLine(model.Id.ToString());
        TestContext.WriteLine("ca:" + model.CreatedAt.ToString());
        TestContext.WriteLine("ua:" + model.UpdatedAt.ToString());



    }

    [Test]
    public void Test_Base_Prop()
    {

        Drug drug = new Drug();

        //validate base properties
        drug.Id.Should().NotBe(Guid.Empty);
        drug.CreatedAt.Should().NotBeNull();
        drug.UpdatedAt.Should().BeNull();
        drug.CreatedBy.Should().BeNull();
        drug.UpdatedBy.Should().BeNull();
        drug.IsDeleted.Should().BeFalse();
        drug.DeletedAt.Should().BeNull();


        //print test result
        TestContext.WriteLine("Drug model is validated");
        TestContext.WriteLine(drug.Id.ToString());
        TestContext.WriteLine(drug.CreatedAt.ToString());


    }
}
