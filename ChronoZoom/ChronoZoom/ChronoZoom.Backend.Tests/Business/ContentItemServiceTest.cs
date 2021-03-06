﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChronoZoom.Backend.Business;
using Moq;
using ChronoZoom.Backend.Data.Interfaces;
using ChronoZoom.Backend.Exceptions;
using System.Collections.Generic;

namespace ChronoZoom.Backend.Tests.Business
{
    [TestClass]
    public class ContentItemServiceTest
    {
        [TestMethod]
        public void ContentItemService_Find_Test()
        {
            // Arrange
            Mock<IContentItemDao> mock = new Mock<IContentItemDao>(MockBehavior.Strict);
            mock.Setup(setup => setup.FindAll(It.IsAny<int>())).Returns(new Entities.ContentItem[] 
            {
                new Entities.ContentItem()
                {
                    Id = 1,
                    Title = "Bevrijding",
                    BeginDate = 1945M,
                    EndDate = 1945M,
                    Depth = 1,
                    Source = "UrlNaSource",
                    ParentId = 1,
                    HasChildren = false,
                }
            });
            ContentItemService target = new ContentItemService(mock.Object);

            // Act
            IEnumerable<Entities.ContentItem> result = target.GetAll(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Entities.ContentItem resultFirst = result.First();
            Assert.AreEqual(1, resultFirst.Id);
            Assert.AreEqual("Bevrijding", resultFirst.Title);
            Assert.AreEqual(1945M, resultFirst.BeginDate);
            Assert.AreEqual(1945M, resultFirst.EndDate);
            Assert.AreEqual("UrlNaSource", resultFirst.Source);
            Assert.AreEqual(1, resultFirst.Depth);
            Assert.AreEqual(1, resultFirst.ParentId);
            Assert.AreEqual(false, resultFirst.HasChildren);
        }

        [ExpectedException(typeof(ContentItemNotFoundException))]
        public void ContentItemService_FindNotFound_Test()
        {
            // Arrange
            Mock<IContentItemDao> mock = new Mock<IContentItemDao>(MockBehavior.Strict);
            mock.Setup(setup => setup.FindAll(It.IsAny<int>())).Throws(new ContentItemNotFoundException());
            ContentItemService target = new ContentItemService(mock.Object);

            // Act
            target.GetAll(-1);
        }
    }
}
