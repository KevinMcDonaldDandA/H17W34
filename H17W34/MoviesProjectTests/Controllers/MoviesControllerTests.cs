/*
*  File	    MoviesControllerTests.cs
*  Author	Kevin McDonald, Dundee & Angus College, kevin.mcdonald@dundeeandangus.ac.uk
*  Date     8th March 2019
*  Purpose	The Test class for our MovieController 
*  Notes    This class is responsible for the testing the controller Actions.
*           It uses the Moq framework for mocking test data.
*
*       Copyright © Kevin McDonald 2019. All rights reserved.
*/
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoviesProject.Models;
using MoviesProject.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace MoviesProject.Controllers.Tests
{
    [TestClass]
    public class MoviesControllerTests
    {

        [TestMethod]
        public void Index_ContainsAListofCustomers_Success()
        {
            //  Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            mock.Setup(m => m.Movies).Returns(new Movie[]
            {
                new Movie{ ID = 1, DirectorId = 1, Title = "Test Movie 1", UserRating = 1.0 },
                new Movie{ ID = 2, DirectorId = 2, Title = "Test Movie 2", UserRating = 2.0 },
                new Movie{ ID = 3, DirectorId = 3, Title = "Test Movie 3", UserRating = 3.0 }
            }.AsQueryable());

            MoviesController controller = new MoviesController(mock.Object);

            //  Act
            var result = controller.Index() as ViewResult;
            var actual = result.Model;

            //  Assert
            Assert.IsNotNull(actual);

        }

        [TestMethod]
        public void Create_ContainsViewModel_Success()
        {
            //  Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            mock.Setup(m => m.Directors).Returns(new Director[]
            {
                new Director{ ID = 1, FName = "FTest1", LName = "LTest1"},
                new Director{ ID = 2, FName = "FTest2", LName = "LTest2"},
            }.AsQueryable());

            MoviesController controller = new MoviesController(mock.Object);

            //  Act
            var result = controller.Create() as ViewResult;
            var actual = result.Model;

            //  Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public void Create_ViewModelHasDirectors_Success()
        {
            //  Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            mock.Setup(m => m.Directors).Returns(new Director[]
            {
                new Director{ ID = 1, FName = "FTest1", LName = "LTest1"},
                new Director{ ID = 2, FName = "FTest2", LName = "LTest2"},
            }.AsQueryable());

            MoviesController controller = new MoviesController(mock.Object);
            int expected = 2;

            //  Act
            var result = controller.Create() as ViewResult;
            MovieDirectors md = result.Model as MovieDirectors;
            var actual = md.Directors.Count;

            //  Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Details_ContainsMovieModel()
        {
            //  Arrange
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            int id = 1;
            mock.Setup(m => m.GetMovieDetails(id)).Returns(new Movie { ID = 1 });

            MoviesController controller = new MoviesController(mock.Object);

            //  Act
            var result = controller.Details(1) as ViewResult;
            var actual = result.Model as Movie;

            //  Assert
            Assert.IsNotNull(actual);
        }
    }
}
//------------------------------- End of File -------------------------------​