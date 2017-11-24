using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebPhoneNumber.Models;
using WebPhoneNumber.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace WebPhoneNumber.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new FormModel() { PhoneExemples = LibPhoneNumberHelper.GetPhoneExemple() };
            var regionList = model.PhoneExemples.Select(x => new { Id = x.Item1, Value = x.Item1 });
            model.RegionList = new SelectList(regionList, "Id", "Value");
            return View(model);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Verify(FormModel phoneModel)
        {
            phoneModel.PhoneExemples = LibPhoneNumberHelper.GetPhoneExemple();
            var regionList = phoneModel.PhoneExemples.Select(x => new { Id = x.Item1, Value = x.Item1 });
            phoneModel.RegionList = new SelectList(regionList, "Id", "Value");

            if (ModelState.IsValid)
            {
                phoneModel.IsValid = LibPhoneNumberHelper.IsValidPhoneNumber(phoneModel.Phone, phoneModel.RegionCode);

                if(!phoneModel.IsValid)
                {
                    phoneModel.Message = "Le numéro n'est pas valide";
                }
                else
                {
                    phoneModel.Message = "Le numéro est valide";
                }
            }

            return View("Index", phoneModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
