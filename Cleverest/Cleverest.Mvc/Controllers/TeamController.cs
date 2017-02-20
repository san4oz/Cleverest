using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cleverest.Business.Entities;
using Cleverest.Mvc.Security;
using Cleverest.Mvc.ViewModels;

namespace Cleverest.Mvc.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        [HttpPost]
        public ActionResult Search(string query)
        {
            var teams = Site.Managers.Team.Search(query);

            var myTeams = Site.Managers.Team.GetTeamsByAccountId(WebSecurity.User.Id);

            var result = teams.Except(myTeams).ToList();

            var viewModel = Site.Services.Mapper.Map<IList<Team>, IList<TeamViewModel>>(result);

            return PartialView("_TeamSuggestions", viewModel);
        }

        [HttpPost]
        public ActionResult SuggestionDetails(string id)
        {
            var team = Site.Managers.Team.Get(id);

            TeamViewModel viewModel = null;

            if (team != null)
            {
                viewModel = Site.Services.Mapper.Map<Team, TeamViewModel>(team);
            }

            return PartialView("_SuggestionDetails", viewModel);
        }

        [HttpPost]
        public ActionResult SendJoinRequest(string teamId)
        {
            if (string.IsNullOrEmpty(teamId))
                return Json(false);


            var teamToJoin = Site.Managers.Team.Get(teamId);
            if (teamToJoin == null)
                return Json(false);

            var request = new AccountTeamRequest() { FromId = WebSecurity.User.Id, ToId = teamToJoin.OwnerId, RequestType = AccountTeamRequestType.Join, TeamId = teamToJoin.Id };

            Site.Managers.AccountTeamRequest.Create(request);

            return Json(true);
        }

        [HttpPost]
        public ActionResult SendInviteRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProcessRequest(string requestId, bool approved)
        {
            Site.Managers.AccountTeamRequest.ProcessRequest(requestId, approved);
            return Json(true);
        }

        [HttpGet]
        public ActionResult MyInvitations()
        {
            var result = new List<AccountTeamRequestViewModel>();

            var requests = Site.Managers.AccountTeamRequest.GetRequestsByReceiverId(WebSecurity.User.Id).ToList();
            requests.ForEach(request =>
            {
                var viewModel = new AccountTeamRequestViewModel();
                viewModel.FromAccount = Site.Services.Mapper.Map<Account, ProfileViewModel>(Site.Managers.Account.Get(request.FromId));
                viewModel.Team = Site.Services.Mapper.Map<Team, ViewModels.TeamViewModel>(Site.Managers.Team.Get(request.TeamId));
                viewModel.Type = request.RequestType;
                viewModel.Id = request.Id;

                result.Add(viewModel);
            });

            return View(result);
        }

        [HttpGet]
        public ActionResult CreateTeam()
        {
            var viewModel = new TeamViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateTeam(TeamViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            if (Site.Managers.Team.GetTeamByName(viewModel.Name) != null)
            {
                ModelState.AddModelError("Name", "Пробачте. Команда з такою назвою вже існує.");
                return View(viewModel);
            }

            var team = new Team() { Name = viewModel.Name, OwnerId = WebSecurity.User.Id };

            Site.Managers.Team.Create(team);

            return RedirectToAction("MyTeams");
        }


        [HttpGet]
        public ActionResult MyTeams()
        {
            var teams = Site.Managers.Team.GetTeamsByAccountId(WebSecurity.User.Id).ToList();

            var teamViewModels = new List<TeamViewModel>();

#warning refactor this piece of shit(write something like object[] GetTeamLookups(string teamId))
            teams.ForEach(t =>
            {
                teamViewModels.Add(new TeamViewModel() { Name = t.Name, ParticipantsCount = Site.Managers.Account.GetAccountsByTeamId(t.Id).Count(), Id = t.Id });
            });

            return View(teamViewModels);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return HttpNotFound();

            var team = Site.Managers.Team.Get(id);
            if (team == null)
                return HttpNotFound();

            var viewModel = new TeamDetailsViewModel();
            viewModel.Info = Site.Services.Mapper.Map<Team, TeamDetailsInfoViewModel>(team);

            var participants = Site.Managers.Account.GetAccountsByTeamId(team.Id);
            viewModel.Participants = Site.Services.Mapper.Map<IList<Account>, IList<ProfileViewModel>>(participants);

            return View(viewModel);
        }
    }
}
