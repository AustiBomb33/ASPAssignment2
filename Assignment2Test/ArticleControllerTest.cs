using ASPAssignment2.Controllers;
using ASPAssignment2.Data;
using ASPAssignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Assignment2Test
{
    [TestClass]
    public class ArticleControllerTest
    {
        private ApplicationDbContext _context;
        private List<Article> articles = new List<Article>();
        private ArticlesController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            //instantiate DB
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            //Create mock data
            Author author = new Author { AuthorName = "Austin Marcoux", AccountID = Guid.NewGuid().ToString(), AuthorId = 0, Articles = articles };
            _context.Add(author);

            articles.Add(new Article { Author = author, AuthorId = 0, Title = "Lorem Ipsum", Content = "Lorem Ipsum Dolor Sit Amet", Id = 1, PeerReviewed = false });
            articles.Add(new Article { Author = author, AuthorId = 0, Title = "Coolest Teacher Ever", Content = "Rich Freeman, of course", Id = 3, PeerReviewed = false });
            articles.Add(new Article { Author = author, AuthorId = 0, Title = "The Truffle Shuffle", Content = "Heyyyyyy youuuuu guyssss", Id = 2, PeerReviewed = false });

            foreach (Article a in articles)
            {
                _context.Add(a);
            }
            _context.SaveChanges();

            //instantiate controller
            controller = new ArticlesController(_context);
        }

        [TestMethod]
        public void IndexViewIsNamed()
        {
            //arrange not needed
            //act
            var result = controller.Index();
            var viewResult = (ViewResult)result.Result;
            //assert
            Assert.AreEqual("Index", viewResult.ViewName);

        }

        [TestMethod]
        public void IndexViewLoadsArticles()
        {
            //arrange not needed
            //act
            var result = controller.Index();
            var viewResult = (ViewResult)result.Result;

            List<Article> model = (List<Article>)viewResult.Model;

            //assert
            CollectionAssert.AreEqual(articles, model);

        }

        [TestMethod]
        public void DetailsArticleNotFound()
        {
            //arrange not needed
            //act
            var result = controller.Details(Id: -1);
            var notFoundResult = (NotFoundResult)result.Result;

            //assert
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod]
        public void DetailsIdNull()
        {
            //arrange not needed
            //act
            var result = controller.Details(Id: null);
            var notFoundResult = (NotFoundResult)result.Result;

            //assert
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod]
        public void DetailsReturnsView()
        {
            //arrange not needed
            //act
            var result = controller.Details(Id: 1);
            var viewResult = (ViewResult)result.Result;

            //assert
            Assert.AreEqual("Details", viewResult.ViewName);
        }

        [TestMethod]
        public void ArticleExistsReturnsTrue()
        {
            //arrange not needed
            //act
            var result = controller.ArticleExists(1);

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ArticleExistsReturnsFalse()
        {
            //arrange not needed
            //act
            var result = controller.ArticleExists(-1);

            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteGetIdNull()
        {
            //arrange not needed
            //act
            var result = controller.Delete(null);
            var notFoundResult = (NotFoundResult)result.Result;

            //assert
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod]
        public void DeleteGetArticleNotFound()
        {
            //arrange not needed
            //act
            var result = controller.Delete(-1);
            var notFoundResult = (NotFoundResult)result.Result;

            //assert
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod]
        public void DeleteGetReturnsView()
        {
            //arrange not needed
            //act
            var result = controller.Delete(1);
            var viewResult = (ViewResult)result.Result;

            //assert
            Assert.AreEqual("Delete", viewResult.ViewName);
        }


    }
}
