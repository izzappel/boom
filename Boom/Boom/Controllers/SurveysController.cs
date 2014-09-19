using Boom.Domain;
using Microsoft.AspNet.Mvc;
using System;
using System.Linq;

namespace Boom.Controllers
{
    public class SurveysController : Controller
    {
        private BoomContext boomContext;

        public SurveysController(BoomContext boomContext)
        {
            this.boomContext = boomContext;
        }

        // GET: /surveys/
        public IActionResult Get()
        {
            var surveys = this.boomContext.Surveys.ToList();
            return this.Json(surveys);
        }

        // GET: /surveys/?open=true
        public IActionResult Get(bool open)
        {
            var surveys = this.boomContext.Surveys.AsQueryable();

            if (open)
            {
                surveys = surveys.Where(s => s.EndDate > DateTime.Now && s.EndDate > s.StartDate);
            }
            surveys.ToList();
            return this.Json(surveys);
        }

        // GET: /surveys/{id}
        public IActionResult Get(long id)
        {
            var survey = this.boomContext.Surveys.SingleOrDefault(s => s.Id == id);

            if (survey == null)
            {
                return this.HttpNotFound();
            }

            return this.Json(survey);
        }

        // POST: /surveys/
        public IActionResult Post([FromBody] Survey survey)
        {
            this.boomContext.Add(survey);

            foreach (var option in survey.Options)
            {
                option.Survey = survey;
                option.SurveyId = survey.Id;
                this.boomContext.Add(option);
            }

            this.boomContext.SaveChanges();

            return this.Json(survey);
        }
    }
}
