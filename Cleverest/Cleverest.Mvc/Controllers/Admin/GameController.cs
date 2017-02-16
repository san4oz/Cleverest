﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.Mvc.Helpers;
using Cleverest.Mvc.ViewModels.Admin;

namespace Cleverest.Mvc.Controllers.Admin
{
    public class GameController : Controller
    {
        public ActionResult List()
        {
            var games = Site.Managers.Game.All();

            var list = Site.Services.Mapper.Map<IList<Game>, IList<GameViewModel>>(games);

            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new GameViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(GameViewModel gameModel)
        {
            if (!ModelState.IsValid)
                return View(gameModel);

            GameGalleryHelper.SaveLogo(gameModel.Id, gameModel.Image);

            var game = Site.Services.Mapper.Map<GameViewModel, Game>(gameModel);
            Site.Managers.Game.Create(game);

            return RedirectToAction("List", "Game");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return HttpNotFound();

            var game = Site.Managers.Game.Get(id);
            if (game == null)
                return HttpNotFound();

            var gameModel = Site.Services.Mapper.Map<Game, GameViewModel>(game);

            return View(gameModel);
        }

        [HttpPost]
        public ActionResult Edit(GameViewModel gameModel)
        {
            if (!ModelState.IsValid)
                return View(gameModel);

            GameGalleryHelper.SaveLogo(gameModel.Id, gameModel.Image);

            var game = Site.Services.Mapper.Map<GameViewModel, Game>(gameModel);
            Site.Managers.Game.Update(game);

            return RedirectToAction("List", "Game");
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return HttpNotFound();

            Site.Managers.Game.Delete(id);

            return Json(true);
        }
    }
}
