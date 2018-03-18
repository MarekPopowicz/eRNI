using eRNI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eRNI.ViewModels
{
    public class CostsBalanceViewModel
    {
        public class RegulationCharges
        {
            public int regulationID;
            public string projectLeader;
            public string projectSapNo;
            public decimal? regulationCost;
            public DateTime regulationDocumentDate;
            public string regulationDocumentSignature;
            public string regulationDocumentType;
            public string regulationDocumentSource;
            public string regulationDocumentName;
            public DateTime? regulationDocumentChargeDate;
            public string regulationDocumentChargeTitle;
            public decimal? regulationDocumentFee;
        }


        public List<ApplicationUser> Users { get; set; }
        public List<RegulationCharges> regulationCharges { get; set; }
        public string TimeOffset;

 
        public CostsBalanceViewModel(int timeOffset)
        {
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            DateTime first = month.AddMonths(-timeOffset);
            DateTime last;

            if (timeOffset > 0)
            {
                last = month.AddDays(-1);
            }
            else
            {
                last = month.AddMonths(1).AddDays(-1);
            }


            TimeOffset = "od " + String.Format("{0:yyyy-MM-dd}", first) + " do " + String.Format("{0:yyyy-MM-dd}", last);
            ApplicationDbContext db = new ApplicationDbContext();
            Users = db.Users.ToList();


            var queryRegulationCharges =
            from regulations in db.tblRegulations
            join regDocs in db.tblRegulationDocuments
                 on regulations.regulationID equals regDocs.regulationID into regulationGroup

            from regulationDocuments in regulationGroup.DefaultIfEmpty()
            join regDocCharges in db.tblRegulationDocumentCharges
                 on regulationDocuments.regulationDocumentID equals regDocCharges.regulationDocumentID into regulationDocumentGroup

            from regulationDocumentCharges in regulationDocumentGroup.DefaultIfEmpty()
            where (regulationDocuments.regulationDocumentDate >= first && regulationDocuments.regulationDocumentDate <= last)

            orderby regulations.regulationID, regulationDocuments.regulationDocumentID, regulationDocumentCharges.regulationDocumentID
            select new RegulationCharges
            {
                regulationID = regulations.regulationID,
                projectLeader = regulations.device.localization.project.projectLeader,
                projectSapNo = regulations.device.localization.project.projectSapNo,
                regulationCost = regulations.regulationCost,
                regulationDocumentDate = regulationDocuments.regulationDocumentDate,
                regulationDocumentSignature = regulationDocuments.regulationDocumentSignature,
                regulationDocumentType = regulationDocuments.regulationDocumentType,
                regulationDocumentSource = regulationDocuments.regulationDocumentSource,
                regulationDocumentName = regulationDocumentCharges.regulationDocumentName,
                regulationDocumentChargeDate = regulationDocumentCharges.regulationDocumentChargeDate,
                regulationDocumentChargeTitle = regulationDocumentCharges.regulationDocumentChargeTitle,
                regulationDocumentFee = regulationDocumentCharges.regulationDocumentFee
            } into x
            //group x by x.regulationDocumentSignature into g
            select x;

            this.regulationCharges = queryRegulationCharges.ToList();
        }

    }

 }