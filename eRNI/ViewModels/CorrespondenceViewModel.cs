using System.Collections.Generic;
using System.Linq;
using eRNI.Models;

namespace eRNI.ViewModels
{
    public class CorrespondenceViewModel
    {
       public AddressBook addressee { get; set; }
       public List<Localization> localizations { get; set; }
       public ProjectCorrespondence projectCorrespondence { get; set; }
       public ApplicationUser templateOperator { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();

       public CorrespondenceViewModel(int? id)
       {
            this.projectCorrespondence = db.tblProjectCorrespondence.Find(id);
            this.addressee = db.tblAddressBook.Where(s => s.addresseeName.Equals(projectCorrespondence.projectCorrespondenceSender)).FirstOrDefault();
            this.localizations = db.tblLocalizations.Where(l => l.projectID == projectCorrespondence.projectID).ToList();
            this.templateOperator = db.Users.Where(u => u.UserName.Equals(projectCorrespondence.project.projectLeader)).FirstOrDefault();
        }

    }

  
}