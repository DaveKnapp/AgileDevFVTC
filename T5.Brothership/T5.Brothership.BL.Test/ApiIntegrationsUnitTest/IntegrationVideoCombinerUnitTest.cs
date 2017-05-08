using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.Integrations;
using T5.Brothership.BL.Test.ClientFakes;
using System.Threading.Tasks;
using T5.Brothership.PL.Test;
using T5.Brothership.Entities.Models;
using System.Linq;

namespace T5.Brothership.BL.Test.ApiIntegrationsUnitTest
{
    [TestClass]
    public class IntegrationVideoCombinerUnitTest
    {
        [TestMethod, TestCategory("UnitTest")]
        public async Task GetRecentVideos_VideosReturned_ReturnedListNotNull()
        {
            var videoCombiner = new IntegrationVideoCombiner(new YoutubeDataClientFake(), new TwitchClientFake(), new FakeBrothershipUnitOfWork());
            var videos = await videoCombiner.GetRecentVideos(1, 20);

            Assert.IsTrue(videos.Count > 1);
            foreach (var video in videos)
            {
                System.Diagnostics.Debug.WriteLine(video.Id + " " + video.ContentType + " " + video.UploadTime);
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task GetRecentVideos_GreaterQtyRequestedThanActualReturnsAllActual_ListCountEqualsActual()
        {
            const int expectedCount = 29;
            var videoCombiner = new IntegrationVideoCombiner(new YoutubeDataClientFake(), new TwitchClientFake(), new FakeBrothershipUnitOfWork());
            var videos = await videoCombiner.GetRecentVideos(1, 50);

            Assert.IsTrue(videos.Count == expectedCount);
        }


        [TestMethod, TestCategory("UnitTest")]
        public async Task GetRecentVideos_ReturnVideosOrderNewestToOldest_ItemsInOrder()
        {
            var videoCombiner = new IntegrationVideoCombiner(new YoutubeDataClientFake(), new TwitchClientFake(), new FakeBrothershipUnitOfWork());
            var videos = await videoCombiner.GetRecentVideos(1, 25);

            VideoContent previousVideo = null;

            foreach (var video in videos)
            {
                System.Diagnostics.Debug.WriteLine(video.Id + " " + video.ContentType + " " + video.UploadTime);
            }

            foreach (var video in videos)
            {
                if(previousVideo != null)
                {
                    Assert.IsTrue(previousVideo.UploadTime >= video.UploadTime);
                }

                previousVideo = video;
            }
        }

        [TestMethod, TestCategory("UnitTest")]
        public async Task GetRecentVideos_ReturnVideosAreNotTheSame_ItemsInOrder()
        {
            var videoCombiner = new IntegrationVideoCombiner(new YoutubeDataClientFake(), new TwitchClientFake(), new FakeBrothershipUnitOfWork());
            var videos = await videoCombiner.GetRecentVideos(1, 25);

            VideoContent previousVideo = null;

            foreach (var video in videos)
            {
                System.Diagnostics.Debug.WriteLine(video.Id + " " + video.ContentType + " " + video.UploadTime);
            }

            foreach (var video in videos)
            {
                Assert.IsTrue(videos.Count(p => p.Id == video.Id) <= 1);

                previousVideo = video;
            }
        }
    }
}
