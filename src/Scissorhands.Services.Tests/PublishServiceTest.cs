﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aliencube.Scissorhands.Services.Configs;
using Aliencube.Scissorhands.Services.Helpers;

using FluentAssertions;

using MarkdownDeep;

using Moq;

using NUnit.Framework;

using RazorEngine.Templating;

namespace Aliencube.Scissorhands.Services.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="PublishService" /> class.
    /// </summary>
    [TestFixture]
    public class PublishServiceTest
    {
        private Directories _directories;
        private List<Theme> _themes;
        private Contents _contents;
        private Mock<IYamlSettings> _settings;
        private Mock<IRazorEngineService> _engine;
        private Markdown _md;
        private Mock<IPublishHelper> _helper;
        private IPublishService _service;

        /// <summary>
        /// Initialises all the resources for test.
        /// </summary>
        [SetUp]
        public void Init()
        {
            this._directories = new Directories() { Themes = "themes", Posts = "posts", Published = "published" };
            this._themes = new List<Theme>()
                               {
                                   new Theme() { Name = "default", Master = "master.cshtml" },
                                   new Theme() { Name = "second", Master = "master.cshtml" },
                               };

            this._settings = new Mock<IYamlSettings>();
            this._settings.SetupGet(p => p.Directories).Returns(this._directories);
            this._settings.SetupGet(p => p.Themes).Returns(this._themes);
            this._settings.SetupGet(p => p.Contents).Returns(this._contents);

            this._engine = new Mock<IRazorEngineService>();
            this._md = new Markdown();

            this._helper = new Mock<IPublishHelper>();

            this._service = new PublishService(this._settings.Object, null, this._helper.Object);
        }

        /// <summary>
        /// Releases all the resources no longer necessary.
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            if (this._service != null)
            {
                this._service.Dispose();
            }
        }

        /// <summary>
        /// Tests whether template is returned or not.
        /// </summary>
        /// <param name="themeName">
        /// The theme name.
        /// </param>
        /// <param name="extension">
        /// The extension.
        /// </param>
        /// <param name="html">
        /// The HTML contents.
        /// </param>
        [Test]
        [TestCase("default", ".md", "<html></html>")]
        public void GivenThemeNameShouldReturnTemplate(string themeName, string extension, string html)
        {
            this._contents = new Contents() { Theme = themeName, Extension = extension };

            this._helper.Setup(p => p.Read(It.IsAny<string>())).Returns(html);

            //var template = this._service.GetTemplate(themeName);
            //template.Should().NotBeNullOrWhiteSpace();
            //template.Should().Be(html);
        }
    }
}
