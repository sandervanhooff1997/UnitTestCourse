using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking.HouseKeeperHelper;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HouseKeeperServiceTests
    {
        private Housekeeper _existingHouseKeeper;
        private Mock<IHouseKeeperRepository> _repository;
        private Mock<IStatementGenerator> _generator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _msgBox;
        private HouseKeeperService _service;
        private string _statementFileName;

        [SetUp]
        public void Setup()
        {
            _existingHouseKeeper = new Housekeeper()
            {
                Oid = 1,
                Email = "johndoe@example.com",
                FullName = "John Doe",
                StatementEmailBody = "Statement email body"
            };

            _repository = new Mock<IHouseKeeperRepository>();
            _repository
                .Setup(r => r.GetHouseKeepers())
                .Returns(new List<Housekeeper>
                {
                    _existingHouseKeeper
                }.AsQueryable());

            _statementFileName = "Filename";
            _generator = new Mock<IStatementGenerator>();
            _generator.Setup(g =>
                    g.SaveStatement(_existingHouseKeeper.Oid, _existingHouseKeeper.FullName, DateTime.Today))
                .Returns(() => _statementFileName);

            _emailSender = new Mock<IEmailSender>();
            _msgBox = new Mock<IXtraMessageBox>();
            _service = new HouseKeeperService(_repository.Object, _generator.Object, _emailSender.Object,
                _msgBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(DateTime.Today);

            VerifyStatementSaved();
        }

        private void VerifyStatementSaved()
        {
            _generator.Verify(g =>
                g.SaveStatement(_existingHouseKeeper.Oid, _existingHouseKeeper.FullName, DateTime.Today));
        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsNull_ShouldNotGenerateStatements()
        {
            _existingHouseKeeper.Email = null;

            _service.SendStatementEmails(DateTime.Today);

            VerifyStatementNotSaved();
        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsWhiteSpace_ShouldNotGenerateStatements()
        {
            _existingHouseKeeper.Email = " ";

            _service.SendStatementEmails(DateTime.Today);

            VerifyStatementNotSaved();
        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsEmpty_ShouldNotGenerateStatements()
        {
            _existingHouseKeeper.Email = "";

            _service.SendStatementEmails(DateTime.Today);

            VerifyStatementNotSaved();
        }

        [Test]
        public void SendStatementEmails_StatementFilenameIsNull_ShouldNotSendEmail()
        {
            _statementFileName = null;

            _service.SendStatementEmails(DateTime.Today);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_StatementFilenameIsWhiteSpace_ShouldNotSendEmail()
        {
            _statementFileName = " ";

            _service.SendStatementEmails(DateTime.Today);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_StatementFilenameIsEmpty_ShouldNotSendEmail()
        {
            _statementFileName = "";

            _service.SendStatementEmails(DateTime.Today);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _service.SendStatementEmails(DateTime.Today);

            VerifyEmailSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_ShowMessageBox()
        {
            _emailSender.Setup(e => e.EmailFile(
                    It.IsAny<string>(), 
                    It.IsAny<string>(),
                    It.IsAny<string>(), 
                    It.IsAny<string>()))
                .Throws<Exception>();

            _service.SendStatementEmails(DateTime.Today);
            
            VerifyMessageBoxShowed();
        }

        private void VerifyMessageBoxShowed()
        {
            _msgBox.Verify(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }

        private void VerifyStatementNotSaved()
        {
            _generator.Verify(
                g => g.SaveStatement(_existingHouseKeeper.Oid, _existingHouseKeeper.FullName, DateTime.Today),
                Times.Never);
        }

        private void VerifyEmailSent()
        {
            _emailSender.Verify(e => e.EmailFile(
                _existingHouseKeeper.Email,
                _existingHouseKeeper.StatementEmailBody,
                _statementFileName,
                It.IsAny<string>()));
        }

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(e => e.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);
        }
    }
}