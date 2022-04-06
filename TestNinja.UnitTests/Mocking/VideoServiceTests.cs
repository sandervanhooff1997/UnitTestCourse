using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    /// <summary>
    /// This unit test class uses Moq framework to mock external dependencies of the VideoService class.
    /// </summary>
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _videoService;
        private Mock<IVideoRepository> _videoRepository;

        [SetUp]
        public void Setup()
        {
            _videoRepository = new Mock<IVideoRepository>();
            _videoService = new VideoService();
        }
        
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnsError()
        {
            var fileReader = new Mock<IFileReader>();
            fileReader
                .Setup((fr) => fr.Read("video.txt"))
                .Returns("");

            var title = _videoService.ReadVideoTitle(fileReader.Object);
            
            Assert.That(title, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnAnEmptyString()
        {
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv(_videoRepository.Object);
            
            // Assert.That(result, Is.EqualTo(""));
            result.Should().Be("");
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AFewUnprocessedVideos_ReturnCommaSeperatedListOfUnprocessedVideoIds()
        {
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>()
            {
                new Video() {Id = 1, IsProcessed = true},
                new Video() {Id = 2, IsProcessed = true}
            });

            var result = _videoService.GetUnprocessedVideosAsCsv(_videoRepository.Object);
            
            // Assert.That(result, Is.EqualTo("1,2"));
            result.Should().Be("1,2");
        }
    }
}