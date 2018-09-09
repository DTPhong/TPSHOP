using AutoMapper;
using System.Web.Mvc;
using TPshop.Model.Models;
using TPshop.Service;
using TPshop.Web.Infrastructure.Extensions;
using TPshop.Web.Models;

namespace TPshop.Web.Controllers
{
    public class ContactController : Controller
    {
        IContactDetailService _contactDetailService;
        IFeedbackService _feedbackService;
        public ContactController(IContactDetailService contactDetailService, IFeedbackService feedbackService)
        {
            this._contactDetailService = contactDetailService;
            this._feedbackService = feedbackService;
        }
        // GET: Contact
        public ActionResult Index()
        {
            FeedbackViewModel viewModel = new FeedbackViewModel();
            viewModel.ContactDetail = GetDetail();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                Feedback newFeedback = new Feedback();
                newFeedback.UpdateFeedback(feedbackViewModel);
                _feedbackService.Create(newFeedback);
                _feedbackService.Save();

                ViewData["SuccessMsg"] = "Send feedback success.";
                feedbackViewModel.Name = string.Empty;
                feedbackViewModel.Message = string.Empty;
                feedbackViewModel.Email = string.Empty;
            }
            feedbackViewModel.ContactDetail = GetDetail();
            return View("Index", feedbackViewModel);
        }

        public ContactDetailViewModel GetDetail()
        {
            var model = _contactDetailService.GetDefaultContact();
            var viewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return viewModel;
        }
    }
}