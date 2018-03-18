using eRNI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eRNI.ViewModels
{
    public class SpentWorkingTimeViewModel
    {

        public class RegulationActivity
        {
            public int activityID;
            public string projectLeader;
            public string projectSapNo;
            public string projectSapElement;
            public DateTime activityDate;
            public int? activityDuration;
        }

        public List<RegulationActivity> regulationActivity { get; set; }
        public string TimeOffset;

        public SpentWorkingTimeViewModel(string Leader, int timeOffset)
        {
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            DateTime first = month.AddMonths(-timeOffset);
            DateTime last;

            //if (timeOffset > 0)
            //{
            //    last = month.AddDays(-1);
            //}
            //else
            //{
                last = month.AddMonths(-timeOffset + 1).AddDays(-1);
            //}

            ApplicationDbContext db = new ApplicationDbContext();

            var queryActivityRegulation =
            (from actions in db.tblActions
             join projects in db.tblProjects on actions.projectID equals projects.projectID
             into actionGroup

             from projectLocalization in actionGroup.DefaultIfEmpty()
             join localization in db.tblLocalizations on projectLocalization.projectID equals localization.projectID
             into projectLocalizationGroup

             from devicesLocalization in projectLocalizationGroup.DefaultIfEmpty()
             join devices in db.tblDevices on devicesLocalization.localizationID equals devices.localizationID
             into devicesLocalizationGroup

             from devicesRegulation in devicesLocalizationGroup.DefaultIfEmpty()
             join regulation in db.tblRegulations on devicesRegulation.deviceID equals regulation.deviceID
             into devicesRegulationGroup

             from activityRegulation in devicesRegulationGroup.DefaultIfEmpty()
             where (actions.actionDate >= first && actions.actionDate <= last) && projectLocalization.projectLeader == Leader
             orderby actions.actionDate
             select new RegulationActivity
             {
                 activityID = actions.actionID,
                 projectLeader = projectLocalization.projectLeader,
                 projectSapNo = projectLocalization.projectSapNo,
                 projectSapElement = activityRegulation.regulationSapElement,
                 activityDate = actions.actionDate,
                 activityDuration = actions.actionDuration
             }

            into ActivityRegulation
            select ActivityRegulation).Distinct().OrderBy(a=>a.activityDate);

            this.regulationActivity = queryActivityRegulation.ToList();

            TimeOffset = "od " + String.Format("{0:yyyy-MM-dd}", first) + " do " + String.Format("{0:yyyy-MM-dd}", last);

        }
    }
}