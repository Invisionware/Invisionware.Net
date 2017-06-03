using Invisionware.Net.WebUtils;
using NUnit.Framework;
using System;
using System.Net.Http;

namespace Invisionware.Net.WebUitls.Tests
{
    [TestFixture]
    public class HttpClientExtensions
    {
        [SetUp]
        public void Initialize()
        {

        }

        [Test]
        public void GetFileAsyncTest()
        {
            var httpClient = new HttpClient();

            var resultTask = httpClient.GetFileAsBase64Async(new Uri("http://dummyimage.com/300.png/09f/fff"));

            resultTask.Wait();

            Assert.NotNull(resultTask.Result);
            Assert.IsTrue(resultTask.Result.StartsWith("iVBORw0KGgoAAAANSUhEUgAAASwAAAEsBAMAAACLU5NGAAAAG1BMVEUAmf"));
        }
    }
}
