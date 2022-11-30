using AppCitas.Service.Controllers;
using AppCitas.Service.Entities;
using AppCitas.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AppCitas.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        Mock<IUserRepository> mockUserRespository;
        Mock<ILikesRepository> mockLikeRespository;

        private LikesController likesControllerUt;

        [TestInitialize]
        public void RunBeforeEachTest()
        {
            this.mockUserRespository = new Mock<IUserRepository>();
            this.mockLikeRespository = new Mock<ILikesRepository>();
        }

        [TestMethod]
        public void AddLike__RunSuccesful()
        {
            // Arrange
            this.likesControllerUt = new LikesController(this.mockUserRespository.Object, this.mockLikeRespository.Object);
            AppUser appUser = new AppUser() { Id = 1, City = "ags", Gender = "M", UserName = "test" }; 
            this.mockUserRespository.Setup(method => method.GetUserByUsernameAsync(It.IsAny<string>())).ThrowsAsync(new Exception("Error"));
            this.mockLikeRespository.Setup(method => method.GetUserWithLikes(It.IsAny<int>())).ReturnsAsync(appUser);

            // Act
            this.likesControllerUt.AddLike(It.IsAny<string>()).Start();

            // Assert
            this.mockUserRespository.Verify(method => method.SaveAllAsync(), Times.Never);
        }

    }
}