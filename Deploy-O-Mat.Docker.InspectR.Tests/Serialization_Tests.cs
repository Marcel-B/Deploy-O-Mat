using System.Collections.Generic;
using System.Text.Json;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.ProcessModels;
using NUnit.Framework;

namespace Deploy_O_Mat.Docker.InspectR.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var json = System.IO.File.ReadAllText("service.json");
            var input = JsonSerializer.Deserialize<List<InspectService>>(json);
            Assert.Pass();
        }
    }
}