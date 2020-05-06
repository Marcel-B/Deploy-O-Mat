using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Services;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Data.Contracts;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models;
using com.b_velop.Deploy_O_Mat.Shared;
using com.b_velop.Deploy_O_Mat.Shared.Contracts;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Deploy_O_Mat.Docker.InspectR.Tests
{
    public class FakeLogger<T> : ILogger<T>
    {
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
        }

        public bool IsEnabled(LogLevel logLevel)
            => true;

        public IDisposable BeginScope<TState>(TState state)
        {
            return default;
        }
    }

    public class ConsoleParserTests
    {
        private ILogger<T> GetLogger<T>()
        {
            return new FakeLogger<T>();
        }

        private IProcessor GetProcessor(ProcessResult returnContent = null)
        {
            var mock = new Mock<IProcessor>();
            mock.Setup(p => p.Process(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(returnContent);
            return mock.Object;
        }

        private IInspectRRepository GetRepo()
        {
            var mock = new Mock<IInspectRRepository>();
            return mock.Object;
        }

        private IMapper GetMapper<T>()
        {
            var mock = new Mock<IMapper>();
            mock.Setup(m => m.Map<T>(It.IsAny<object>())).Returns(null);
            return mock.Object;
        }

        [Test]
        public async Task Read()
        {
            // Arrange
            const int expected = 18;
            const int delta = 0;
            var content = await File.ReadAllTextAsync("services.txt");
            var returnContext = new ProcessResult {Success = true, ReturnCode = 0, Result = content};
            var sut = new DockerServiceService(GetProcessor(returnContext), GetMapper<DockerServiceDetail>(), GetRepo(), GetLogger<DockerServiceService>());

            // Act
           var actual = await sut.GetDockerServices();

            // Assert
            Assert.AreEqual(expected, actual.Count(), delta, "Not enough items in the collection");
        }
        
        [Test]
        public async Task ReadInspect()
        {
            // Arrange
            const int expected = 18;
            const int delta = 0;
            var content = await File.ReadAllTextAsync("inspect.json");
            var returnContext = new ProcessResult {Success = true, ReturnCode = 0, Result = content};
            var sut = new DockerServiceService(GetProcessor(returnContext), GetMapper<IEnumerable<DockerServiceDetail>>(), GetRepo(), GetLogger<DockerServiceService>());

            // Act
            var actual = await sut.GetDockerServiceDetails(new []{"123", "345"});

            // Assert
            Assert.Pass();
            // Assert.AreEqual(expected, actual.Count(), delta, "Not enough items in the collection");
        }
    }
}