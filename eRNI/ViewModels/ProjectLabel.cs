using eRNI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eRNI.ViewModels
{
    public class ProjectLabel
        {
        public string projectDocumentFolderNo { get; set; }
        public string projectSapNo { get; set; }
        public string projectTask { get; set; }
        public bool projectPriority { get; set; }
        public string projectManager { get; set; }
        public string projectLeader { get; set; }
        public List<Localization> projectLocalizations { get; set; }
        public List<LabelOwner> labelOwners { get; set; }

  
    public void SetProjectLabelData(Project project, List<Localization> projectLocalizations, List<Owner> projectOwners, List<Ownership> ownership)
        {
            this.projectDocumentFolderNo = project.projectID.ToString();
            this.projectSapNo = project.projectSapNo;
            this.projectTask = project.projectTask;
            this.projectPriority = project.projectPriority;
            this.projectManager = project.projectManager;
            this.projectLeader = project.projectLeader;
            this.projectLocalizations = projectLocalizations;

            List<LabelOwner> Owners = new List<LabelOwner>();
           
            foreach (var ownershipItem in ownership)
            {
                LabelOwner labelOwner = new LabelOwner();
                Owner owner = projectOwners.Where(o => o.ownerID == ownershipItem.ownerID).FirstOrDefault();
                labelOwner.setOwnerLocalizationData(ownershipItem.localizationID, owner);
                Owners.Add(labelOwner);
            }

            this.labelOwners = Owners;
        }

        
        public class LabelOwner
        {
            public int ownerLocalization { get; set; }
            public string ownerName { get; set; }
            public string ownerAptNo { get; set; }
            public string ownerCellPhone { get; set; }
            public string ownerPhone { get; set; }
            public string ownerCity { get; set; }
            public string ownerEmail { get; set; }
            public string ownerHouseNo { get; set; }
            public string ownerZipCode { get; set; }
            public string ownerPostOffice { get; set; }
            public string ownerStreet { get; set; }

            public void setOwnerLocalizationData(int localizationID, Owner labelOwner)
            {
                this.ownerLocalization = localizationID;
                this.ownerName = labelOwner.ownerName;
                this.ownerAptNo = labelOwner.ownerAptNo;
                this.ownerCellPhone = labelOwner.ownerCellPhone;
                this.ownerPhone = labelOwner.ownerPhone;
                this.ownerCity = labelOwner.ownerCity;
                this.ownerEmail = labelOwner.ownerEmail;
                this.ownerHouseNo = labelOwner.ownerHouseNo;
                this.ownerZipCode = labelOwner.ownerZipCode;
                this.ownerPostOffice = labelOwner.ownerPostOffice;
                if (labelOwner.streetID == null)
                {
                    this.ownerStreet = string.Empty;
                }
                else
                    this.ownerStreet = labelOwner.street.streetName;
            }
            
        }


    public bool IsAnyOwner(List<Ownership> ownership)
        {
            if(ownership.Select(i => i.localizationID).FirstOrDefault() > 0)
                {
                    return true;
                }
                else
                    return false;
        }
       
    }

   
}