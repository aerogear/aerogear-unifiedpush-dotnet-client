﻿﻿/**
 * JBoss, Home of Professional Open Source
 * Copyright Red Hat, Inc., and individual contributors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * 	http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using AeroGear;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;
using System.Threading.Tasks;

namespace tests
{
    [TestClass]
    public class SenderClientTest
    {
        [TestMethod]
        public async Task SentMessage()
        {
            //given
            var mock = new Mock<HttpClient>();
            UnifiedMessage unifiedMessage = new UnifiedMessage();
            unifiedMessage.message.alert = "Test push message";
            SenderClient client = new SenderClient(mock.Object);

            //when
            mock.Setup(httpClient => httpClient.Send(unifiedMessage)).ReturnsAsync(HttpStatusCode.OK);
            await client.Send(unifiedMessage);

            //then
            mock.Verify();
        }
    }
}
